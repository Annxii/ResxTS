using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core
{
    public class ResxValue
    {
        public class Creator
        {
            private List<ResxValueSegment> segments = new List<ResxValueSegment>();

            public Creator AddSegment(ResxValueSegment segment)
            {
                this.segments.Add(segment);

                return this;
            }

            public ResxValue Create()
            {
                return new ResxValue(this.segments);
            }
        }

        private ResxValueSegment[] segments;

        protected ResxValue(IEnumerable<ResxValueSegment> segments)
        {
            this.segments = segments.OrderBy(x => x.Start).ToArray();
        }

        public IEnumerable<ResxValueSegment> Segments
        {
            get { return segments; }
        }

        public int GetNumOfParameters()
        {
            return Segments.OfType<ParameterSegment>().Select(x => x.ParameterNumber).Distinct().Count();
        }
    }
}
