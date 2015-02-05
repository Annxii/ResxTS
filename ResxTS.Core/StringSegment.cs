using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core
{
    public class StringSegment : ResxValueSegment
    {
        public string Value
        {
            get { return this.RawValue; }
        }
        private StringSegment(int start, int end, string rawValue)
            : base(SegmentType.String, start, end, rawValue)
        {

        }

        public static StringSegment Create(int start, int end, string rawValue)
        {
            var segment = new StringSegment(start, end, rawValue);
            return segment;
        }
    }
}
