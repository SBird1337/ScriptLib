using System.IO;
using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public interface IParameterProvider<out T> where T : IParameter
    {
        string Name { get; }
        T Create(BinaryReader reader);
        string ToString(IDeparser deparser, IParameter parameter);
    }
}
