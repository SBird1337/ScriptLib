using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptLib.Extensions;
using ScriptLib.Parser;
using ScriptLib.Scripts.Parameter;

namespace ScriptLib.Scripts
{
    public class StructureCommand : ICommand
    {
        public StructureCommand(StructureCommandProvider provider)
        {
            Types = new List<IParameter>();
            Provider = provider;
        }

        public List<IParameter> Types { get; set; }

        public byte[] Data
        {
            get { return ArrayHelper.Combine(Types.Select(type => type.Value).ToArray()); }
        }

        public int Length
        {
            get { return Data.Length; }
        }

        public string ToString(IDeparser deparser)
        {
            return string.Join(Environment.NewLine, Types.Select(type => type.ToString(deparser)));
        }

        public ICommandProvider<ICommand> Provider { get; private set; }
    }
}
