using System;

namespace LicenseLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var licenseEncryptor = new LicenseEncryptor();
            var testText = "Hallo Welt";

            testText = licenseEncryptor.Encrypt(testText);
            Console.WriteLine("---------\n" + testText);
            testText = licenseEncryptor.Decrypt(testText);
            Console.Write("----------\n" + testText);



            Console.ReadLine();
        }
    }
}
