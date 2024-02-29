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
        
        protected byte[] stringToBytes(string str)
        {
            byte[] bytes = new byte[str.Length / 2];

            for (int i = 0; i != str.Length; i += 2)
                bytes[i / 2] = (byte)str[i];
            return bytes;
        }

        static public bool AppRunning;
        static public void HandleEndOfApp(object sender, EventArgs e)
        {
            AppRunning=false;
            LogRunning();
        }
        static public void HandleBeginOfApp(object sender, EventArgs e)
        {
            AppRunning = true;
            LogRunning();
        }
        public static void LogRunning()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("Logs");
            bool haveCurrLog = false;
            string desiredName="Log"+DateTime.Now.Date.ToShortDateString()+".txt";
            try
            {
                foreach(FileInfo file in directoryInfo.GetFiles("*.*"))
                {
                    if(file.Name==desiredName)
                    {
                        haveCurrLog = true;
                        break;
                    }
                }
            }
            catch { }
            

            FileStream fileStream=new FileStream("Logs\\\\"+desiredName, haveCurrLog ? FileMode.Open : FileMode.Create, FileAccess.ReadWrite,FileShare.Write);
            StreamWriter fileWriter = new StreamWriter(fileStream);
            fileWriter.BaseStream.Seek(0, SeekOrigin.End);
            fileWriter.Write(DateTime.Now.ToShortTimeString()+"\t");
            fileWriter.WriteLine(AppRunning ? "Application started" : "Application Finished");
            
            fileWriter.Close();
            //fileStream.Close();
        }
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox3.ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox3.Text = textBox1.Text + textBox2.Text;
        }
    }
}
