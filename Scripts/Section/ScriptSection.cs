using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace ScriptLib.Scripts.Section
{
    public class ScriptSection : ISection<ScriptCommand>
    {
        private string _labelName;
        private readonly List<ScriptCommand> _commands;
        public ScriptSection(string name, ScriptSectionProvider provider, BinaryReader reader, Script parent)
        {
            IsGlobal = false;
            _commands = new List<ScriptCommand>();
            Provider = provider;
            LabelName = name;
            Parent = parent;

            byte id;
            do
            {
                id = reader.ReadByte();
                _commands.Add(provider.Provider.Database.GetCommandDescriptionByIdent(id).ReadCommand(reader, Parent));
            }
            while (!provider.Provider.EndIdentifier.Contains(id));
        }
        public Script Parent { get; protected set; }

        public ScriptCommand[] Commands
        {
            get { return _commands.ToArray(); }
        }

        public string AssemblySection 
        {
            get { return "text"; }
        }

        public bool IsGlobal { get; set; }
        public event EventHandler<string> OnLabelNameChanged;

        public ISectionProvider<ScriptCommand> Provider { get; set; }

        public string LabelName {
            get { return _labelName; }
            set
            {
                _labelName = value;
                if(OnLabelNameChanged != null)
                    OnLabelNameChanged(this, _labelName);
            }
        }

        public byte[] Value
        {
            get
            {
                return Extensions.ArrayHelper.Combine(Commands.Select(command => command.Data).ToArray());
            }
        }

        public bool HasData
        {
            get { return _commands.Count > 0; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ICommand cmd in Commands)
                sb.AppendLine(cmd.ToString());
            return sb.ToString();
        }
    }
}