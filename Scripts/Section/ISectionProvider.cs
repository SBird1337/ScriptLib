using System.IO;

namespace ScriptLib.Scripts.Section
{
    public interface ISectionProvider<out T> where T : ICommand
    {
        string Name { get; }
        ISection<T> Create(string labelName, BinaryReader reader, Script parent);
    }
}
