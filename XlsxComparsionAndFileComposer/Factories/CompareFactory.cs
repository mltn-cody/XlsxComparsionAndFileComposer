using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using XlsxComparsionAndFileComposer.Writer;

namespace XlsxComparsionAndFileComposer.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public class CompareFactory : ICompareFactory<DataTable>
    {
        private readonly IResolutionRoot _resolutionRoot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolutionRoot"></param>
        public CompareFactory(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ICompare<DataTable> CreateComparer(IEnumerable<DataTable> sources)
        {
            var dataTables = sources as IList<DataTable> ?? sources.ToList();
            return _resolutionRoot.Get<ICompare<DataTable>>(
                new ConstructorArgument("theirData", dataTables.Last()),
                new ConstructorArgument("ourData", dataTables.First()));
        }
    }
}
