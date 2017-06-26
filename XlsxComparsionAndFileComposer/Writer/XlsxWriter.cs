using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using XlsxComparsionAndFileComposer.Extensions;

namespace XlsxComparsionAndFileComposer.Writer
{
    /// <summary>
    /// 
    /// </summary>
    public class XlsxWriter : IFileWriter
    {
        private IEnumerable<DataTable> _sources;
        private DataTable _exportSource;
        private ICompare<DataTable> _compare;
        private readonly ICompareFactory<DataTable> _compareFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="compareFactory"></param>
        public XlsxWriter(ICompareFactory<DataTable> compareFactory)
        {
            _compareFactory = compareFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        public  Task ImportSourcesAsync(IEnumerable<DataTable> sources)
        {
            return Task.Run(() =>_sources = sources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="columnKeyOne"></param>
        /// <param name="columnKeyTwo"></param>
        public async Task WriteAsync(string fileName, string columnKeyOne, string columnKeyTwo)
        {
            var comparer = _compareFactory.CreateComparer(_sources);
            var dataTable = await comparer.CompareAsync(columnKeyOne, columnKeyTwo).ConfigureAwait(false);
            _exportSource = dataTable;
            _exportSource.ExportToExcel(fileName);
        }
    }
}
