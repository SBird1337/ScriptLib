using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptLib.Scripts.Parameter;

namespace ScriptLib.Scripts
{
    public class StructureCommandProvider : ICommandProvider<StructureCommand>
    {
        //public List<IParameterProvider<IParameter>> Types { get; set; }
        public List<string> TypeNames { get; set; }
        public ScriptDatabase Database { get; set; }

        public StructureCommandProvider(string typeFile, ScriptDatabase database)
        {
            Database = database;
            TypeNames = new List<string>();
            using (StringReader reader = new StringReader(File.ReadAllText(typeFile)))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    TypeNames.Add(line);
                    //Types.Add(Database.GetTypeDescriptionByName(line));
                }
            }
        }

        public StructureCommand ReadCommand(BinaryReader reader, Script parent)
        {
            StructureCommand cmd = new StructureCommand(this);
            foreach (IParameterProvider<IParameter> type in TypeNames.Select(name => Database.GetTypeDescriptionByName(name)))
            {
                cmd.Types.Add(type.Create(reader));
            }
            return cmd;
        }
    }
}
