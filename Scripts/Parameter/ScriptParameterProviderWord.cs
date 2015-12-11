using System;
using System.IO;
using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public class ScriptParameterProviderWord : IParameterProvider<ScriptParameter>
    {
        private static ScriptParameterProviderWord _instance;

        public static ScriptParameterProviderWord Instance
        {
            get { return _instance ?? (_instance = new ScriptParameterProviderWord()); }
        }

        public string Name
        {
            get { return "word"; }
        }

        public int Length
        {
            get { return 4; }
        }

        public ScriptParameter Create(BinaryReader reader)
        {
            return new ScriptParameter(reader.ReadBytes(Length), this);
        }

        public string ToString(IDeparser deparser, IParameter parameter)
        {
            return deparser.DeparseWord(BitConverter.ToUInt32(parameter.Value, 0));
        }
    }
}
