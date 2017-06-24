using System.Data;
using XlsxComparsionAndFileComposer.Extensions;

namespace XlsxComparsionAndFileComposer.Writer
{
    /// <summary>
    /// 
    /// </summary>
    public class XlsxWriter : IFileWriter
    {
        private DataTable _source;

        public void ImportSource(DataTable source)
        {
            _source = source;
        }

        public void Write(string fileName)
        {
            _source.ExportToExcel(fileName);
        }
    }
}
