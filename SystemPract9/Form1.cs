using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemPract9
{
    class Logger
    {
        static UInt64 ButtonClicks = 0,TextChanges=0;
        protected byte[] stringToBytes(string str)
        {
            byte[] bytes = new byte[str.Length / 2];

            for (int i = 0; i != str.Length; i += 2)
                bytes[i / 2] = (byte)str[i];
            return bytes;
        }

        static bool AppRunning;
        static public bool LogFileExists(string path,string desiredName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            bool haveCurrLog = false;
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                if (file.Name == desiredName)
                {
                    haveCurrLog = true;
                    break;
                }
            }
            return haveCurrLog;
        }
        static void WriteLogInfo()
        {
            StreamWriter streamWriter = OpenLog();
            streamWriter.WriteLine(ButtonClicks.ToString().PadRight(8, ' ')+"\tclicks made");
            streamWriter.WriteLine(TextChanges.ToString().PadRight(8,' ')+"\ttext changes made");
            streamWriter.Close();
        }
        static public void HandleEndOfApp(object sender, EventArgs e)
        {
            AppRunning=false;
            LogRunning();
            WriteLogInfo();
        }
        static public void HandleBeginOfApp(object sender, EventArgs e)
        {
            AppRunning = true;
            LogRunning();
        }


        static StreamWriter OpenLog()
        {
            string desiredName = "Log" + DateTime.Now.Date.ToShortDateString() + ".txt";
            FileStream fileStream = new FileStream("Logs\\\\" + desiredName, LogFileExists("Logs", desiredName) ? FileMode.Open : FileMode.Create, FileAccess.ReadWrite);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.BaseStream.Seek(0, SeekOrigin.End);
            return writer;
        }

        static public void HandleClick(object sender, EventArgs e)
        {
            StreamWriter streamWriter = OpenLog();

            streamWriter.WriteLine(DateTime.Now.TimeOfDay.ToString() + "\t"+(++ButtonClicks).ToString().PadRight(8,' ')+" Click");
            streamWriter.Close();
        }

        static public void HandleTextChanges(object sender, EventArgs e)
        {
            StreamWriter streamWriter = OpenLog();
            ++TextChanges;

            streamWriter.WriteLine(DateTime.Now.TimeOfDay.ToString() + "\tText box with name\t" + ((System.Windows.Forms.TextBox)(sender)).Name + "\twas changed");
            streamWriter.Close();
        }

        public static void LogRunning()
        {
            StreamWriter streamWriter= OpenLog();

            streamWriter.WriteLine(DateTime.Now.TimeOfDay.ToString() + "\t"+(AppRunning ? "Application started" : "Application finished"));
            streamWriter.Close();
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox3.ReadOnly = true;
            button1.Click += new EventHandler(Logger.HandleClick);
            textBox1.TextChanged += new EventHandler(Logger.HandleTextChanges);
            textBox2.TextChanged += new EventHandler(Logger.HandleTextChanges);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = textBox1.Text + textBox2.Text;
        }
    }
}
