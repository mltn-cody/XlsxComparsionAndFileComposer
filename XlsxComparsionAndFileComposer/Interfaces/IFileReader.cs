using System.Collections.Generic;
using System.Data;

namespace XlsxComparsionAndFileComposer
{
    interface IFileReader
    {
        List<DataTable> Collection { get; }
        void ProcessDirectory(string targetDirectory);
        DataTable Read(string fileName);
    }
}