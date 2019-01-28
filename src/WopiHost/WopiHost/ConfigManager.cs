using System;
using System.IO;
using System.Runtime.Serialization.Json;
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
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                var dcjs = new DataContractJsonSerializer(typeof(Config));
                var obj = dcjs.ReadObject(ms) as Config;

                config = obj;
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
    }
}
