using System;

namespace ScriptLib.Scripts.Section
{
    public interface ISection<out T> where T : ICommand
    {
        ISectionProvider<T> Provider { get; }
        byte[] Value { get; }
        string LabelName { get; set; }
        Script Parent { get; }
        T[] Commands { get; }
        string AssemblySection { get; }
        bool IsGlobal { get; set; }
        event EventHandler<string> OnLabelNameChanged;
    }
}
