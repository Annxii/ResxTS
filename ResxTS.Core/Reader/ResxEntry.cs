using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Reader
{
    public class ResxEntry
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public ResxEntry(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
