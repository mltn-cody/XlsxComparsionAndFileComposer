using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace XlsxComparsionAndFileComposer
{
    public class XlsxReader : IFileReader
    {
        public List<DataTable> Collection { get; } = new List<DataTable>();


        /// <summary>
        /// Process all files in the directory passed in, recurse on any directories 
        //  that are found, and process the files they contain.
        /// </summary>
        /// <param name="targetDirectory"></param>
        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory, "*.xlsx");
            foreach (string fileName in fileEntries)
                Collection.Add(Read(fileName));

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        public DataTable Read(string fileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No of sheets on excel file  
           
            using (OleDbConnection objConn = new OleDbConnection(@" Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + fileName + "; Extended Properties = 'Excel 8.0;HDR=YES'"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                        where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                        select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returning Datatable  
            }
        }
    }
}