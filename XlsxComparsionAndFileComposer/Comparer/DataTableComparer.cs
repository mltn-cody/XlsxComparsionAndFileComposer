using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace XlsxComparsionAndFileComposer
{
    /// <summary>
    /// 
    /// </summary>
    public class DataTableComparer : ICompare<DataTable>
    {
        private readonly DataTable _theirData;
        private readonly DataTable _ourData;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theirData"></param>
        /// <param name="ourData"></param>
        public DataTableComparer(DataTable theirData, DataTable ourData)
        {
            _theirData = theirData;
            _ourData = ourData;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="theirColumnKey"></param>
        /// <param name="ourDataKey"></param>
        /// <returns></returns>
        public Task<DataTable> CompareAsync(string theirColumnKey, string ourDataKey)
        {
            return Task.Run(() =>
            {
                var pattern = new Regex($"{ourDataKey}");
                var outputTable = new DataTable();
                foreach (DataRow row in _ourData.Rows)
                {
                    var matchingRows = _theirData.AsEnumerable()
                        .Where(r => Regex.IsMatch(r[theirColumnKey].ToString(), row[ourDataKey].ToString()));

                    outputTable.Merge(matchingRows.CopyToDataTable());
                }

                return outputTable;
            });
        }
    }
}
