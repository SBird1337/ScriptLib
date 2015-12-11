using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public class ScriptParameter : IParameter
    {
        public byte[] Value { get; set; }

        public int Length
        {
            get { return Value.Length; }
        }

        public IParameterProvider<IParameter> Provider { get; set; }
        public string ToString(IDeparser deparser)
        {
            return Provider.ToString(deparser, this);
        }

        public ScriptParameter(byte[] value, IParameterProvider<ScriptParameter> provider)
        {
            Provider = provider;
            Value = value;
        }
    }
}
