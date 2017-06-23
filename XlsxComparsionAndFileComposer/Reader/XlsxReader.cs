using System;
using System.Data;
using System.Data.OleDb;

namespace XlsxComparsionAndFileComposer
{
    class XlsxReader : IFileReader
    {
        public DataTable Read(string fileName)
        {
            DataTable dtResult = null;
            int totalSheet = 0; //No�of�sheets�on�excel�file��
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data�Source=" + fileName + ";Extended�Properties='Excel�12.0;HDR=YES;IMEX=1;';"))
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
                cmd.CommandText = "SELECT�*�FROM�[" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returning�Datatable��
            }
        }
    }
}