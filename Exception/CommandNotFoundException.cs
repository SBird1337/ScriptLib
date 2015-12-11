namespace ScriptLib.Exception
{
    public class CommandNotFoundException : System.Exception
    {
        public CommandNotFoundException(string name) 
            : base("Command with name " + name + " was not found.")
        {
        }

        public CommandNotFoundException(byte ident)
            : base("Command with identifier 0x" + ident.ToString("X") + " was not found.")
        {

        }
    }
}
