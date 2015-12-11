using System.Text;
using ScriptLib.Scripts;
using ScriptLib.Scripts.Parameter;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Parser
{
    public class AsmDeparser : IDeparser
    {
        public string DeparseByte(byte b)
        {
            return string.Format(".byte 0x{0}", b.ToString("X2"));
        }

        public string DeparseHalfword(ushort h)
        {
            return string.Format(".hword 0x{0}", h.ToString("X4"));
        }

        public string DeparseWord(uint w)
        {
            return string.Format(".word 0x{0}", w.ToString("X8"));
        }

        public string DeparseParameter(IParameter parameter)
        {
            if (parameter is ScriptPointerParameter)
            {
                ScriptPointerParameter pointer = parameter as ScriptPointerParameter;
                if(pointer.IsDynamic)
                return ".word " + pointer.DynamicName;
            }
            return parameter.ToString(this);
        }

        public string CreateLineComment(string comment)
        {
            return "@" + comment;
        }

        public string DeparseCommand(ICommand cmd)
        {
            return cmd.ToString(this);
        }

        public string DeparseSection(ISection<ICommand> section)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(".section ." + section.AssemblySection);
            if (section.IsGlobal)
                sb.AppendLine(".global " + section.LabelName + "\n");
            sb.AppendLine(".align 2");
            sb.AppendLine(section.LabelName + ":");
            //sb.AppendLine();
            foreach (ICommand cmd in section.Commands)
            {
                sb.AppendLine(DeparseCommand(cmd));
            }
            return sb.ToString();
        }

        public string DeparseScript(Script script)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DeparseSection(script.MainSection));
            foreach (ISection<ICommand> section in script.SubSections)
            {
                sb.Append(DeparseSection(section));
            }
            return sb.ToString();
        }
    }
}
