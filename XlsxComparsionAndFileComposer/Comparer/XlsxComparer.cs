using System.Data;

namespace XlsxComparsionAndFileComposer
{
    public class XlsxComparer
    {
        private ICompare<DataTable> _compare;

        public XlsxComparer(ICompare<DataTable> comparer)
        {
            _compare = comparer;
        }
    }
}