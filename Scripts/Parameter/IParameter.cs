using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public interface IParameter
    {
        byte[] Value { get; }
        int Length { get; }
        IParameterProvider<IParameter> Provider { get; set; }
        string ToString(IDeparser deparser);
    }
}
