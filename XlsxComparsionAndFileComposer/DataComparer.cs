using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XlsxComparsionAndFileComposer
{
    class XlsxComparer
    {
        private ICompare<DataTable> _compare;

        public XlsxComparer(ICompare<DataTable> comparer)
        {
            _compare = comparer;
        }
    }

    class DataTableComparer : ICompare<DataTable>
    {
        private DataTable _sourceOne;
        private DataTable _sourceTwo;

        public Task Compare(DataTable sourceOne, DataTable sourceTwo, string sOneKey, string sTwoKey)
        {
            return Task.Run(() =>
            {
                foreach (var row in sourceOne.Rows)
                {
                    var findTheseVals = new object[1];
                    var pattern = new Regex(sTwoKey);


                    sourceTwo.Rows.Find(findTheseVals);

                }
            });
        }
    }

    internal interface ICompare<T>
    {
        Task Compare(T sourceOne, T sourceTwo, string sOneKey, string sTwoKey);
    } 
}
