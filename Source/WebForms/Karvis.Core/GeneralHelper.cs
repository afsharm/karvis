using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using System.Web;
using System.Configuration;
using System.ServiceModel;

namespace Karvis.Core
{
    public class GeneralHelper
    {
        public static string GetAppUrlPure()
        {
            //HttpContext is null when this method is called in a WCF service

            if (HttpContext.Current != null)
                return HttpContext.Current.Request.Url.Authority;
            else
                return OperationContext.Current.Host.BaseAddresses[0].Authority;
        }

        public static string GetAppUrl()
        {
            return string.Format("http://{0}/", GetAppUrlPure());
        }

        public static string GetAppEmail()
        {
            return ConfigurationManager.AppSettings["email"];
        }

        public static IOrderedEnumerable<KeyValuePair<string, ushort>> AnalyseTags(IList<string> rawTags)
        {
            var result = new SortedDictionary<string, UInt16>(StringComparer.OrdinalIgnoreCase);

            foreach (string tagList in rawTags)
            {
                foreach (string tag in tagList.Split(','))
                {
                    string trimmedTag = tag.Trim();

                    if (!result.ContainsKey(trimmedTag))
                        result.Add(trimmedTag, 0);

                    result[trimmedTag]++;
                }
            }

            return result.OrderByDescending(x => x.Value);
        }



        static void EncryptPassword()
        {
            const string encryptionKey = "AE09F72BA97CBBB5";
            string password = "123456";

            var hash = new HMACSHA1();
            hash.Key = HexToByte(encryptionKey);
            string encodedPassword =
                Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));

            Console.WriteLine(encodedPassword);
            Console.ReadLine();
        }

        private static byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
