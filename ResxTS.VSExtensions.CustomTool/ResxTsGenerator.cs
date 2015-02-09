using Microsoft.VisualStudio.TextTemplating.VSHost;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using System.Text.RegularExpressions;
using ResxTS.Core.Utils;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using System.IO;
using ResxTS.Core;
using ResxTS.Core.Generator;

namespace ResxTS.VSExtensions.CustomTool
{
    [Guid(GuidList.guidCustomToolString)]
    [ProvideObject(typeof(ResxTsGenerator))]
    [CodeGeneratorRegistration(typeof(ResxTsGenerator), "ResxTsGenerator", VSConstants.UICONTEXT.CSharpProject_string, GeneratesDesignTimeSource = true)]
    public class ResxTsGenerator : BaseCodeGeneratorWithSite
    {
        private static readonly ResxResultProvider resxResultProvider = new ResxResultProvider();
        private static readonly TypeScriptGenerator resxTypeScriptGenerator = new TypeScriptGenerator();

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            byte[] mainFileBytes = null;
            var targetItem = Dte.Solution.FindProjectItem(inputFileName);

            if (targetItem != null)
            {
                var targetFileInfo = new FileInfo(targetItem.FileNames[0]);

                var resxResults = GetRelatedFilesNames(inputFileName).Select(x => resxResultProvider.GetResult(x));

                List<string> generatedFileNames = new List<string>();

                ResxResult structureItem = null;
                foreach (var item in resxResults)
                {
                    var fileName = GetFileName(targetFileInfo, item);
                    if (item.IsDefault)
                    {
                        structureItem = item;

                        using (var ms = new MemoryStream())
                        {
                            resxTypeScriptGenerator.Generate(ms, FileNamespace, structureItem, item);
                            mainFileBytes = ms.ToArray();
                        }
                    }
                    else
                    {
                        using (var fs = File.Open(fileName, FileMode.Create, FileAccess.Write))
                        {
                            resxTypeScriptGenerator.Generate(fs, FileNamespace, structureItem, item);
                        }

                        // only add the file to the project for localized files
                        // visual studio handles the addition of the default file
                        targetItem.ProjectItems.AddFromFile(fileName);
                    }

                    generatedFileNames.Add(fileName);
                }

                // ensure only generated files are child-items
                foreach (ProjectItem item in targetItem.ProjectItems)
                {
                    if(!generatedFileNames.Contains(item.FileNames[0]))
                    {
                        item.Delete();
                    }
                }
            }

            return mainFileBytes;
        }

        private string GetFileName(FileInfo targetFileInfo, ResxResult result)
        {
            string fileName = "";
            if (result.IsDefault)
                fileName = string.Format("{0}.ts", result.Name);
            else
                fileName = string.Format("{0}.{1}.ts", result.Name, result.Culture);

            return Path.Combine(targetFileInfo.Directory.FullName, fileName);
        }

        private IEnumerable<string> GetRelatedFilesNames(string mainFileName)
        {
            FileInfo fileInfo = new FileInfo(mainFileName);
            if(fileInfo.Exists)
            {
                var match = ResxNaming.NameRegex.Match(fileInfo.Name);
                if (match.Success)
                {
                    var name = match.Groups["name"].Value.ToLower();

                    var siblings = fileInfo.Directory
                        .EnumerateFiles("*.resx", SearchOption.TopDirectoryOnly)
                        .Where(x => x.Name.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                        .OrderBy(x => x.Name.Length)
                        .ThenBy(x => x.Name)
                        .Select(x => x.FullName);
                    
                    return siblings;
                }
            }

            return Enumerable.Empty<string>();
        }

        public override string GetDefaultExtension()
        {
            return ".ts";
        }
    }
}
