using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace LicenseLibrary
{
    class LicenseEncryptor
    {
        private static RSACryptoServiceProvider _cryptoServiceProvider = new RSACryptoServiceProvider();
        private KeyManager _keyManager = new KeyManager();
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public LicenseEncryptor()
        {
            _privateKey = _keyManager.GetPrivateKey();
            _publicKey = _keyManager.GetPublicKey();
        }

        public string GetPublicKey()
        {
            var stringWriter = new StringWriter();
            var xmlSerializer = new XmlSerializer(typeof(RSAParameters));
            xmlSerializer.Serialize(stringWriter, _publicKey);
            return stringWriter.ToString();
        }

        public string Encrypt(string plainText)
        {
            _cryptoServiceProvider = new RSACryptoServiceProvider();
            _cryptoServiceProvider.ImportParameters(_publicKey);
            var data = Encoding.Unicode.GetBytes(plainText);
            var cypherText = _cryptoServiceProvider.Encrypt(data, false);
            return Convert.ToBase64String(cypherText);
        }

        public string Decrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            _cryptoServiceProvider.ImportParameters(_privateKey);
            var plainText = _cryptoServiceProvider.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plainText);
        }
    }
}
