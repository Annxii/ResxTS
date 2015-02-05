using ResxTS.Core.Reader;
using ResxTS.Core.Scanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core
{
    public class ResxResult
    {
        private Dictionary<string, ResxValue> entries = new Dictionary<string, ResxValue>();

        public string Name { get; private set; }
        public string Culture { get; private set; }
        public bool IsDefault { get; private set; }

        public IReadOnlyDictionary<string, ResxValue> Entries
        {
            get { return entries; }
        }

        public ResxResult(string name, string culture, bool isDefault = false)
        {
            Name = name;
            Culture = culture;
            IsDefault = isDefault;
        }

        public static ResxResult Create<T>(IResxReader<T> reader) where T : ResxEntry
        {
            var scanner = new ResxValueScanner();
            var result = new ResxResult(reader.Name, reader.Culture, isDefault: string.IsNullOrWhiteSpace(reader.Culture));

            foreach (var item in reader.GetEntries())
            {
                result.entries.Add(item.Key, scanner.Scan(item.Value));
            }

            return result;
        }
    }
}
