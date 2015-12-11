using ScriptLib.Scripts.Parameter;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts
{
    public class AnimationLanguage : LanguageProviderBase
    {
        public override string CommandFile
        {
            get { return @"command\anim.slh"; }
        }

        public override void PreInitialize()
        {
            base.PreInitialize();
            Database.Types.Add(ScriptParameterProviderByte.Instance);
            Database.Types.Add(ScriptParameterProviderShort.Instance);
            Database.Types.Add(ScriptParameterProviderWord.Instance);
            Database.Types.Add(new AdditionalParameterProvider());
        }

        public override void Initialize()
        {
            Database.Sections.Add(new ScriptSectionProvider(this));
            Database.Sections.Add(new TextSectionProvider());
            Database.Sections.Add(new StructureSectionProvider("oam", new StructureCommandProvider(@"struct\objecttemp.slh", Database)));
        }

        public override void PostInitialize()
        {
            foreach (ISectionProvider<ICommand> provider in Database.Sections)
                Database.Types.Add(new ScriptPointerParameterProvider(provider.Name, Database));
        }



        public override byte[] EndIdentifier {
            get { return new byte[] {0x8, 0xF, 0x11, 0x13}; }
        }
    }
}
