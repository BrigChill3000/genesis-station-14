using Content.Shared.Actions;
using Content.Shared.Audio;
using Content.Shared.StatusEffect;
using Content.Shared.Throwing;
using Content.Shared.Item;
using Content.Shared.Inventory;
using Content.Shared.Hands;
using Content.Shared.Actions.Events;
using Content.Shared.IdentityManagement;
using Content.Shared.Nutrition.Components;
using Content.Shared.Nutrition.EntitySystems;
using Content.Server.Medical;
using Content.Server.Popups;
using Robust.Shared.Random;
using Robust.Shared.Prototypes;
using Robust.Shared.Player;
using Robust.Server.Audio;
using Content.Server.Chemistry.EntitySystems;
using Content.Shared.Medical;
using Content.Shared.Tag;
using Content.Server._Genesis.Abilities.Felinid;
using Content.Shared.Body.Components;
using Content.Shared.Charges.Systems;
using Content.Shared.Charges.Components;

namespace Content.Server._Genesis.Abilities.Felinid;

public sealed partial class FelinidSystem : EntitySystem
{
    [Dependency] private SharedActionsSystem _actionsSystem = default!;
    [Dependency] private SatiationSystem _satiationSystem = default!;
    [Dependency] private VomitSystem _vomitSystem = default!;
    [Dependency] private SolutionContainerSystem _solutionContainer = default!;
    [Dependency] private IRobustRandom _robustRandom = default!;
    [Dependency] private PopupSystem _popupSystem = default!;
    [Dependency] private InventorySystem _inventorySystem = default!;
    [Dependency] private AudioSystem _audio = default!;
    [Dependency] private TagSystem _tagSystem = default!;
    [Dependency] private SharedChargesSystem _charges = default!;

