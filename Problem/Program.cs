using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Problem
{
    class Program
    {
        static string rowvalues;
        static DataTable workingTable = new DataTable("Working Tabel");
        
        static void Main(string[] args)
        {
            Reader r = new Reader();
           DataTable table =  r.readCsvFileData();
            workingTable.Columns.Add("clientId", typeof(string));
            workingTable.Columns.Add("distinctSongPlayed", typeof(int));
            workingTable.Columns.Add("songsId", typeof(string));
            foreach (DataRow dr in table.Rows)
            {
                rowvalues = string.Empty;
                rowvalues = dr[0].ToString();
                string[] client = Regex.Split(rowvalues, "\t");

                bool existsClientId = workingTable.Select().ToList().Exists(row => row["clientId"].ToString() == client[2].ToString());
                string[] date = client[3].Split(' ');
                if (date[0] == "10/08/2016") 
                {
                   
                    if (existsClientId == false)
                    {
                        workingTable.Rows.Add(client[2], 1, client[1]);
                    }
                    else
                    {
                        foreach (DataRow rowInWorkingTable in workingTable.Rows)
                        {

                            if (rowInWorkingTable["clientId"].ToString() == client[2])
                            {
                                string songsId = rowInWorkingTable["songsId"].ToString();
                                int distinctSongPlayed = Int32.Parse(rowInWorkingTable["distinctSongPlayed"].ToString());
                                string[] splitSongsId = songsId.Split(',');
                                bool songsIdFound = false;
                                foreach (var item in splitSongsId)
                                {
                                    if (item == client[1])
                                    {
                                        songsIdFound = true;
                                    }

                                }
                                if (songsIdFound == false)
                                {
                                    rowInWorkingTable["songsId"] = songsId + "," + client[1];
                                    rowInWorkingTable["distinctSongPlayed"] = distinctSongPlayed + 1;
                                }
                            }

                        }
                    }
                }
            }
           
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("distinct_play_count", typeof(int));
            resultTable.Columns.Add("client_count", typeof(int));
            foreach (DataRow dataRow in workingTable.Rows)
            {

               // Console.WriteLine(dataRow[1].ToString());
                bool existsNrDistinctPlay = resultTable.Select().ToList().Exists(row => row["distinct_play_count"].ToString() == dataRow[1].ToString());

                if(existsNrDistinctPlay == false)
                {
                    resultTable.Rows.Add(Int32.Parse(dataRow[1].ToString()), 1);
                }
                else
                {
                    foreach (DataRow drow in resultTable.Rows)
                    {
                        int client_count = Int32.Parse(drow["client_count"].ToString());
                        if (dataRow[1].ToString() == drow["distinct_play_count"].ToString())
                        {
                            drow[1] =client_count + 1;
                        }
                    }
                   
                }
                

            }
            int nr = 0;
            foreach (DataRow dataRow in resultTable.Rows)
            {
                
                   Console.WriteLine(dataRow[0] + "\t" + dataRow[1]);
                //if(Int32.Parse(dataRow[0].ToString())>nr)
                //{
                //    nr = Int32.Parse(dataRow[0].ToString());
                //}
                
            }
          
            Console.WriteLine(nr);
            Console.Read();
        }
        
    }
}




