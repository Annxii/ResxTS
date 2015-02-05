using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Scanner
{
    public class StringValueScanner
    {
        public ResxValue Scan(string txt)
        {
            var creator = new ResxValue.Creator();

            ParseText(creator, 0, ref txt);

            var value = creator.Create();

            return value;
        }

        private void ParseText(ResxValue.Creator creator, int startIdx, ref string txt)
        {
            int idx = startIdx;
            StringBuilder buffer = new StringBuilder();

            while (txt.Length > idx)
            {
                if (txt[idx] == '{')
                {
                    if (txt.Length > idx + 1)
                    {
                        // cruly brace escape char
                        if (txt[idx + 1] == '{')
                        {
                            // skip one of the braces to add only one to the text value
                            idx++;
                        }
                        else
                        {
                            ParseParameter(creator, idx, ref txt);

                            // revert one step as it belongs to the parameter
                            idx--;
                            break;
                        }
                    }
                }

                buffer.Append(txt[idx]);
                idx++;
            }

            if(buffer.Length > 0)
            {
                creator.AddSegment(StringSegment.Create(startIdx, idx, buffer.ToString()));
            }
        }

        private void ParseParameter(ResxValue.Creator creator, int startIdx, ref string txt)
        {
            int idx = startIdx;
            StringBuilder buffer = new StringBuilder();
            
            if (txt[idx] != '{')
                throw new StringParseException("Expected '{': " + txt);

            // skip initial character
            idx++;

            while (txt[idx] != '}')
            {
                if (txt.Length <= idx)
                    throw new StringParseException("Parameter is missing ending: " + txt);

                buffer.Append(txt[idx]);
                idx++;
            }

            int paramNum = -1;
            if(!int.TryParse(buffer.ToString(), out paramNum))
            {
                throw new StringParseException("Invalid parameter content: " + txt);
            }
            else
            {
                creator.AddSegment(ParameterSegment.Create(startIdx, idx, txt.Substring(startIdx, idx - startIdx + 1), paramNum));
            }

            idx++;
            ParseText(creator, idx, ref txt);
        }
    }
}
