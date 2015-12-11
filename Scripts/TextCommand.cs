using System.Text;
using ScriptLib.Parser;

namespace ScriptLib.Scripts
{
    public class TextCommand : ICommand
    {
        public TextCommand(TextCommandProvider provider)
        {
            Provider = provider;
        }

        public byte[] Data { get; set; }

        public int Length
        {
            get { return Data.Length; }
        }
        public string ToString(IDeparser deparser)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in Data)
            {
                sb.AppendFormat(".byte 0x{0} ", b.ToString("X2"));
            }
            return sb.ToString();
        }

        public ICommandProvider<ICommand> Provider { get; private set; }
    }
}
