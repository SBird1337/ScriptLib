using System.Collections.Generic;
using System.IO;

namespace ScriptLib.Scripts
{
    public class TextCommandProvider : ICommandProvider<TextCommand>
    {
        public TextCommand ReadCommand(BinaryReader reader, Script parent)
        {
            TextCommand cmd = new TextCommand(this);
            List<byte> text = new List<byte>();
            byte current;
            do
            {
                current = reader.ReadByte();
                text.Add(current);
            } while (current != 0xFF);
            cmd.Data = text.ToArray();
            return cmd;
        }
    }
}
