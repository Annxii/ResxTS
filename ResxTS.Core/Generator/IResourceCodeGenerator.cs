using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Generator
{
    public interface IResourceCodeGenerator
    {
        public string Generate(string @namespace, ResxResult mainResx, ResxResult localizedResx = null);

        public void Generate(Stream stream, string @namespace, ResxResult mainResx, ResxResult localizedResx = null);
    }
}
