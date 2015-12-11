using System.IO;
using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public class ScriptParameterProviderByte : IParameterProvider<ScriptParameter>
    {
        private static ScriptParameterProviderByte _instance;

        public static ScriptParameterProviderByte Instance
        {
            get { return _instance ?? (_instance = new ScriptParameterProviderByte()); }
        }

        public string Name
        {
            get { return "byte"; }
        }

        public int Length
        {
            get { return 1; }
        }

        public ScriptParameter Create(BinaryReader reader)
        {
            return new ScriptParameter(reader.ReadBytes(Length), this);
        }

        public string ToString(IDeparser deparser, IParameter parameter)
        {
            return deparser.DeparseByte(parameter.Value[0]);
        }
    }
}
