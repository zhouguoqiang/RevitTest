using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestFrame.Models
{
    public class AssemblyAndPath
    {
        public static Dictionary<string, string> AssemblyAndPathDict = new Dictionary<string, string>();

        public static void AddAssembly(string assemblyKey,string assemblyPath)
        {
            if (!AssemblyAndPathDict.ContainsKey(assemblyKey))
                AssemblyAndPathDict.Add(assemblyKey, assemblyPath);
        }
    }
}
