using System.Text;

namespace NeyeTech.ZeroCarbon.Core.Extensions
{
    public static class GeneratorExtensions
    {
        public static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder randomCode = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomCode.Append(chars[random.Next(chars.Length)]);
            }

            return randomCode.ToString();
        }
    }
}
