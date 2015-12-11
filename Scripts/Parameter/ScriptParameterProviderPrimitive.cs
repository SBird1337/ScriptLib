using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptLib.Script.Parameter
{
    public abstract class ScriptParameterProviderPrimitive : IParameterProvider<ScriptParameter>
    {
        public abstract string Name { get; }
        public abstract int Length { get; }

        public ScriptParameter Create(byte[] value)
        {
            return new ScriptParameter(value.Take(Length).ToArray());
        }
    }
}
