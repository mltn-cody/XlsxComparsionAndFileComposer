using System;
using Ninject.Modules;
using XlsxComparsionAndFileComposer.Writer;

namespace XlsxComparsionAndFileComposer.Core
{
    public class XlsxResolver : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IFileReader>().To<XlsxReader>();
            Kernel.Bind<IFileWriter>().To<XlsxWriter>();
        }
    }
}
