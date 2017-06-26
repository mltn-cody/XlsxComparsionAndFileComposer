using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace XlsxComparsionAndFileComposer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="comparisionKeyOne"></param>
        /// <param name="comparisionKeyTwo"></param>
        /// <returns></returns>
        Task WriteAsync(string fileName,string comparisionKeyOne, string comparisionKeyTwo);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        Task ImportSourcesAsync(IEnumerable<DataTable> sources);
    }
}