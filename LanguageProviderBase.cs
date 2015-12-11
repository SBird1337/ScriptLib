using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptLib.Scripts;

namespace ScriptLib
{
    public abstract class LanguageProviderBase : ILanguageProvider
    {
        protected LanguageProviderBase()
        {
            DoInitialize();
        }

        private void DoInitialize()
        {
            PreInitialize();
            Initialize();
            PostInitialize();
        }

        public abstract string CommandFile { get; }

        public ScriptDatabase Database { get; set; }

        public virtual void PreInitialize()
        {
            Database = new ScriptDatabase(CommandFile);
        }

        public abstract void PostInitialize();

        public abstract void Initialize();

        public abstract byte[] EndIdentifier { get; }
    }
}
