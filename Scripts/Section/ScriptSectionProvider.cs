using System.IO;

namespace ScriptLib.Scripts.Section
{
    public class ScriptSectionProvider : ISectionProvider<ScriptCommand>
    {

        public ILanguageProvider Provider { get; private set; }

        public ScriptSectionProvider(ILanguageProvider provider)
        {
            Provider = provider;
        }

        public string Name
        {
            get { return "script"; }
        }

        public ISection<ScriptCommand> Create(string labelName, BinaryReader reader, Script parent)
        {
            return new ScriptSection(labelName, this, reader, parent);
        }
    }
}
