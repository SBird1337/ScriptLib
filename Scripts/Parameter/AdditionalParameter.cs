using System;
using System.Collections.Generic;
using System.Text;
using ScriptLib.Parser;

namespace ScriptLib.Scripts.Parameter
{
    public class AdditionalParameter : IParameter
    {
        private readonly byte _amount;
        private readonly ushort[] _parameters;

        public AdditionalParameter(byte amount, AdditionalParameterProvider provider, params ushort[] arguments )
        {
            Provider = provider;
            _amount = amount;
            _parameters = arguments;
        }

        public byte[] Value {
            get
            {
                List<byte> val = new List<byte> {_amount};
                foreach(ushort parameter in _parameters)
                    val.AddRange(BitConverter.GetBytes(parameter));
                return val.ToArray();
            }
        }
        public int Length {
            get { return _parameters.Length + 2; }
        }
        public IParameterProvider<IParameter> Provider { get; set; }
        public string ToString(IDeparser deparser)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(deparser.DeparseByte(_amount));
            foreach (ushort parameter in _parameters)
            {
                sb.AppendLine(deparser.DeparseHalfword(parameter));
            }
            return sb.ToString();
        }
    }
}
