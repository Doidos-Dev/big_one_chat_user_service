namespace Application.Security
{
    public interface ICriptographyAss
    {
        string Encrypt(string message, string publicKey);
        string Decrypt(string encryptedMessage, string privateKey);
    }
}
