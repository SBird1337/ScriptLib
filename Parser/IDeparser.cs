using ScriptLib.Scripts;
using ScriptLib.Scripts.Parameter;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Parser
{
    public interface IDeparser
    {
        string CreateLineComment(string comment);
        string DeparseCommand(ICommand cmd);
        string DeparseSection(ISection<ICommand> section);
        string DeparseScript(Script script);
        string DeparseByte(byte b);
        string DeparseHalfword(ushort s);
        string DeparseWord(uint w);
        string DeparseParameter(IParameter parameter);
    }
}
