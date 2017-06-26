namespace XlsxComparsionAndFileComposer.Writer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICompareFactory<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICompare<T> CreateComparer();
    }
}