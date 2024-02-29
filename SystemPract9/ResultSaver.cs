using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemPract9
{
    class ResultSaver
    {
        static private bool ResultFileExists(string path, string desiredName)
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
        const string resultFilePath = "result.txt";
        const string resultDirectoryPath = "Result";
        static bool resultWasSAved = false;

        static public void SaveResult(string results)
        {
            FileStream fileStream = new FileStream(resultFilePath, File.Exists(resultFilePath) ? FileMode.Open : FileMode.Create, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            streamWriter.WriteLine(results);
            resultWasSAved = true;
            streamWriter.Close();
        }
        static public void SaveSessionResult()
        {
            if(resultWasSAved)
            try
            {
                FileStream fileStream = new FileStream(resultFilePath, FileMode.Open, FileAccess.ReadWrite);
                StreamReader streamReader = new StreamReader(fileStream);
                streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            
                DirectoryInfo directoryInfo = new DirectoryInfo(resultDirectoryPath);
                if (!directoryInfo.Exists)
                {
                    MessageBox.Show("Директория для результатов не найдена", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MessageBox.Show("Создаётся директория", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    directoryInfo.Create();
                }
                    


                string FullName = resultDirectoryPath + "\\\\" + resultFilePath.Substring(0, resultFilePath.LastIndexOf('.')) + "_"
                    + DateTime.Now.ToShortDateString()+" "
                    + DateTime.Now.Hour.ToString().PadLeft(2,'0')+"."
                    + DateTime.Now.Minute.ToString().PadLeft(2, '0')+ "." 
                    + DateTime.Now.Second.ToString().PadLeft(2, '0') + "."
                    + DateTime.Now.Millisecond.ToString().PadLeft(7, '0')+".txt";
                FileStream fileStreamStore = new FileStream(FullName, FileMode.Create, FileAccess.Write);
                StreamWriter streamWriterStore = new StreamWriter(fileStreamStore);
                streamWriterStore.BaseStream.Seek(0, SeekOrigin.Begin);

                string currStrinng;
                while (true)
                {
                    if ((currStrinng = streamReader.ReadLine()) != null)
                        streamWriterStore.WriteLine(currStrinng);
                    else
                        break;
                }


                streamWriterStore.Close();
                streamReader.Close();
                File.Delete(resultFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Ошибка сохранеия сессионых результатов",MessageBoxButtons.OK,MessageBoxIcon.Error);
                string FullName = 
                    resultDirectoryPath + "\\\\" + resultFilePath.Substring(0, resultFilePath.LastIndexOf('.')) + "_"
                    + DateTime.Now.ToShortDateString() + " "
                    + DateTime.Now.Hour.ToString().PadLeft(2, '0') + "."
                    + DateTime.Now.Minute.ToString().PadLeft(2, '0') + "."
                    + DateTime.Now.Second.ToString().PadLeft(2, '0') + "."
                    + DateTime.Now.Millisecond.ToString().PadLeft(7, '0') + ".txt";
                FileStream fileStreamStore = new FileStream(FullName, FileMode.Create, FileAccess.Write);
                throw;
            }
        }

    }
}
