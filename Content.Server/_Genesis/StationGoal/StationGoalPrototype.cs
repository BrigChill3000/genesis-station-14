using Robust.Shared.Prototypes;

namespace Content.Server._Genesis.StationGoal
{
    [Prototype]
    public sealed partial class StationGoalPrototype : IPrototype
    {
        [IdDataFieldAttribute]
        public string ID { get; private set; } = default!;

        [DataField]
        public string Text { get; set; } = string.Empty;

        [DataField]
        public int? MinPlayers;

        [DataField]
        public int? MaxPlayers;

    }
}
