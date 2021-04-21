using ConnetionDetect.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConnetionDetect
{
    public class MainViewModel : ViewModelBase
    {

        public string HostIP
        {
            get { return hostIP; }
            set
            {
                hostIP = value;
                this.OnPropertyChanged();
            }
        }
        private string hostIP = "1.171.102.109";


        private static int Second = 0;
        private static int Minuite = 0;
        private static int Hour = 0;
        

        public MainViewModel()
        {
            new DataAccess();
            HostIP =DataAccess.IP;
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000; //執行間隔時間,單位為毫秒; 
            timer.Start();    
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer);

            System.Timers.Timer DetectFrequency = new System.Timers.Timer();
            DetectFrequency.Enabled = true;
            DetectFrequency.Interval = DataAccess.Frequency; //執行間隔時間,單位為毫秒; 
            DetectFrequency.Start();
            DetectFrequency.Elapsed += new System.Timers.ElapsedEventHandler(PingIP);
        }

        private string log="";

        public string Log
        {
            get { return log; }
            set
            {
                log = value;
                this.OnPropertyChanged();
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                this.OnPropertyChanged();
            }
        }
        private string status = "";

        /// <summary>
        /// 開始時間
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                this.OnPropertyChanged();
            }
        }
        private DateTime startTime = DateTime.Now;

        /// <summary>
        /// 持續時間
        /// </summary>
        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                this.OnPropertyChanged();
            }
        }
        private string duration;

        /// <summary>
        /// 計時器
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void Timer(object source, ElapsedEventArgs e)
        {
            TimeSpan ts = DateTime.Now.Subtract(startTime);

            int totalSecond = (int)ts.TotalSeconds;

            Hour = totalSecond / 3600;

            Minuite = ((int)ts.TotalSeconds % 3600) / 60;

            Second = ((int)ts.TotalSeconds % 3600) % 60;

            Duration = $"{Hour}小時  {Minuite}分鐘  {Second}秒";
        }

        /// <summary>
        /// 固定時間執行pingIP
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void PingIP(object source, ElapsedEventArgs e)
        {
            Status=PingIp(HostIP).ToString();
        }

        Ping pingSender = new Ping();

        /// <summary>
        /// ping IP
        /// </summary>
        /// <returns></returns>
        private string PingIp(string HostIP)
        {
            string Connected = "線路正常";
            string Disconnected = "斷線了";
            try
            {
                PingReply reply = pingSender.Send(HostIP, 120);//第一個引數為ip地址,第二個引數為ping的時間
                if (reply.Status == IPStatus.Success)
                    return Connected;
                else
                    return WriteLog(Disconnected);
            }
            catch (PingException ex)
            {
                return WriteLog(Disconnected);
            }                
        }
        /// <summary>
        /// 寫log
        /// </summary>
        private string WriteLog(string content)
        {
            string nowTime = DateTime.Now.ToString();
            using (StreamWriter sw = File.AppendText("ConnectLog.txt"))    
            {
                // Add some text to the file.
                sw.WriteLine(nowTime+ $"  {content}");
                Log += nowTime+ $"  {content}" + "\r\n";
            }
            return content;
        }
        private bool CanCommand()
        {
            return true;
        }
    }
}
