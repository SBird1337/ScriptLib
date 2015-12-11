using System.Collections.Generic;
using System.IO;
using ScriptLib.Parser;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts
{
    public class Script
    {
        public List<ISection<ICommand>> SubSections { get; set; }
        public ISection<ICommand> MainSection { get; set; }

        public Script(BinaryReader reader, ISectionProvider<ICommand> provider, string mainName, bool isGlobal = false)
        {
            SubSections = new List<ISection<ICommand>>();
            MainSection = provider.Create(mainName, reader, this);
            MainSection.IsGlobal = isGlobal;
        }

        public string ToString(IDeparser deparser)
        {
            return deparser.DeparseScript(this);
        }

        public void Refractor()
        {

        }
    }
}
