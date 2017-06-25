using System;
using System.IO;
using System.Reflection;
using Ninject;
using XlsxComparsionAndFileComposer.Core;

namespace XlsxComparsionAndFileComposer
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<XlsxResolver>();
            var reader = kernel.Get<IFileReader>();
            var location = Assembly.GetExecutingAssembly().Location;
            if (location != null)
                reader.ProcessDirectory(new DirectoryInfo(location).Parent?.FullName + "/files");

            var compare = kernel.Get(typeof(ICompare<>));
            var writer = kernel.Get<IFileWriter>();
            
            Console.ReadLine();
        }
    }
}
