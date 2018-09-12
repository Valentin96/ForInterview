using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
   public class Reader
    {
      public DataTable readCsvFileData() 
            // transform csv into dataTable
        {

            string path = @"C:\Users\valentin.ciufu\Desktop\qwewqeq\doc.csv";
            StreamReader streamreader = new StreamReader(path);

            DataTable datatable = new DataTable();

            int rowcount = 0;
            string[] columnname = null;
            string[] streamdatavalue = null;
            while (!streamreader.EndOfStream)
            {
                string streamrowdata = streamreader.ReadLine().Trim();
                if (streamrowdata.Length > 0)
                {
                    streamdatavalue = streamrowdata.Split(':');
                    if (rowcount == 0)
                    {
                        rowcount = 1;

                        columnname = streamdatavalue;
                        foreach (string csvheader in columnname)
                        {
                            DataColumn datacolumn = new DataColumn(csvheader.ToUpper(), typeof(string));
                            datacolumn.DefaultValue = string.Empty;
                            datatable.Columns.Add(datacolumn);


                        }

                    }
                    else
                    {
                        DataRow datarow = datatable.NewRow();
                        for (int i = 0; i < columnname.Length; i++)
                        {
                            datarow[columnname[i]] = streamdatavalue[i] == null ? string.Empty : streamdatavalue[i].ToString();
                        }
                        datatable.Rows.Add(datarow);
                    }
                }
            }
            streamreader.Close();
            streamreader.Dispose();
            return datatable;
            foreach (DataRow dr in datatable.Rows)
            {
                string rowvalues = string.Empty;
                foreach (string csvcolumns in columnname)
                {
                    rowvalues += csvcolumns + "=" + dr[csvcolumns].ToString() + "    ";
                }
               // Console.WriteLine(rowvalues);
            }
         //   Console.ReadLine();
        }
    }
}
