using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnetionDetect
{
    public class Config
    {
        /// <summary>
        /// 連線的IP
        /// </summary>
        [JsonProperty(PropertyName = "IP")]
        public string IP { set; get; }

        /// <summary>
        /// 偵測的頻率(毫秒)
        /// </summary>
        [JsonProperty(PropertyName = "Frequency")]
        public int Frequency { set; get; }
    }
}

