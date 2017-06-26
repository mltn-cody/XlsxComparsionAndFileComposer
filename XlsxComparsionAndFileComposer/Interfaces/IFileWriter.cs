using System.Threading.Tasks;

namespace XlsxComparsionAndFileComposer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFileWriter
    {
        Task WriteAsync(string fileName);
    }
}