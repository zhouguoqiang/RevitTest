using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestFrame.Utils
{
    public class TypeInstanceUtils
    {
        private Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        private static WeakReference _default = null;
        public static TypeInstanceUtils Default
        {
            get
            {
                if(_default==null||!_default.IsAlive)
                {
                    _default = new WeakReference(new TypeInstanceUtils());
                }
                return _default.Target as TypeInstanceUtils;
            }
        }

        public object GetInstance(Type type)
        {
            if(!_instances.ContainsKey(type))
            {
                try
                {
                    object obj = Activator.CreateInstance(type);
                    _instances.Add(type, obj);
                }catch(Exception e)
                {
                    Type t = e.GetType();
                }
            }
            return _instances[type];
               
        }
        private TypeInstanceUtils() { }
    }
}
