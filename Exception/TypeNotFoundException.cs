namespace ScriptLib.Exception
{
    public class TypeNotFoundException : System.Exception
    {
        public TypeNotFoundException(string name) : base(string.Format("The specified type {0} was not found.", name))
        {
        }
    }
}
