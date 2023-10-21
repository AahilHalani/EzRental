using System.Security.Cryptography;

namespace EzRental.Services
{
    public class PasswordHasher
    {
        private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8 ;
        private const int iterations = 10000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;

        public string[] Hash(string password)
        {   
            //creates a byte array
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, KeySize);

            return new string[] { Convert.ToBase64String(hash), Convert.ToBase64String(salt) };
        }

        public bool Verify(string passwordHash, string inputPassword, string salt)
        {   
            try {
                var passwordHashByte = Convert.FromBase64String(passwordHash);
                
                var saltHashByte = Convert.FromBase64String(salt);
                
                var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, saltHashByte, iterations, 
                    hashAlgorithm, KeySize);
                
                return CryptographicOperations.FixedTimeEquals(passwordHashByte, hashInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    
    }
}
