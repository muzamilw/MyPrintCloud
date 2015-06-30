using System.Text;

namespace MPC.Models.Common
{
    public static class StringHelper
    {
        /// <summary>
        /// Removes special characters and gives simple string
        /// </summary>
        public static string SimplifyString(string stringWithSpecialChars)
        {

            // ReSharper disable once SuggestUseVarKeywordEvident
            StringBuilder sb = new StringBuilder();
            // Set as Empty string in case of null
            stringWithSpecialChars = stringWithSpecialChars ?? string.Empty;
            foreach (char c in stringWithSpecialChars)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == '-')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
