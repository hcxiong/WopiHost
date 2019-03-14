using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WopiHost
{
    public class ConfigManager
    {
        public class Config
        {
            private string _url = "http://localhost:800/";
            public string url
            {
                get
                {
                    return _url.TrimEnd('/') + "/";
                }
                set => _url = value;
            }
            public string root { get; set; }
            public string login { get; set; } = "netnr";
            public string name { get; set; } = "netnr";
            public string mail { get; set; } = "netnr@netnr.com";
        }

        public Config config { get; set; } = new Config();

        public ConfigManager()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + "/config.json";
            var json = ReadText(path);
            if (!string.IsNullOrWhiteSpace(json))
            {
                var jo = JObject.Parse(json);
                var pis = config.GetType().GetProperties();
                foreach (var pi in pis)
                {
                    if (jo.ContainsKey(pi.Name))
                    {
                        pi.SetValue(config, jo[pi.Name].ToString(), null);
                    }
                }
            }
        }

        public static string ReadText(string path, string fileName = "", Encoding e = null)
        {
            string result = string.Empty;

            try
            {
                if (e == null)
                {
                    e = Encoding.UTF8;
                }

                using (var sr = new StreamReader(path + fileName, Encoding.Default))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }
            return result;
        }

        public static string MD5(string s, int len = 32)
        {
            string result = "";

            var md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            result = sb.ToString();

            return len == 32 ? result : result.Substring(8, 16);
        }
    }
}