using System;
using System.Data;
using System.IO;

namespace SRO_Process_Merge
{
    class FileProcessing
    {
        public bool WriteFile(DataTable Data, string FileName)
        {
            bool success = false;
            try
            {
                using (StreamWriter sw = new StreamWriter(FileName))
                {
                    string line = "";

                    foreach (DataColumn col in Data.Columns)
                    {
                        line = line + col.ColumnName.ToString() + ",";
                    }

                    sw.WriteLine(line.Substring(0, line.Length - 1));

                    foreach (DataRow r in Data.Rows)
                    {
                        line = "";
                        foreach (object c in r.ItemArray)
                        {
                            line = line + c.ToString() + ",";
                        }

                        sw.WriteLine(line.Substring(0, line.Length - 1));
                    }
                }
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Save Failed. " + e.Message);
            }

            return success;
        }

        public DataTable ReadFile(string FileName)
        {
            DataTable file = new DataTable();

            using (StreamReader sr = new StreamReader(FileName))
            {
                string[] columns = sr.ReadLine().Split(new char[]{','});
                foreach (string s in columns)
                    file.Columns.Add(s);

                while (!sr.EndOfStream)
                {
                    file.Rows.Add(sr.ReadLine().Split(new char[]{','}));
                }
            }
            return file;
        }
    }
}
