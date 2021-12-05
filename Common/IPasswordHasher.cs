namespace HRApp.Common
{
    public interface IPasswordHasher
    {
        byte[] ComputeSalt();

        byte[] ComputeHash(string plainText, byte[] salt);

        bool Compare(byte[] b1, byte[] b2);
    }
}
