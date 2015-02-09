using ResxTS.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResxTS.Core.Generator
{
    public class TypeScriptGenerator : IResourceCodeGenerator
    {
        public string Generate(string @namespace, ResxResult mainResx, ResxResult localizedResx = null)
        {
            var b = new TypeScriptBuilder();

            WriteModule(b, 0, @namespace, mainResx, localizedResx);

            return b.ToString();
        }

        public void Generate(Stream stream, string @namespace, ResxResult mainResx, ResxResult localizedResx = null)
        {
            using (IndentWriter w = new IndentWriter(stream))
            {
                WriteModule(w, 0, @namespace, mainResx, localizedResx);
            }
        }

        private void WriteModule(IIdentWriter w, int i, string @namespace, ResxResult mainResx, ResxResult localizedResx)
        {
            w.WL(i, "module {0} {{", @namespace);

            if(localizedResx == null || localizedResx.IsDefault)
            {
                WriteInterface(w, i + 1, mainResx);
                w.LineBreak();
            }

            WriteImplementation(w, i + 1, mainResx, localizedResx ?? mainResx);

            w.LineBreak();
            
            WriteInstanceInitialization(w, i + 1, localizedResx ?? mainResx);

            w.WL(i, "}");
        }

        private void WriteInterface(IIdentWriter w, int i, ResxResult mainResx)
        {
            w.WL(i, "export interface I{0} {{", mainResx.Name);

            foreach (var item in mainResx.Entries.OrderBy(x => x.Key))
            {
                w.WL(i + 1,"{0};", GetKeySignature(item.Key, item.Value));
            }

            w.WL(i, "}");
        }

        private void WriteImplementation(IIdentWriter w, int i, ResxResult mainResx, ResxResult localizedResx)
        {
            w.WL(i, "class {0}Impl {{", mainResx.Name);

            foreach (var mainItem in mainResx.Entries.OrderBy(x => x.Key))
            {
                ResxValue contentItem = null;
                if (!localizedResx.Entries.TryGetValue(mainItem.Key, out contentItem))
                {
                    contentItem = mainItem.Value;
                }

                WriteResourceFunction(w, i + 1, mainItem.Key, mainItem.Value, contentItem);
            }

            w.WL(i, "}");
        }

        private void WriteResourceFunction(IIdentWriter w, int i, string key, ResxValue structure, ResxValue content)
        {
            w.WL(i, "public {0} {{", GetKeySignature(key, structure));

            var contentSegments =
                content.Segments.Select(seg =>
                {
                    if (seg.Type == SegmentType.Parameter)
                    {
                        return string.Format("param{0}", ((ParameterSegment)seg).ParameterNumber);
                    }
                    else
                    {
                        return string.Format("\"{0}\"", ((StringSegment)seg).Value.Replace("\"", "\\\""));
                    }
                });

            w.WL(i + 1, "return {0};", string.Join(" + ", contentSegments));

            w.WL(i, "}");
        }

        private void WriteInstanceInitialization(IIdentWriter w, int i, ResxResult content)
        {
            w.WL(i, "{1}{0}: I{0} = new {0}Impl();", content.Name, content.IsDefault ? "export var " : "");
        }

        private string GetKeySignature(string key, ResxValue value)
        {
            return string.Format("{0}({1}): string",
                char.ToLower(key[0]) + key.Substring(1),
                string.Join(", ", Enumerable.Range(0, value.GetNumOfParameters()).Select(x => string.Format("param{0}: string", x)))
            );
        }
    }
}
