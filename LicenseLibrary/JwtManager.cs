using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace LicenseLibrary
{
    class JwtManager
    {
        public string CreateToken(List<Claim> claims, string privateRsaKey)
        {
            RSAParameters rsaParams;
            using (var stringReader = new StringReader(privateRsaKey))
            {
                var pemReader = new PemReader(stringReader);
                var privateRsaParams = pemReader.ReadObject() as RsaPrivateCrtKeyParameters;
                if (privateRsaParams == null)
                {
                    throw new Exception("Could not read RSA private key");
                }
                rsaParams = DotNetUtilities.ToRSAParameters(privateRsaParams);
            }
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
                Dictionary<string, object> payload = claims.ToDictionary(k => k.Type, v => (object)v.Value);
                return Jose.JWT.Encode(payload, rsa, Jose.JwsAlgorithm.RS512);
            }
        }

        public string DecodeToken(string token, string publicRsaKey)
        {
            RSAParameters rsaParams;

            using (var tr = new StringReader(publicRsaKey))
            {
                var pemReader = new PemReader(tr);
                var publicKeyParams = pemReader.ReadObject() as RsaKeyParameters;
                if (publicKeyParams == null)
                {
                    throw new Exception("Could not read RSA public key");
                }
                rsaParams = DotNetUtilities.ToRSAParameters(publicKeyParams);
            }
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(rsaParams);
                // This will throw if the signature is invalid
                return Jose.JWT.Decode(token, rsa, Jose.JwsAlgorithm.RS512);
            }
        }
    }
}
