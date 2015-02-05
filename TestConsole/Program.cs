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
            var rdr = ResxReader.Create("Testresources.resx");
            var result = ResxResult.Create(rdr);

            var generator = new TypeScriptGenerator();

            var output = generator.Generate("anx.localized", result);
        }
    }
}
