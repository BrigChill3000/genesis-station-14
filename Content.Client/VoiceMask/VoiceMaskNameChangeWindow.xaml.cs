using Content.Shared._Genesis.TTS;
using Content.Client.UserInterface.Controls;
using Content.Shared._Genesis.CCCVars;
using Content.Shared.Speech;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Prototypes;

namespace Content.Client.VoiceMask;

[GenerateTypedNameReferences]
public sealed partial class VoiceMaskNameChangeWindow : FancyWindow
{
    public Action<string>? OnNameChange;
    public Action<string?>? OnVerbChange;
    public Action<string>? OnVoiceChange; // Genesis-TTS

    private List<(string, string)> _verbs = new();
    private List<TTSVoicePrototype> _voices = new(); // Genesis-TTS

    private string? _verb;

    public VoiceMaskNameChangeWindow(IPrototypeManager proto)
    {
        RobustXamlLoader.Load(this);

        NameSelectorSet.OnPressed += _ =>
        {
            OnNameChange?.Invoke(NameSelector.Text);
        };

        SpeechVerbSelector.OnItemSelected += args =>
        {
            OnVerbChange?.Invoke((string?) args.Button.GetItemMetadata(args.Id));
            SpeechVerbSelector.SelectId(args.Id);
        };

        ReloadVerbs(proto);

        // Genesis-TTS-Start
        if (IoCManager.Resolve<IConfigurationManager>().GetCVar(CCCVars.TTSEnabled))
        {
            TTSContainer.Visible = true;
            ReloadVoices(proto);
        }
        // Genesis-TTS-End

        AddVerbs();
    }

    private void ReloadVerbs(IPrototypeManager proto)
    {
        foreach (var verb in proto.EnumeratePrototypes<SpeechVerbPrototype>())
        {
            _verbs.Add((Loc.GetString(verb.Name), verb.ID));
        }
        _verbs.Sort((a, b) => a.Item1.CompareTo(b.Item1));
    }

    private void AddVerbs()
    {
        SpeechVerbSelector.Clear();

        AddVerb(Loc.GetString("chat-speech-verb-name-none"), null);
        foreach (var (name, id) in _verbs)
        {
            AddVerb(name, id);
        }
    }

    private void AddVerb(string name, string? verb)
    {
        var id = SpeechVerbSelector.ItemCount;
        SpeechVerbSelector.AddItem(name);
        if (verb is {} metadata)
            SpeechVerbSelector.SetItemMetadata(id, metadata);

        if (verb == _verb)
            SpeechVerbSelector.SelectId(id);
    }

    // Genesis-TTS-Start
    private void ReloadVoices(IPrototypeManager proto)
    {
        VoiceSelector.OnItemSelected += args =>
        {
            VoiceSelector.SelectId(args.Id);
            if (VoiceSelector.SelectedMetadata != null)
                OnVoiceChange!((string)VoiceSelector.SelectedMetadata);
        };
        _voices = proto
            .EnumeratePrototypes<TTSVoicePrototype>()
            .Where(o => o.RoundStart)
            .OrderBy(o => Loc.GetString(o.Name))
            .ToList();
        for (var i = 0; i < _voices.Count; i++)
        {
            var name = Loc.GetString(_voices[i].Name);
            VoiceSelector.AddItem(name);
            VoiceSelector.SetItemMetadata(i, _voices[i].ID);
        }
    }
    // Genesis-TTS-End

    public void UpdateState(string name, string voice, string? verb) // Genesis-TTS
    {
        NameSelector.Text = name;
        _verb = verb;

        for (int id = 0; id < SpeechVerbSelector.ItemCount; id++)
        {
            if (string.Equals(verb, SpeechVerbSelector.GetItemMetadata(id)))
            {
                SpeechVerbSelector.SelectId(id);
                break;
            }
        }

        // Genesis-TTS-Start
        var voiceIdx = _voices.FindIndex(v => v.ID == voice);
        if (voiceIdx != -1)
            VoiceSelector.Select(voiceIdx);
        // Genesis-TTS-End
    }
}
