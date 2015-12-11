using System.Collections.Generic;
using System.IO;
using System.Text;
using ScriptLib.Parser;
using ScriptLib.Scripts.Parameter;

namespace ScriptLib.Scripts
{
    public class ScriptCommand : ICommand
    {
        private readonly byte _identifier;
        public List<IParameter> Parameters { get; set; }

        public ScriptCommand(byte ident, ScriptCommandProvider provider)
        {
            _identifier = ident;
            Provider = provider;
            Parameters = new List<IParameter>();
        }

        public byte[] Data
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(_identifier);
                foreach (IParameter parameter in Parameters)
                {
                    bw.Write(parameter.Value);
                }
                return ms.ToArray();
            }
        }

        public int Length
        {
            get { return Data.Length; }
        }

        public string ToString(IDeparser deparser)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(deparser.DeparseByte(_identifier));
            sb.Append(" ");
            sb.AppendLine(deparser.CreateLineComment(((ScriptCommandProvider)Provider).Name));
            foreach (IParameter parameter in Parameters)
            {
                sb.AppendLine(deparser.DeparseParameter(parameter));
            }
            return sb.ToString();
        }

        public ICommandProvider<ICommand> Provider { get; private set; }

        //For testing purposes
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("0x{0}", _identifier.ToString("X"));
            foreach (IParameter parameter in Parameters)
            {
                sb.AppendFormat(" 0x{0}", parameter);
            }
            return sb.ToString();
        }
    }
}
