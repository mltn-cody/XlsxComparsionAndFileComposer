using System.Threading.Tasks;

namespace XlsxComparsionAndFileComposer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICompare<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sOneKey"></param>
        /// <param name="sTwoKey"></param>
        /// <returns></returns>
        Task<T> CompareAsync(string sOneKey, string sTwoKey);
    }
}