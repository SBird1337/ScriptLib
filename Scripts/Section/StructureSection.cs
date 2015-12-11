using System;
using System.IO;

namespace ScriptLib.Scripts.Section
{
    public class StructureSection : ISection<StructureCommand>
    {
        private string _labelName;
        private readonly StructureCommand _command;

        public StructureSection(string name, StructureSectionProvider provider, BinaryReader reader, Script parent)
        {
            IsGlobal = false;
            Provider = provider;
            LabelName = name;
            Parent = parent;

            _command = provider.Provider.ReadCommand(reader, parent);
        }

        public ISectionProvider<StructureCommand> Provider { get; private set; }
        public byte[] Value {
            get { return _command.Data; }
        }
        public string LabelName
        {
            get { return _labelName; }
            set
            {
                _labelName = value;
                if (OnLabelNameChanged != null)
                    OnLabelNameChanged(this, _labelName);
            }
        }

        public Script Parent { get; private set; }
        public StructureCommand[] Commands
        {
            get { return new[] { _command }; }
        }
        public string AssemblySection
        {
            get { return "text"; }
        }
        public bool IsGlobal { get; set; }
        public event EventHandler<string> OnLabelNameChanged;
    }

    public class StructureSectionProvider : ISectionProvider<StructureCommand>
    {
        public StructureSectionProvider(string name, StructureCommandProvider cmdProvider)
        {
            Name = name;
            Provider = cmdProvider;
        }

        public StructureCommandProvider Provider { get; private set; }

        public string Name { get; private set; }
        public ISection<StructureCommand> Create(string labelName, BinaryReader reader, Script parent)
        {
            return new StructureSection(labelName, this, reader, parent);
        }
    }
}
