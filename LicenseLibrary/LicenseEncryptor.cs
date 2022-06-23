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
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public LicenseEncryptor()
        {
            _privateKey = "MIIBOQIBAAJAeglubMM6GJ8k2fPEAq1j3HAmjbBWx56B1cfFo+bmyhzo3cn8xcuIhV9KpxFU5iVDj+X4+5WXFNHo16HPygsCQQIDAQABAkBLqEkzWJ1N4mwAS2X7mu9MHKNqOCa0vwoTNTTMdjwilIhd4veVqTpsoWp1OjW4d4kC7Q89slYkugEV6MBhC+9ZAiEAy2v4Ysqr8XMrVDV7xCZLueci4GAiY0s5ZlUXN2sWgmcCIQCZlGBXoUT3OL1MwpUn/RkNflUSNqcvQcAXpkfEej99FwIgdnug5QnfNHc8WYP9XrZfjRxPeBkGboc2G6CcMS8yoSkCIGqbwejyjMIkQ9ul8w4oNhzUxk73W0SFmseP6J+t0KaPAiEAkAD54e0X843WmiVKqsC9+HrRKiT43QV2RKkk8nT7X1k=";

            _privateKey = _cryptoServiceProvider.ExportParameters(true);
            _publicKey = _cryptoServiceProvider.ExportParameters(false);
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
