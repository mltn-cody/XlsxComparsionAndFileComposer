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
        private DataTable _source;
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
        /// <param name="fileName"></param>
        public async Task WriteAsync(string fileName)
        {
            var comparer = _compareFactory.CreateComparer();
            var dataTable = await comparer.CompareAsync("", "").ConfigureAwait(false);
            _source = dataTable;
            _source.ExportToExcel(fileName);
        }
    }
}
