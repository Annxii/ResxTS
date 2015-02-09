using ResxTS.Core.Reader;
using ResxTS.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ResxTS.Core
{
    public class ResxResultProvider : IResxResultProvider<ResxResult>
    {
        public virtual ResxResult GetResult(string filePath)
        {
            var reader = this.GetReader(filePath);

            return ResxResult.Create(reader);
        }

        protected virtual IResxReader<ResxEntry> GetReader(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
                throw new FileNotFoundException("File not found", fileInfo.FullName);

            if (!fileInfo.Extension.Equals(ResxNaming.Extension, StringComparison.InvariantCultureIgnoreCase))
                throw new FileLoadException("Invalid file. Resx-file required", fileInfo.FullName);

            var fileMatch = ResxNaming.NameRegex.Match(fileInfo.Name);

            if (fileMatch.Success)
            {
                var name = fileMatch.Groups["name"].Value;
                var culture = "";

                var cultureMatch = fileMatch.Groups["culture"];
                if (cultureMatch.Success)
                    culture = cultureMatch.Value;

                return new ResxReader(name, culture, XDocument.Load(fileInfo.FullName));
            }
            else
            {
                throw new Exception("Unable to validate name and culture: " + fileInfo.FullName);
            }
        }
    }
}
