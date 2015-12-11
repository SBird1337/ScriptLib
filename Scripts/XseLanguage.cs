using ScriptLib.Scripts.Parameter;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts
{
    public class XseLanguage : LanguageProviderBase
    {

        public override void Initialize()
        {
            Database.Sections.Add(new ScriptSectionProvider(this));
            Database.Sections.Add(new TextSectionProvider());
        }

        public override void PostInitialize()
        {
            Database.Types.Add(ScriptParameterProviderByte.Instance);
            Database.Types.Add(ScriptParameterProviderShort.Instance);
            Database.Types.Add(ScriptParameterProviderWord.Instance);
            foreach(ISectionProvider<ICommand> provider in Database.Sections)
                Database.Types.Add(new ScriptPointerParameterProvider(provider.Name, Database));
        }

        public override string CommandFile
        {
            get { return @"command\xse.slh"; }
        }

        public override byte[] EndIdentifier
        {
            get { return new byte[] {0x2, 0x3}; }
        }
    }
}
