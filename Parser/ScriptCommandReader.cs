using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptLib.Exception;
using ScriptLib.Script;
using ScriptLib.Script.Parameter;

namespace ScriptLib.Parser
{
    public class ScriptCommandReader : ICommandReader<ScriptCommand>
    {
        private readonly ILanguageProvider _provider;
        public ScriptCommandReader(ILanguageProvider provider)
        {
            _provider = provider;
        }

        public ScriptCommand ReadCommand(BinaryReader reader, bool refractor = false)
        {
            byte ident = reader.ReadByte();
            CommandDescription description = _provider.Database.GetCommandDescriptionByIdent(ident);
            if (description == null)
                throw new CommandNotFoundException(ident);
            ScriptCommand cmd = new ScriptCommand(ident, description);
            foreach (string parameter in description.Parameters)
            {
                IParameterProvider<IParameter> typeProvider = _provider.Database.GeTypeDescriptionByName(parameter);
                if (typeProvider == null)
                    throw new TypeNotFoundException(parameter);
                switch (typeProvider.Length)
                {
                    case 1:
                        cmd.Parameters.Add(typeProvider.Create(reader.ReadBytes(1)));
                        break;
                    case 2:
                        cmd.Parameters.Add(typeProvider.Create(reader.ReadBytes(2)));
                        break;
                    case 4:
                        cmd.Parameters.Add(typeProvider.Create(reader.ReadBytes(4)));
                        break;
                }
            }
            return cmd;
        }

        public IEnumerable<ScriptCommand> ReadCommandsToEnd(BinaryReader reader, bool refractor = false)
        {
            ScriptCommand currentCommand;
            do
            {
                currentCommand = ReadCommand(reader, refractor);
                yield return currentCommand;
            } while (currentCommand.Description.Identifier != _provider.EndIdentifier);
        }
    }
}
