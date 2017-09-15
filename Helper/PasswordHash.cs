using System;
using System.Security.Cryptography;
using System.Text;

namespace MailGenerator.Helper
{
    public class PasswordHash
    {
        public string GetSha1HashData(string data)
        {
            try
            {
                //create new instance of md5
                SHA1 sha1 = SHA1.Create();

                //convert the input text to array of bytes
                byte[] hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));

                //create new instance of StringBuilder to save hashed data
                StringBuilder returnValue = new StringBuilder();

                //loop for each byte and add it to StringBuilder
                for (int i = 0; i < hashData.Length; i++)
                {
                    returnValue.Append(i.ToString());
                }

                // return hexadecimal string
                return returnValue.ToString();
            }
            catch (Exception ex)
            {
                Logging.LogException(ex);
                return null;
            }
        }
    }
}