    private static readonly EntProtoId EatMouseActionId = "ActionEatMouse";

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<FelinidComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<FelinidComponent, HairballActionEvent>(OnHairball);
        SubscribeLocalEvent<FelinidComponent, EatMouseActionEvent>(OnEatMouse);
        SubscribeLocalEvent<FelinidComponent, DidEquipHandEvent>(OnEquipped);
        SubscribeLocalEvent<FelinidComponent, DidUnequipHandEvent>(OnUnequipped);
        SubscribeLocalEvent<HairballComponent, ThrowDoHitEvent>(OnHairballHit);
        SubscribeLocalEvent<HairballComponent, GettingPickedUpAttemptEvent>(OnHairballPickupAttempt);
    }

    private Queue<EntityUid> RemQueue = new();

    public override void Update(float frameTime)
    {
        base.Update(frameTime);
        foreach (var cat in RemQueue)
        {
            RemComp<CoughingUpHairballComponent>(cat);
        }
        RemQueue.Clear();

        foreach (var (hairballComp, catComp) in EntityQuery<CoughingUpHairballComponent, FelinidComponent>())
        {
            hairballComp.Accumulator += frameTime;
            if (hairballComp.Accumulator < hairballComp.CoughUpTime.TotalSeconds)
                continue;

            hairballComp.Accumulator = 0;
            SpawnHairball(hairballComp.Owner, catComp);
            RemQueue.Enqueue(hairballComp.Owner);
        }
    }

    private void OnMapInit(EntityUid uid, FelinidComponent component, MapInitEvent args)
    {
        _actionsSystem.AddAction(uid, ref component.HairballActionEntity, component.HairballAction, uid);
    }

    private void OnEquipped(EntityUid uid, FelinidComponent component, DidEquipHandEvent args)
    {
        if (!_tagSystem.HasTag(args.Equipped, "FelinidFood"))
            return;

        component.PotentialTarget = args.Equipped;

        _actionsSystem.AddAction(uid, ref component.EatMouse, EatMouseActionId);
    }

    private void OnUnequipped(EntityUid uid, FelinidComponent component, DidUnequipHandEvent args)
    {
        if (args.Unequipped == component.PotentialTarget)
        {
            component.PotentialTarget = null;
            _actionsSystem.RemoveAction(uid, component.EatMouse);
        }
    }

    private void OnHairball(EntityUid uid, FelinidComponent component, HairballActionEvent args)
    {
        if (_inventorySystem.TryGetSlotEntity(uid, "mask", out var maskUid) &&
        EntityManager.TryGetComponent<IngestionBlockerComponent>(maskUid, out var blocker) &&
        blocker.Enabled)
        {
            _popupSystem.PopupEntity(Loc.GetString("hairball-mask", ("mask", maskUid)), uid, uid);
            return;
        }

        _popupSystem.PopupEntity(Loc.GetString("hairball-cough", ("name", Identity.Entity(uid, EntityManager))), uid);
        _audio.PlayEntity("/Audio/Backmen/Effects/Species/hairball.ogg", Filter.Pvs(uid), uid, true, AudioHelpers.WithVariation(0.15f));

        EnsureComp<CoughingUpHairballComponent>(uid);
        args.Handled = true;
    }

    private void OnEatMouse(EntityUid uid, FelinidComponent component, EatMouseActionEvent args)
    {
        if (component.PotentialTarget == null)
            return;

        if (!TryComp<SatiationComponent>(uid, out var hunger))
            return;

        var satiation = new Entity<SatiationComponent>(uid, hunger);
        if (_satiationSystem.GetValueOrNull(satiation, SatiationSystem.Hunger) is { } value &&
            _satiationSystem.GetMaximumValue(satiation, SatiationSystem.Hunger) is { } max &&
            value >= max)
        {
            _popupSystem.PopupEntity(Loc.GetString("food-system-you-cannot-eat-any-more"), uid, uid, Shared.Popups.PopupType.SmallCaution);
            return;
        }

        if (_inventorySystem.TryGetSlotEntity(uid, "mask", out var maskUid) &&
        EntityManager.TryGetComponent<IngestionBlockerComponent>(maskUid, out var blocker) &&
        blocker.Enabled)
        {
            _popupSystem.PopupEntity(Loc.GetString("hairball-mask", ("mask", maskUid)), uid, uid, Shared.Popups.PopupType.SmallCaution);
            return;
        }

        if (_actionsSystem.GetAction(component.HairballActionEntity) is { } action)
        {
            EnsureComp<LimitedChargesComponent>(action, out var chargeComp);
            _charges.AddCharges((action, chargeComp), 1);
        }

        _actionsSystem.SetEnabled(component.HairballActionEntity, true);

        QueueDel(component.PotentialTarget.Value);
        component.PotentialTarget = null;

        _audio.PlayEntity(component.EatSound, Filter.Pvs(uid), uid, true, AudioHelpers.WithVariation(0.15f));

        _satiationSystem.ModifyValue(satiation, SatiationSystem.Hunger, 70f);

        _actionsSystem.RemoveAction(uid, component.EatMouse);
    }

    private void SpawnHairball(EntityUid uid, FelinidComponent component)
    {
        var hairball = EntityManager.SpawnEntity(component.HairballPrototype, Transform(uid).Coordinates);
        var hairballComp = Comp<HairballComponent>(hairball);

        if (TryComp<BloodstreamComponent>(uid, out var bloodStream) &&
            _solutionContainer.ResolveSolution(uid, bloodStream.BloodSolutionName, ref bloodStream.BloodSolution))
        {
            var vomitChemstreamAmount = _solutionContainer.SplitSolution(bloodStream.BloodSolution.Value, 20);

            if (_solutionContainer.TryGetSolution(hairball, hairballComp.SolutionName, out var hairballSolution))
            {
                _solutionContainer.TryAddSolution(hairballSolution.Value, vomitChemstreamAmount);
            }
        }
    }
    private void OnHairballHit(EntityUid uid, HairballComponent component, ThrowDoHitEvent args)
    {
        if (HasComp<FelinidComponent>(args.Target) || !HasComp<StatusEffectsComponent>(args.Target))
            return;

        if (_robustRandom.Prob(0.2f))
            _vomitSystem.Vomit(args.Target);
    }

    private void OnHairballPickupAttempt(EntityUid uid, HairballComponent component, GettingPickedUpAttemptEvent args)
    {
        if (HasComp<FelinidComponent>(args.User) || !HasComp<StatusEffectsComponent>(args.User))
            return;

        if (_robustRandom.Prob(0.2f))
        {
            _vomitSystem.Vomit(args.User);
            args.Cancel();
        }
    }
}
