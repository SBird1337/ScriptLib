using System.Collections.Generic;
using System.IO;
using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public class AdditionalParameterProvider : IParameterProvider<IParameter>
    {
        public string Name 
        {
            get { return "additional"; }
        }
        public IParameter Create(BinaryReader reader)
        {
            byte amount = reader.ReadByte();
            List<ushort> add = new List<ushort>();
            for (int i = 0; i < amount; ++i)
            {
                add.Add(reader.ReadUInt16());
            }
            return new AdditionalParameter(amount, this, add.ToArray());
        }

        public string ToString(IDeparser deparser, IParameter parameter)
        {
            return parameter.ToString(deparser);
        }
    }
}
