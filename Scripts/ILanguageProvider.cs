namespace ScriptLib.Scripts
{
    public interface ILanguageProvider
    {
        ScriptDatabase Database { get; set; }
        void PreInitialize();
        void PostInitialize();
        void Initialize();
        byte[] EndIdentifier { get; }
        
    }
}
