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
        private ICompare<DataTable> Compare { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparer"></param>
        public XlsxWriter(ICompare<DataTable> comparer)
        {
            Compare = comparer;
        }

        public async Task ImportSource(DataTable source)
        {
            var data = await Compare.CompareAsync("", "").ConfigureAwait(false);
            _source = source;
        }

        public void Write(string fileName)
        {
            _source.ExportToExcel(fileName);
        }
    }
}
