using System.Security.Cryptography;
using System.Text;

namespace MultiReservas.Extensions
{
    public static class CriptografiaSHA256
    {
        public static string Criptografar(string senha)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senha + "7D@84fihq@BH12DBd"));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}