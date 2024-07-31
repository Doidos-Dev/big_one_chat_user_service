using System.Security.Cryptography;
using System.Text;

namespace Application.Security.Implementations
{
    public class CriptographyAss : ICriptographyAss
    {
        public string Decrypt(string encryptedMessage, string privateKey)
        {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
            var decryptedData = rsa.Decrypt(Convert.FromBase64String(encryptedMessage), RSAEncryptionPadding.Pkcs1);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public string Encrypt(string message, string publicKey)
        {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            var encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(message), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedData);
        }
    }
}
