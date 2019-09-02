using Jose;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCF.App_Code
{
    public class Token
    {
        public string GetToken()
        {
            byte[] secretKey = Base64UrlDecode("PNbPIGl6lIDSB-sI6fc2zE1PxOUmfXP4uMyei0VmeXVgP-Zbsi-ISlVaPYVCi5eh");
            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Now.AddHours(10);

            var payload = new Dictionary<string, object>()
            {
                {"iss", "http://www.1zentinela1.com/BO/User/Login/"},
                {"aud", "gXHQpVE7cPgxBORm7b7wRXc1E1e2pLu4"},
                {"sub", "anonymous"},
                {"iat", ToUnixTime(issued).ToString()},
                {"exp", ToUnixTime(expire).ToString()}
            };
            try
            {
                string token = JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
                string tokenDecode = JWT.Decode(token, secretKey);
                JObject jsonToken = JObject.Parse(tokenDecode);
                return token;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public byte[] Base64UrlDecode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default:
                    throw new System.Exception(
             "Illegal base64url string!");
            }
            return Convert.FromBase64String(s); // Standard base64 decoder
        }

        public int ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public Dictionary<string, object> GetPayload()
        {
            DateTime issued = DateTime.Now;
            DateTime expire = DateTime.Now.AddHours(2);
            Dictionary<string, object> payload = new Dictionary<string, object>()
            {
                {"iss", "http://www.1zentinela1.com/BO/User/Login/"},
                {"aud", "gXHQpVE7cPgxBORm7b7wRXc1E1e2pLu4"},
                {"sub", "anonymous"},
                {"iat", ToUnixTime(issued).ToString()},
                {"exp", ToUnixTime(expire).ToString()}
            };

            return payload;
        }
    }
}