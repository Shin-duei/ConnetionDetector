using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConnetionDetect
{
    public class DataAccess
    {
        public DataAccess()
        {
            Config config = ReadConfiguration();

            if (config == null)
                System.Environment.Exit(0);


            Frequency = config.Frequency;
            IP = config.IP;
        }

        public static string IP;
        public static int Frequency;

        private const string ConfigFileName = "Config.dat";

        /// <summary>
        /// 讀取配置
        /// </summary>
        /// <returns></returns>
        private Config ReadConfiguration()
        {
            if (File.Exists(ConfigFileName))
            {
                using (StreamReader r = new StreamReader(ConfigFileName))
                {
                    string jsonString = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<Config>(jsonString);//反序列化
                }
            }
            else
            {
                return MigrationConfig();
            }

        }
        /// <summary>
        /// 配置檔自動生成
        /// </summary>
        /// <returns></returns>
        private Config MigrationConfig()
        {
            var config = new Config
            {
                IP = "1.171.214.239",
                Frequency = 5000
            };

            using (StreamWriter sw = new StreamWriter(ConfigFileName))
            {
                try
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.NullValueHandling = NullValueHandling.Ignore;

                    //構建Json.net的寫入流  
                    JsonWriter writer = new JsonTextWriter(sw)
                    {
                        Formatting = Formatting.Indented,//格式化缩进
                        Indentation = 4,  //缩进四个字符
                        IndentChar = ' '  //缩进的字符是空格
                    }; ;
                    //把模型數據序列化並寫入Json.net的JsonWriter流中  
                    serializer.Serialize(writer, config);
                    //ser.Serialize(writer, ht);  
                    writer.Close();
                    sw.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "提示"); ex.Message.ToString();
                }
            }
            return config;
        }
    }
}
