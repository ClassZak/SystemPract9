using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemPract9
{
    class Logger
    {
        static UInt64 ButtonClicks = 0, TextChanges = 0;
        static bool AppRunning;
        static public bool LogFileExists(string path, string desiredName)
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
            streamWriter.WriteLine(ButtonClicks.ToString().PadRight(8, ' ') + "\tclicks made");
            streamWriter.WriteLine(TextChanges.ToString().PadRight(8, ' ') + "\ttext changes made");
            streamWriter.Close();
        }
        static public void HandleEndOfApp(object sender, EventArgs e)
        {
            AppRunning = false;
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
            try
            {
                string desiredName = "Log" + DateTime.Now.Date.ToShortDateString() + ".txt";
                FileStream fileStream = new FileStream("Logs\\\\" + desiredName, LogFileExists("Logs", desiredName) ? FileMode.Open : FileMode.Create, FileAccess.ReadWrite);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.BaseStream.Seek(0, SeekOrigin.End);

                return writer;

            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ошибка директории", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("Создать необходимую директорию?", "Попытка создания необходимой директории для логов", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return null;
                }
                else
                {
                    DirectoryInfo directory = new DirectoryInfo("Logs");
                    directory.Create();

                    string desiredName = "Log" + DateTime.Now.Date.ToShortDateString() + ".txt";
                    FileStream fileStream = new FileStream("Logs\\\\" + desiredName, FileMode.Create, FileAccess.ReadWrite);
                    StreamWriter writer = new StreamWriter(fileStream);

                    writer.BaseStream.Seek(0, SeekOrigin.End);

                    return writer;
                }

            }
            catch (System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка открытия файла!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        static public void HandleClick(object sender, EventArgs e)
        {
            ++ButtonClicks;
            StreamWriter streamWriter = OpenLog();
            if (streamWriter == null)
                return;


            streamWriter.WriteLine(DateTime.Now.TimeOfDay.ToString() + "\t" + ButtonClicks.ToString().PadRight(8, ' ') + " Click");
            streamWriter.Close();
        }

        static public void HandleTextChanges(object sender, EventArgs e)
        {
            ++TextChanges;
            StreamWriter streamWriter = OpenLog();
            if (streamWriter == null)
                return;

            streamWriter.WriteLine(DateTime.Now.TimeOfDay.ToString() + "\tText box with name\t" + ((System.Windows.Forms.TextBox)(sender)).Name + "\twas changed");
            streamWriter.Close();
        }

        public static void LogRunning()
        {
            StreamWriter streamWriter = OpenLog();

            streamWriter.WriteLine(DateTime.Now.TimeOfDay.ToString() + "\t" + (AppRunning ? "Application started" : "Application finished"));
            streamWriter.Close();
        }
    }
}