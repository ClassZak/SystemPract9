using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemPract9
{
    internal static class Program
    {
        static void FinishProgram()
        {
            if (EndProgram != null)
                EndProgram(null,null);
        }
        static void StartProgram()
        {
            if (BeginProgram != null)
                BeginProgram(null, null);
        }
        static public event EventHandler EndProgram;
        static public event EventHandler BeginProgram;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EndProgram += new EventHandler(Logger.HandleEndOfApp);
            BeginProgram += new EventHandler(Logger.HandleBeginOfApp);
            StartProgram();
            Application.Run(new Form1());
            FinishProgram();
        }
    }
}
