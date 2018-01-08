using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
namespace WindowsFormsApp1
{
    public partial class 跳两跳 : Form
    {
        internal bool prePos;
        internal bool curPos;
        internal bool isPress;
        private struct Vector2
        {
            public int x;
            public int y;
        }
        private double distance = 0;
        private Vector2 prePosition,curPosition;
        [DllImport("User32")]
        public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        [DllImport("User32")]
        public extern static bool GetCursorPos(out POINT p);

        [StructLayout(LayoutKind.Sequential)]

        public struct POINT
        {
            public int X;
            public int Y;
        }
        private float time;
        public enum MouseEventFlags
        {
            Move = 0x0001, 
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800,
            Absolute = 0x8000
        }

        public 跳两跳()
        {
            InitializeComponent();
        }
        System.Timers.Timer myTimer;
        System.Timers.Timer timer;
        private void Form1_Load(object sender, EventArgs e)
        {
            myTimer = new System.Timers.Timer(200);//定时周期2秒
            timer = new System.Timers.Timer(10);
            myTimer.Elapsed += myTimer_Elapsed;//到2秒了做的事件
            timer.Elapsed += timer_Elapsed;//到2秒了做的事件
            myTimer.AutoReset = true; //是否不断重复定时器操作
            myTimer.Enabled = true;
            
            this.TopMost = true;
            this.KeyPreview = true;
            prePos = false;
            curPos = false;

            //MessageBox.Show(this.Location.X + " " + this.Location.Y);
        }
       
        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            //this.Activate();
            if (!prePos)
            {
                label1.Text = Control.MousePosition.ToString();
                prePosition.x = MousePosition.X;
                prePosition.y = MousePosition.Y;
            }
            if(!curPos)
            {
                label2.Text = Control.MousePosition.ToString();
                curPosition.x = MousePosition.X;
                curPosition.y = MousePosition.Y;
                distance = Math.Pow(prePosition.x - curPosition.x, 2) + Math.Pow(prePosition.y - curPosition.y, 2);
                distance = Math.Sqrt(distance);
                label3.Text = distance.ToString();
            }
            
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            time += 0.01f;
            label4.Text = time.ToString();
            
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(e.KeyChar == 'q')
            {
                prePos = true;
            }
            else if (e.KeyChar == 'w')
            {
                curPos = true;
                
            }
            else if(e.KeyChar == 'e')
            {
                
                //float pretime = time;
                SetCursorPos(prePosition.x, prePosition.y);
                
                timer.Enabled = true;
                isPress = true;
                Thread thread1 = new Thread(new ThreadStart(Thread1));
                thread1.Start();
                thread1.IsBackground = true;
                
            }
            
                //    MessageBox.Show(tim)
                //  //mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), prePosition.x, prePosition.y, 0, IntPtr.Zero);
                //    if(time >= 1)
                //    {
                //        isPress = false;
                //    }
                //}
                //mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), prePosition.x, prePosition.y, 0, IntPtr.Zero);

            }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("要使用程序，你需要：一台手机，一根数据线，一台电脑\n 首先请下载total control这款软件 : http://tc.sigma-rt.com.cn/ 这是一款可以在电脑上控制手机的软件\n成功连接电脑后，打开跳一跳，光标移动到人物所在方块按Q，移动到下一个方块按W，\n然后按E开始自动跳跃，通过调整微调数字来调整跳的距离（数字越大跳的越近），调到合适的数字后只需要重复前面的步骤即可\n这个属于半自动辅助软件，绝对不会被封号，原则上也不会被检测，请放心使用","帮助");
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        void Thread1()
            {
                //bool isPress = true;
                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), prePosition.x, prePosition.y, 0, IntPtr.Zero);
                while (isPress)
                {
                     if (time >= distance / (float)numericUpDown1.Value)
                    {
                        isPress = false;
                    }
                }
                timer.Stop();
                time = 0;
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), prePosition.x, prePosition.y, 0, IntPtr.Zero);
                prePos = false;
                curPos = false;
            //this.Activate();
            //this.Activate();
            if (this.Focused == false)
            {
                this.Activate();
            }
            
        }
    }
}
