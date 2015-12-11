using System.Globalization;

namespace ScriptLib.Extensions
{
    public static class StringExtensions
    {
        public static bool TryParse(this string input, out byte output)
        {
            if (input.StartsWith("0x"))
                return byte.TryParse(input.Remove(0,2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out output);
            return byte.TryParse(input, out output);
        }

        public static bool TryParse(this string input, out uint output)
        {
            if (input.StartsWith("0x"))
                return uint.TryParse(input.Remove(0, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out output);
            return uint.TryParse(input, out output);
        }
    }
}
