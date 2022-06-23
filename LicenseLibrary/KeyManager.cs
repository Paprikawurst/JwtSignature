using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LicenseLibrary
{
    class KeyManager
    {
        string privateKey = "MIIBOQIBAAJAeglubMM6GJ8k2fPEAq1j3HAmjbBWx56B1cfFo+bmyhzo3cn8xcuIhV9KpxFU5iVDj+X4+5WXFNHo16HPygsCQQIDAQABAkBLqEkzWJ1N4mwAS2X7mu9MHKNqOCa0vwoTNTTMdjwilIhd4veVqTpsoWp1OjW4d4kC7Q89slYkugEV6MBhC+9ZAiEAy2v4Ysqr8XMrVDV7xCZLueci4GAiY0s5ZlUXN2sWgmcCIQCZlGBXoUT3OL1MwpUn/RkNflUSNqcvQcAXpkfEej99FwIgdnug5QnfNHc8WYP9XrZfjRxPeBkGboc2G6CcMS8yoSkCIGqbwejyjMIkQ9ul8w4oNhzUxk73W0SFmseP6J+t0KaPAiEAkAD54e0X843WmiVKqsC9+HrRKiT43QV2RKkk8nT7X1k=";
        string publicKey = "MFswDQYJKoZIhvcNAQEBBQADSgAwRwJAeglubMM6GJ8k2fPEAq1j3HAmjbBWx56B1cfFo+bmyhzo3cn8xcuIhV9KpxFU5iVDj+X4+5WXFNHo16HPygsCQQIDAQAB";

        public RSAParameters GetPublicKey()
        {
            return KeyToRsaParam(publicKey);
        }

        public RSAParameters GetPrivateKey()
        {
            return KeyToRsaParam(privateKey);
        }

        public RSAParameters KeyToRsaParam(string keyString)
        {
            //get a stream from the string
            var sr = new System.IO.StringReader(keyString);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream
            var key = (RSAParameters)xs.Deserialize(sr);
            return key;
        }

    }
}
