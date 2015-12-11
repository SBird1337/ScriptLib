using System;
using System.IO;
using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public class ScriptParameterProviderShort : IParameterProvider<ScriptParameter>
    {
        private static ScriptParameterProviderShort _instance;

        public static ScriptParameterProviderShort Instance
        {
            get { return _instance ?? (_instance = new ScriptParameterProviderShort()); }
        }

        public string Name
        {
            get { return "hword"; }
        }

        public int Length
        {
            get { return 2; }
        }

        public ScriptParameter Create(BinaryReader reader)
        {
            return new ScriptParameter(reader.ReadBytes(Length), this);
        }

        public string ToString(IDeparser deparser, IParameter parameter)
        {
            return deparser.DeparseHalfword(BitConverter.ToUInt16(parameter.Value, 0));
        }
    }
}
