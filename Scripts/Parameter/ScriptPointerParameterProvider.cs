using System;
using System.IO;
using ScriptLib.Parser;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts.Parameter
{
    public class ScriptPointerParameterProvider : IParameterProvider<ScriptPointerParameter>
    {
        private readonly ISectionProvider<ICommand> _provider;
        //Needs Section Provider etc. to create sections
        //TODO change for pointer:section

        public string SectionName { get; set; }
        
        
        public ScriptPointerParameterProvider(string sectionName, ScriptDatabase database)
        {
            SectionName = sectionName;
            //init provider
            _provider = database.GetSectionProviderByName(sectionName);
        }

        public string Name
        {
            get { return "pointer:" + _provider.Name; }
        }

        public int Length
        {
            get { return 4; }
        }

        public ScriptPointerParameter Create(BinaryReader reader)
        {
            return new ScriptPointerParameter(reader.ReadUInt32() & 0x1FFFFFF, _provider, this);
        }

        public string ToString(IDeparser deparser, IParameter parameter)
        {
            return deparser.DeparseWord(BitConverter.ToUInt32(parameter.Value, 0));
        }
    }
}
