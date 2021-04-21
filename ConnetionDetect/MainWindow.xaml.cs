using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConnetionDetect
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        NotifyIcon notifyIcon;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 托盤小圖示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Configure and show a notification icon in the system tray

            this.notifyIcon = new NotifyIcon();

            this.notifyIcon.BalloonTipText = "Connect Detector is executing in background";

            this.notifyIcon.Text = "Connect Detector";

            this.notifyIcon.Icon = Properties.Resources.Logo;
            //this.notifyIcon.Icon = new System.Drawing.Icon("Logo.ico");

            this.notifyIcon.MouseDoubleClick+= new System.Windows.Forms.MouseEventHandler(notifyIconMenuItem_DoubleClick);

            //TODO:初始化 notifyIcon菜單

            System.Windows.Forms.ContextMenu notifyIconMenu = new System.Windows.Forms.ContextMenu();


            System.Windows.Forms.MenuItem notifyIconMenuItem = new System.Windows.Forms.MenuItem();

            notifyIconMenuItem.Index = 0;

            notifyIconMenuItem.Text = "結束(Exit)";

            notifyIconMenuItem.Click += new EventHandler(notifyIconMenuItem_Click);

            notifyIconMenu.MenuItems.Add(notifyIconMenuItem);


            this.notifyIcon.ContextMenu = notifyIconMenu;
        }
        /// <summary>
        /// 關閉按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void notifyIconMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 雙擊小圖示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void notifyIconMenuItem_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.Show();//顯示窗體
            this.ShowInTaskbar = true;//圖示不顯示在工作列
            this.notifyIcon.Visible = false; //顯示托盤圖示
        }

        /// <summary>
        /// 放大縮小事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case WindowState.Maximized:
                    break;
                case WindowState.Minimized:
                    this.notifyIcon.Visible = true; //顯示托盤圖示
                    this.Hide();//隱藏窗體
                    this.ShowInTaskbar = false;//圖示不顯示在工作列
                    this.notifyIcon.ShowBalloonTip(5000);
                    break;
                case WindowState.Normal:

                    break;
            }
        }

        private void Window_StateChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
