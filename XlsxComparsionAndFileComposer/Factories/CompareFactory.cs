using System;
using System.Data;
using XlsxComparsionAndFileComposer.Writer;

namespace XlsxComparsionAndFileComposer.Factories
{
    public class CompareFactory : ICompareFactory<DataTable>
    {
        public ICompare<DataTable> CreateComparer()
        {
            throw new NotImplementedException();
        }
    }
}
