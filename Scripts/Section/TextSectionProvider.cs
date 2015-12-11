namespace ScriptLib.Scripts.Section
{
    public class TextSectionProvider : ISectionProvider<TextCommand>
    {
        public string Name
        {
            get { return "text"; }
        }

        public ISection<TextCommand> Create(string labelName, System.IO.BinaryReader reader, Script parent)
        {
            return new TextSection(labelName, reader, parent, this);
        }
    }
}
