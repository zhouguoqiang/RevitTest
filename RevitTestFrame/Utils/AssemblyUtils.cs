using RevitTestCore;
using RevitTestFrame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestFrame.Utils
{
    public class AssemblyUtils
    {
        public List<TestTypeInfo> GetTypeInfos(Assembly assembly)
        {
            List<TestTypeInfo> result = new List<TestTypeInfo>();
            Type[] types= assembly.GetExportedTypes();
            foreach(Type t in types)
            {
                TestClassAttribute attribute = t.GetCustomAttribute(typeof(TestClassAttribute)) as TestClassAttribute;
                if(attribute != null)
                {
                    result.Add(TestTypeInfo(t,attribute.DocumentPath));
                }
            }
            return result;
        }

        public TestTypeInfo TestTypeInfo(Type type,string path)
        {
            List<TestMethodInfo> ms = new List<TestMethodInfo>();
            MethodInfo[] methodInfos = type.GetMethods();
            foreach(MethodInfo minfo in methodInfos)
            {
                TestMethodAttribute attribute = minfo.GetCustomAttribute(typeof(TestMethodAttribute)) as TestMethodAttribute;
                if(attribute!=null)
                {
                    ms.Add(new TestMethodInfo( minfo));
                }
            }
            return new TestTypeInfo(type, ms,path);
        }

        private static WeakReference _default = null;

        public static AssemblyUtils Default
        {
            get
            {
                if(_default==null||!_default.IsAlive)
                {
                    _default = new WeakReference(new AssemblyUtils());
                }
                return _default.Target as AssemblyUtils;
            }
        }

        private AssemblyUtils()
        { }
    }
}
