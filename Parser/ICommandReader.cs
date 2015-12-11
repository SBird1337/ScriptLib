using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptLib.Script;

namespace ScriptLib.Parser
{
    public interface ICommandReader<T> where T : ICommand
    {
        T ReadCommand(BinaryReader reader, bool refractor);
        IEnumerable<T> ReadCommandsToEnd(BinaryReader reader, bool refractor);
    }
}
