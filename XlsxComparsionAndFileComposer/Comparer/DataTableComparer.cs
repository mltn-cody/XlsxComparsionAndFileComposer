using System.Data;
using System.Linq;
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
        /// <param name="ourColumnKey"></param>
        /// <returns></returns>
        public Task<DataTable> CompareAsync(string theirColumnKey, string ourColumnKey)
        {
            // Todo use data parallelism to split this task. 
            return Task.Run(() =>
            {
                var pattern = new Regex($"{ourColumnKey}");
                var outputTable = new DataTable();
                foreach (DataRow row in _ourData.Rows)
                {
                    var matchingRows = _theirData.AsEnumerable()
                        .Where(r => Regex.IsMatch(r[theirColumnKey].ToString(), row[ourColumnKey].ToString()));
                  if(!matchingRows.Any()) continue;

                    outputTable.Merge(matchingRows.CopyToDataTable());
                }

                return outputTable;
            });
        }
    }
}
