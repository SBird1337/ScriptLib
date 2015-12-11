using System;
using System.IO;

namespace ScriptLib.Scripts.Section
{
    public class TextSection : ISection<TextCommand>
    {
        private readonly TextCommand _text;
        private string _labelName;
        public TextSection(string name, BinaryReader reader, Script parent, TextSectionProvider secProvider)
        {
            IsGlobal = false;
            LabelName = name;
            Parent = parent;
            TextCommandProvider provider = new TextCommandProvider();
            _text = provider.ReadCommand(reader, parent);
            Provider = secProvider;
        }

        public ISectionProvider<TextCommand> Provider { get; private set; }

        public byte[] Value
        {
            get { return _text.Data; }
        }

        public string LabelName {
            get { return _labelName; }
            set
            {
                _labelName = value;
                if(OnLabelNameChanged != null)
                    OnLabelNameChanged(this, _labelName);
            }
        }
        public Script Parent { get; private set; }

        public TextCommand[] Commands
        {
            get { return new[] { _text }; }
        }

        public string AssemblySection
        {
            get { return "text"; }
        }

        public bool IsGlobal { get; set; }
        public event EventHandler<string> OnLabelNameChanged;
    }
}
