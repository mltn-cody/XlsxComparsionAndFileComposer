using System.Threading.Tasks;

namespace XlsxComparsionAndFileComposer
{
    internal interface ICompare<T>
    {
        Task<T> CompareAsync(string sOneKey, string sTwoKey);
    }
}