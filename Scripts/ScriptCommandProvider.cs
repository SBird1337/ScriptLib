using System.Collections.Generic;
using System.IO;
using System.Text;
using ScriptLib.Scripts.Parameter;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts
{
    public class ScriptCommandProvider : ICommandProvider<ScriptCommand>
    {
        public ScriptCommandProvider(string name, byte identifier, ScriptDatabase database)
        {
            Name = name;
            Identifier = identifier;
            Parameters = new List<string>();
            Database = database;
        }

        public List<string> Parameters { get; set; }
        public byte Identifier { get; set; }
        public string Name { get; set; }
        public ScriptDatabase Database { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("0x");
            sb.Append(Identifier.ToString("X"));
            sb.Append(": ");
            sb.Append(Name);

            foreach (string s in Parameters)
            {
                sb.Append(" [");
                sb.Append(s);
                sb.Append("]");
            }
            return sb.ToString();
        }

        public ScriptCommand ReadCommand(BinaryReader reader, Script parent)
        {
            ScriptCommand command = new ScriptCommand(Identifier, this);
            foreach (string s in Parameters)
            {
                IParameterProvider<IParameter> provider = Database.GetTypeDescriptionByName(s);
                IParameter parameter = provider.Create(reader);
                command.Parameters.Add(parameter);
                if (parameter is ScriptPointerParameter)
                {
                    ScriptPointerParameter pointer = (parameter as ScriptPointerParameter);

                    //Console.WriteLine("I am a pointer: {0}", pointer.DynamicName);
                    //Let the parent script create a section
                    //TODO: Implement abstract ReadSubSection
                    long op = reader.BaseStream.Position;
                    ISection<ICommand> section = pointer.CreateSection(reader, parent);
                    reader.BaseStream.Position = op;
                    parent.SubSections.Add(section);
                    //TODO: Add section to parent script
                }
            }
            return command;
        }
    }
}
