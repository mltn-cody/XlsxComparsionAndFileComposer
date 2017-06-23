using System.Data;

namespace XlsxComparsionAndFileComposer
{
    interface IFileReader
    {
        DataTable Read(string fileName);
    }
}