﻿using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AutoVersionsDB.Helpers
{
    public class FileChecksum : IDisposable
    {

        private readonly SHA512 _hashEncryptor;

        public FileChecksum()
        {
            _hashEncryptor = SHA512.Create();
        }


        public string GetHashByFilePath(string fileFullPath)
        {
            string fileContentStr = File.ReadAllText(fileFullPath);
            return GetHash(fileContentStr);
        }


        public string GetHash(string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = _hashEncryptor.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2", CultureInfo.InvariantCulture));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public bool VerifyHash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetHash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FileChecksum()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources

                _hashEncryptor.Dispose();
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
