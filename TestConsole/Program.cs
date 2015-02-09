using ResxTS.Core;
using ResxTS.Core.Generator;
using ResxTS.Core.Reader;
using ResxTS.Core.Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = new ResxResultProvider();
            var result = provider.GetResult("Testresources.resx");

            var generator = new TypeScriptGenerator();

            var output = generator.Generate("anx.localized", result);
        }
    }
}
