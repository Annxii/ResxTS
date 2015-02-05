using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core
{
    public enum SegmentType
    {
        String,
        Parameter
    }

    public abstract class ResxValueSegment
    {
        public SegmentType Type { get; private set; }
        public int Start { get; private set; }
        public int End { get; private set; }
        public string RawValue { get; private set; }

        protected ResxValueSegment(SegmentType type, int start, int end, string rawValue)
        {
            Type = type;
            Start = start;
            End = end;
            RawValue = rawValue;
        }
    }
}
