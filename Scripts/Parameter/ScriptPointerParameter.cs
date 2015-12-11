using System;
using System.IO;
using ScriptLib.Parser;
using ScriptLib.Scripts.Section;

namespace ScriptLib.Scripts.Parameter
{
    public class ScriptPointerParameter : IParameter
    {
        #region Fields
        private uint _offset;
        private string _dynamicName;
        private ISection<ICommand> _section; 
        #endregion

        //Do not forget to add section

        #region Constructors
        public ScriptPointerParameter(uint offset, ISectionProvider<ICommand> sectionProvider, IParameterProvider<ScriptPointerParameter> paraProvider)
            : this(sectionProvider, paraProvider)
        {
            Offset = offset;
        }

        public ScriptPointerParameter(string dynamicName, ISectionProvider<ICommand> sectionProvider, IParameterProvider<ScriptPointerParameter> paraProvider)
            : this(sectionProvider, paraProvider)
        {
            DynamicName = dynamicName;
        }

        private ScriptPointerParameter(ISectionProvider<ICommand> provider, IParameterProvider<ScriptPointerParameter>  paraProvider)
        {
            SectionProvider = provider;
            Provider = paraProvider;
        }
        #endregion

        #region Properties

        public uint Offset
        {
            get
            {
                if (!IsDynamic)
                    return _offset;
                throw new NotSupportedException("The parameter is dynamic.");
            }
            set
            {
                _offset = value;
                DynamicName = string.Format("0x{0}", _offset.ToString("X"));
                IsDynamic = false;
                //Parameter is no longer dynamic
            }
        }

        public ISectionProvider<ICommand> SectionProvider { get; private set; }

        public string DynamicName
        {
            get { return _dynamicName; }
            set
            {
                _dynamicName = value;
                IsDynamic = true;
            }
        }

        public bool IsDynamic { get; private set; }

        #endregion

        #region IParameter Member
        public byte[] Value
        {
            get { return BitConverter.GetBytes(Offset | 0x08000000); }
        }

        public int Length
        {
            get { return Value.Length; }
        }

        public IParameterProvider<IParameter> Provider { get; set; }
        public string ToString(IDeparser deparser)
        {
            return Provider.ToString(deparser, this);
        }

        #endregion

        #region Methods

        public ISection<ICommand> CreateSection(BinaryReader reader, Script parent)
        {
            reader.BaseStream.Position = Offset;
            _section = SectionProvider.Create(IsDynamic ? DynamicName : string.Format("0x{0}", Offset.ToString("X")), reader, parent);
            _section.IsGlobal = true;
            _section.OnLabelNameChanged += _section_OnLabelNameChanged;
            return _section;
        }

        void _section_OnLabelNameChanged(object sender, string e)
        {
            DynamicName = e;
        }
        #endregion
    }
}
