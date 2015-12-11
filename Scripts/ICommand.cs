using ScriptLib.Parser;

namespace ScriptLib.Scripts
{
    public interface ICommand
    {
        byte[] Data { get; }
        int Length { get; }
        string ToString(IDeparser deparser);
        ICommandProvider<ICommand> Provider { get; }
    }
}
