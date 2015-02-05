using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Reader
{
    public interface IResxReader<T> where T : ResxEntry
    {
        public string Name { get; }
        public string Culture { get; }
        public IEnumerable<T> GetEntries();
    }
}
