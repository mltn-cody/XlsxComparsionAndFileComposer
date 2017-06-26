using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace XlsxComparsionAndFileComposer
{
    /// <summary>
    /// 
    /// </summary>
    public class XlsxReader : IFileReader
    {
        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable Read(string fileName)
        {
            using (OleDbConnection objConn = new OleDbConnection(@" Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + fileName + "; Extended Properties = 'Excel 8.0;HDR=YES'"))
            {
                objConn.Open();
                var ds = new DataSet();
                var dtSheet = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var sheetName = string.Empty;
                if (dtSheet != null)
                {
                    foreach (DataRow dr in dtSheet.Rows)
                    {
                        sheetName = dr["TABLE_NAME"].ToString();

                        if (!sheetName.EndsWith("$"))
                            continue;

                        var dt = new DataTable {TableName = sheetName};
                        var da = new OleDbDataAdapter($"SELECT * FROM [{sheetName}]", objConn);
                        da.Fill(dt);
                        ds.Tables.Add(dt);
                    }
                }
                var dtResult = ds.Tables[$"{sheetName}"];
                objConn.Close();
                return dtResult;  
            }
        }
    }
}