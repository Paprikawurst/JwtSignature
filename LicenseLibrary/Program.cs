using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace LicenseLibrary
{
    class Program
    {
        static void Main()
        {
            string publicKey = File.ReadAllText(@"C:\keys\public.pem");
            string privateKey = File.ReadAllText(@"C:\keys\private.pem");

            var claims = new List<Claim>();
            claims.Add(new Claim("test1", "test2"));

            var jwtManager = new JwtManager();
            var token = jwtManager.CreateToken(claims, privateKey);
            Console.WriteLine("---------\nEncoded Token: " + token);

            var payload = jwtManager.DecodeToken(token, publicKey);
            Console.WriteLine("---------\nDecoded Token: " + payload);

            Console.ReadLine();
        }
    }
}
