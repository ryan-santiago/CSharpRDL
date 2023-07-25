using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CSharpRDL.ViewModel
{
    public class HashPassword
    {
        public string Password(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password to bytes
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Compute the hash
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Convert the hash to a string (hexadecimal format)
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}