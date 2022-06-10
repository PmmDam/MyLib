using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Security
{
    public class AsymmetricCryptographyManager
    {
        #region Asymmetric Cryptography
        /// <summary>
        /// Generate private and public Keys
        /// </summary>
        /// <param name="includePrivateKey"></param>
        /// <returns></returns>
        public static string GenerateKeys(bool includePrivateKey)
        {
            string xmlKeys = string.Empty;
            using (RSACryptoServiceProvider rsaAlgorithm = new RSACryptoServiceProvider())
            {
                xmlKeys = rsaAlgorithm.ToXmlString(includePrivateKey);

            }
            return xmlKeys;
        }
        /// <summary>
        /// Encrypt a message with xmlKeys which contains the receptor public key
        /// </summary>
        /// <param name="clearBytes"></param>
        /// <param name="xmlKeys"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clearBytes, string xmlKeys)
        {
            byte[] encryptedBytes = null;
            using (RSA rsaAlg = RSA.Create())
            {
                rsaAlg.FromXmlString(xmlKeys);
                encryptedBytes = rsaAlg.Encrypt(clearBytes, RSAEncryptionPadding.Pkcs1);
            }
            return encryptedBytes;
        }
        /// <summary>
        /// Decrypt an encrypted message with the receptor private key
        /// </summary>
        /// <param name="encryptedBytes"></param>
        /// <param name="xmlKeys"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] encryptedBytes, string xmlKeys)
        {
            byte[] decryptedBytes = null;
            using (RSA rsaAlgorithm = RSA.Create())
            {
                rsaAlgorithm.FromXmlString(xmlKeys);
                decryptedBytes = rsaAlgorithm.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
            }
            return decryptedBytes;
        }

        #endregion Asymmetric Cryptography

        #region DigitalSignature
        /// <summary>
        /// Sign a message with the rece0...............
        /// </summary>
        /// <param name="message"></param>
        /// <param name="xmlKeys"></param>
        /// <returns></returns>
        public static Byte[] GetDigitalSignature(String message, String xmlKeys)
        {
            Byte[] signedBytes = null;
            SHA512 sha512Algorithm = SHA512.Create();
            try
            {
                RSACryptoServiceProvider rsaAlgorithm = new RSACryptoServiceProvider();
                try
                {
                    rsaAlgorithm.FromXmlString(xmlKeys);
                    RSAPKCS1SignatureFormatter formatter = new
                    RSAPKCS1SignatureFormatter(rsaAlgorithm);
                    formatter.SetHashAlgorithm("SHA512");
                    Byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    Byte[] hashedBytes = sha512Algorithm.ComputeHash(messageBytes);
                    signedBytes = formatter.CreateSignature(hashedBytes);
                }
                finally
                {
                    if (rsaAlgorithm != null)
                    {
                        rsaAlgorithm.Dispose();
                        rsaAlgorithm = null;
                    }
                }
            }
            finally
            {
                if (sha512Algorithm != null)
                {
                    sha512Algorithm.Dispose();
                    sha512Algorithm = null;
                }
            }
            return signedBytes;
        }

        public static bool IsValidSignature(string message, byte[] digitalSignature, string xmlPublicKey)
        {
            bool isValid = false;
            SHA512 sha512Algorithm = SHA512.Create();
            try
            {
                RSACryptoServiceProvider rsaAlgorithm = new RSACryptoServiceProvider();
                try
                {
                    Byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    Byte[] hashedBytes = sha512Algorithm.ComputeHash(messageBytes);
                    rsaAlgorithm.FromXmlString(xmlPublicKey);
                    RSAPKCS1SignatureDeformatter deformatter = new
                    RSAPKCS1SignatureDeformatter(rsaAlgorithm);
                    deformatter.SetHashAlgorithm("SHA512");
                    if (deformatter.VerifySignature(hashedBytes, digitalSignature))
                    {
                        isValid = true;
                    }
                }
                finally
                {
                    if (rsaAlgorithm != null)
                    {
                        rsaAlgorithm.Dispose();
                        rsaAlgorithm = null;
                    }
                }
            }
            finally
            {
                if (sha512Algorithm != null)
                {
                    sha512Algorithm.Dispose();
                    sha512Algorithm = null;
                }
            }
            return isValid;
        }

        #endregion DigitalSignature
    }
}
