using System.Text.RegularExpressions;
using Content.Server._Genesis.Speech.Components;
using Content.Shared.Speech;
using Robust.Shared.Random;

namespace Content.Server._Genesis.Speech.EntitySystems;

public sealed partial class GrowlingAccentSystem : EntitySystem
{
    [Dependency] private IRobustRandom _random = default!;
    private static readonly Regex LowerLatinR = new("r+");
    private static readonly Regex UpperLatinR = new("R+");
    private static readonly Regex LowerCyrillicR = new("р+");
    private static readonly Regex UpperCyrillicR = new("Р+");

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<GrowlingAccentComponent, AccentGetEvent>(OnAccent);
    }

    private void OnAccent(EntityUid uid, GrowlingAccentComponent component, AccentGetEvent args)
    {
        var message = args.Message;

        // r => rrr
        message = LowerLatinR.Replace(
            message,
            _random.Pick(new List<string> { "rr", "rrr" })
        );
        // R => RRR
        message = UpperLatinR.Replace(
            message,
            _random.Pick(new List<string> { "RR", "RRR" })
        );

        // р => ррр
        message = LowerCyrillicR.Replace(
            message,
            _random.Pick(new List<string> { "рр", "ррр" })
        );
        // Р => РРР
        message = UpperCyrillicR.Replace(
            message,
            _random.Pick(new List<string> { "РР", "РРР" })
        );

        args.Message = message;
    }
}
