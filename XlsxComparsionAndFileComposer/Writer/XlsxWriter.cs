using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
