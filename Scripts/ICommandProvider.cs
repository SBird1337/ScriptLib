using System.IO;

namespace ScriptLib.Scripts
{
    public interface ICommandProvider<out T> where T : ICommand
    {
        T ReadCommand(BinaryReader reader, Script parent);
    }
}
