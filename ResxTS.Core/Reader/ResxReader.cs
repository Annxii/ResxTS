using ResxTS.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ResxTS.Core.Reader
{
    public class ResxReader : IResxReader<ResxEntry>
    {
        public string Name { get; private set; }
        public string Culture { get; private set; }

        private readonly XDocument doc;

        internal ResxReader(string name, string culture, XDocument doc)
        {
            this.Name = name;
            this.Culture = culture;
            this.doc = doc;
        }

        public IEnumerable<ResxEntry> GetEntries()
        {
            foreach (var item in doc.Descendants(XName.Get("data", "")))
            {
                var nameAttribute = item.Attribute(XName.Get("name", ""));
                var valueElement = item.Element(XName.Get("value", ""));

                if (nameAttribute != null && valueElement != null)
                {
                    yield return new ResxEntry(nameAttribute.Value, valueElement.Value);
                }
            }
        }
    }
}
