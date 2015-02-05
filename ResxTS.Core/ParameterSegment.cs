using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core
{
    public class ParameterSegment : ResxValueSegment
    {
        public int ParameterNumber { get; private set; }

        private ParameterSegment(int start, int end, string rawValue, int parameterNumber)
            : base(SegmentType.Parameter, start, end, rawValue)
        {
            ParameterNumber = parameterNumber;
        }

        public static ParameterSegment Create(int start, int end, string rawValue, int parameterNumber)
        {
            var segment = new ParameterSegment(start, end, rawValue, parameterNumber);

            return segment;
        }
    }
}
