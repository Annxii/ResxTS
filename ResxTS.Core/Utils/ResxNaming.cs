using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResxTS.Core.Utils
{
    public static class ResxNaming
    {
        public const string Extension = ".resx";
        public static readonly Regex NameRegex = new Regex(@"^(?<name>[\w\d_]+)(\.(?<culture>[\w]{2}(\-[\w]{2})?))?(?<extension>\.resx)$", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
    }
}
