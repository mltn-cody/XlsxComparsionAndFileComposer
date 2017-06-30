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
    /// <remarks>
    /// https://msdn.microsoft.com/en-us/library/ff963547.aspx
    /// </remarks>>
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
            // Todo this maybe a canidate for map/reduce algorithm
            return Task.Run(() =>
            {
                var pattern = new Regex($"{ourColumnKey}");
                var outputTable = new DataTable();

                Parallel.ForEach(_ourData.AsEnumerable(), row => LoadData(theirColumnKey, ourColumnKey, row, outputTable));

                return outputTable;
            });
        }

        private void LoadData(string theirColumnKey, string ourColumnKey, DataRow row, DataTable outputTable)
        {
            var matchingRows = _theirData.AsEnumerable()
                .Where(r => Regex.IsMatch(r[theirColumnKey].ToString(), row[ourColumnKey].ToString()));
            if (!matchingRows.Any()) return;

            outputTable.Merge(matchingRows.CopyToDataTable());
        }


        private Task Map()
        {
            return Task.Run(() => { });
        }

        private Task Reduce()
        {
            return Task.Run(() => { });
        }
    }
}
