using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MoneyWallet
{
    internal class UserService
    {
        private readonly JsonFileService fileService =new JsonFileService();
        private List<User> users;
        public UserService()
        {
            users = fileService.Read<User>("users.json");
        }

        // Create password salt
        private string CreateSalt()
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            return Convert.ToBase64String(salt);
        }

        // Hash password
        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password,saltBytes,600000,HashAlgorithmName.SHA256,32);
            return Convert.ToBase64String(hash);
        }

        // Verify password
        private bool VerifyPassword(string password, string salt, string savedHash)
        {
            byte[] enteredHash = Convert.FromBase64String(HashPassword(password, salt));
            byte[] storedHash = Convert.FromBase64String(savedHash);
            return CryptographicOperations.FixedTimeEquals(enteredHash, storedHash);
        }

        // Register user
        public bool Register(string username,string email,string password,out string message)
        {
            username = username.Trim();
            email = email.Trim();
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                message = "All fields are required.";
                return false;
            }
            else if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                message = "Enter a valid email address.";
                return false;
            }
            else if (password.Length < 8)
            {
                message = "Password must contain at least 8 characters.";
                return false;
            }
            else if (users.Any(u =>
                u.email.Equals(email, StringComparison.OrdinalIgnoreCase)))
            {
                message = "Email already registered.";
                return false;
            }
            else
            {
                int newId = users.Count == 0 ? 1:users.Max(u => u.uid) + 1;
                string salt = CreateSalt();
                string hash = HashPassword(password, salt);
                User newUser = new User
                {
                    uid = newId,username = username,email = email,passwordHash = hash,passwordSalt = salt
                };
                users.Add(newUser);
                fileService.Write("users.json", users);
                message = "Registration successful!";
                return true;
            }
        }

        // Login user
        public User Login(string email,string password,out string message)
        {
            email = email.Trim();
            User user = users.FirstOrDefault(u => u.email.Equals(email,StringComparison.OrdinalIgnoreCase));
            if (user == null)
            {
                message = "Invalid email or password.";
                return null;
            }
            else if (!VerifyPassword(password,user.passwordSalt,user.passwordHash))
            {
                message = "Invalid email or password.";
                return null;
            }
            else
            {
                message = "Login successful!";
                return user;
            }
        }
    }
}
