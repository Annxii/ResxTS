using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ResxTS.Utils
{
    public class ResxReader
    {
        private readonly XDocument doc;

        public ResxReader(string filePath)
        {
            this.doc = XDocument.Load(filePath);
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
