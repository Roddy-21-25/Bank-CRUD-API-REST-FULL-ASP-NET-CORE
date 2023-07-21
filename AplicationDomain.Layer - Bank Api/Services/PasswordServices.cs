using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace AplicationDomain.Layer___Bank_Api.Services
{
    public class PasswordServices : IPasswordHasher
    {
        private readonly PasswordOptionsValues _passwordOptionsValues;

        public PasswordServices(IOptions<PasswordOptionsValues> passwordOptionsValues)
        {
            _passwordOptionsValues = passwordOptionsValues.Value;
        }

        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using (var algorithm = new Rfc2898DeriveBytes(
                password, salt, iterations))
            {
                var keyToCheck = algorithm.GetBytes(_passwordOptionsValues.KeySize);
                return keyToCheck.SequenceEqual(key);
            };
        }

        public string Hash(string password)
        {
            using(var algorithm = new Rfc2898DeriveBytes(
                password,
                _passwordOptionsValues.SaltSize,
                _passwordOptionsValues.Iterations)) {

                var key = Convert.ToBase64String(algorithm.GetBytes(_passwordOptionsValues.KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{_passwordOptionsValues.Iterations}.{salt}.{key}";
            }
        }
    }
}
