using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptLib.Extensions;
using ScriptLib.Scripts.Parameter;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts
{
    public class ScriptDatabase
    {
        public ScriptDatabase(params string[] paths)
        {
            Types = new List<IParameterProvider<IParameter>>();
            Commands = new List<ScriptCommandProvider>();
            Sections = new List<ISectionProvider<ICommand>>();
            foreach (string s in paths)
            {
                LoadCommandsFromFile(s);
            }
        }

        public List<IParameterProvider<IParameter>> Types { get; set; }
        public List<ScriptCommandProvider> Commands { get; set; }
        public List<ISectionProvider<ICommand>> Sections { get; set; }

        private void LoadCommandsFromFile(string path)
        {
            LoadCommandsFromString(File.ReadAllText(path));
        }

        private void LoadCommandsFromString(string commands)
        {
            using (StringReader reader = new StringReader(commands))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] commandparts = line.Split(' ');
                    if(commandparts.Length < 2)
                        throw new FormatException(string.Format("Command not in format \"[name] [identifier] <parameters>\": {0}", line));

                    byte ident;
                    if(!commandparts[1].TryParse(out ident))
                        throw new FormatException(string.Format("The identifier {0} should be in hexadecimal or decimal form", commandparts[1]));
                    ScriptCommandProvider description = new ScriptCommandProvider(commandparts[0], ident, this);
                    for(int i = 2; i < commandparts.Length; ++i)
                        description.Parameters.Add(commandparts[i]);

                    Commands.Add(description);
                }
            }
        }

        public ScriptCommandProvider GetCommandDescriptionByIdent(byte ident)
        {
            return Commands.Count(cmd => cmd.Identifier == ident) > 0 ? Commands.First(cmd => cmd.Identifier == ident) : null;
        }

        public IParameterProvider<IParameter> GetTypeDescriptionByName(string name)
        {
            return Types.Count(parameter => parameter.Name == name) > 0 ? Types.First(parameter => parameter.Name == name) : null;
        }

        public ISectionProvider<ICommand> GetSectionProviderByName(string name)
        {
            return Sections.Count(section => section.Name == name) > 0
                ? Sections.First(section => section.Name == name)
                : null;
        }
    }
}
