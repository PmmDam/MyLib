using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Security
{
    public class SymmetricCryptographyManager
    {
        public static byte[] GenerateKey(string password, int saltLength, int keyLength)
        {
            Rfc2898DeriveBytes derivator = new Rfc2898DeriveBytes(password, saltLength);
            byte[] key = null;
            try
            {
                key = derivator.GetBytes(keyLength);
            }
            finally
            {
                if (derivator != null)
                {
                    derivator.Dispose();
                    derivator = null;
                }
            }
            return key;
        }
        public static byte[] GenerateKey()
        {
            byte[] key;
            using (Aes aesAlgorithm = Aes.Create())
            {
                key = aesAlgorithm.Key;
            }
            return key;
        }

        public static byte[] GenerateIV()
        {
            byte[] iv;
            using (Aes aesAlgorithm = Aes.Create())
            {
                iv = aesAlgorithm.IV;
            }
            return iv;
        }

        public static byte[] Encrypt(string inputMessage, byte[] iv, byte[] key)
        {
            byte[] encrypted;
            Aes aesAlgorithm = Aes.Create();
            try
            {
                aesAlgorithm.Key = key;
                aesAlgorithm.IV = iv;
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();
                try
                {
                    MemoryStream memoryPipeline = new MemoryStream();
                    try
                    {
                        CryptoStream cryptoPipeline = new CryptoStream(memoryPipeline, encryptor,
                         CryptoStreamMode.Write);
                        try
                        {
                            StreamWriter pipeLineWriter = new StreamWriter(cryptoPipeline);
                            try
                            {
                                pipeLineWriter.Write(inputMessage);
                                pipeLineWriter.Flush();
                                cryptoPipeline.FlushFinalBlock();
                                encrypted = memoryPipeline.ToArray();
                            }
                            finally
                            {
                                if (pipeLineWriter != null)
                                {
                                    pipeLineWriter.Dispose();
                                    pipeLineWriter = null;
                                    cryptoPipeline = null;
                                    memoryPipeline = null;
                                }
                            }
                        }
                        finally
                        {
                            if (cryptoPipeline != null)
                            {
                                cryptoPipeline.Dispose();
                                cryptoPipeline = null;
                                memoryPipeline = null;
                            }
                        }
                    }
                    finally
                    {
                        if (memoryPipeline != null)
                        {
                            memoryPipeline.Dispose();
                            memoryPipeline = null;
                        }
                    }
                }
                finally
                {
                    if (encryptor != null)
                    {
                        encryptor.Dispose();
                        encryptor = null;
                    }
                }
            }
            finally
            {
                if (aesAlgorithm != null)
                {
                    aesAlgorithm.Dispose();
                    aesAlgorithm = null;
                }
            }
            return encrypted;
        }

        public static string Decrypt(byte[] encryptedMessage, byte[] iv, byte[] key)
        {
            string decryptedMessage = null;
            Aes aesAlgorithm = Aes.Create();
            try
            {
                aesAlgorithm.Key = key;
                aesAlgorithm.IV = iv;
                ICryptoTransform encryptor = aesAlgorithm.CreateDecryptor();
                try
                {
                    MemoryStream memoryPipeline = new MemoryStream(encryptedMessage);
                    try
                    {
                        CryptoStream cryptoPipeline = new CryptoStream(memoryPipeline, encryptor,
                         CryptoStreamMode.Read);
                        try
                        {
                            StreamReader pipeLineReader = new StreamReader(cryptoPipeline);
                            try
                            {
                                decryptedMessage = pipeLineReader.ReadToEnd();
                            }
                            finally
                            {
                                if (pipeLineReader != null)
                                {
                                    pipeLineReader.Dispose();
                                    pipeLineReader = null;
                                    cryptoPipeline = null;
                                    memoryPipeline = null;
                                }
                            }
                        }
                        finally
                        {
                            if (cryptoPipeline != null)
                            {
                                cryptoPipeline.Dispose();
                                cryptoPipeline = null;
                                memoryPipeline = null;
                            }
                        }
                    }
                    finally
                    {
                        if (memoryPipeline != null)
                        {
                            memoryPipeline.Dispose();
                            memoryPipeline = null;
                        }
                    }
                }
                finally
                {
                    if (encryptor != null)
                    {
                        encryptor.Dispose();
                        encryptor = null;
                    }
                }
            }
            finally
            {
                if (aesAlgorithm != null)
                {
                    aesAlgorithm.Dispose();
                    aesAlgorithm = null;
                }
            }
            return decryptedMessage;
        }
    }
}
