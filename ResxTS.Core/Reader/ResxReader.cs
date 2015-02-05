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
        private const string RESX_EXTENSION = ".resx";
        private static readonly Regex RESX_NAME_REGEX = new Regex(@"^(?<name>[\w\d_]+)(\.(?<culture>[\w]{2}(\-[\w]{2})?))?(?<extension>\.resx)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        public static ResxReader Create(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
                throw new FileNotFoundException("File not found", fileInfo.FullName);

            if (!fileInfo.Extension.Equals(RESX_EXTENSION, StringComparison.InvariantCultureIgnoreCase))
                throw new FileLoadException("Invalid file. Resx-file required", fileInfo.FullName);

            var fileMatch = RESX_NAME_REGEX.Match(filePath);

            if(fileMatch.Success)
            {
                var name = fileMatch.Groups["name"].Value;
                var culture = "";

                var cultureMatch = fileMatch.Groups["culture"];
                if (cultureMatch.Success)
                    culture = cultureMatch.Value;

                return new ResxReader(name, culture, XDocument.Load(filePath));
            }
            else
            {
                throw new Exception("Unable to validate name and culture: " + fileInfo.FullName);
            }
        }

        public string Name { get; private set; }
        public string Culture { get; private set; }

        private readonly XDocument doc;

        private ResxReader(string name, string culture, XDocument doc)
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
