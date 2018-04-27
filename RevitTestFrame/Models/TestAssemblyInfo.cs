using RevitTestCore;
using RevitTestFrame.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace RevitTestFrame.Models
{
    public class TestAssemblyInfo:ViewModelBase
    {
        private bool _isChecked = false;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                RaisePropertyChanged();                
            }
        }

        private Assembly _assembly = null;
        public Assembly Assembly
        {
            get
            {
                return _assembly;
            }
            set
            {
                _assembly = value;                
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<TestTypeInfo> _typeInfos = new ObservableCollection<TestTypeInfo>();
        public ObservableCollection<TestTypeInfo> TypeInfos
        {
            get
            {
                return _typeInfos;
            }
            set
            {
                _typeInfos = value;
                RaisePropertyChanged();
            }
        }

        public TestAssemblyInfo()
        {

        }

        public TestAssemblyInfo(string asseblyPath)
        {
            byte[] bytes = File.ReadAllBytes(asseblyPath);
            //_assembly = Assembly.LoadFile(asseblyPath);
            _assembly = Assembly.Load(bytes);
            string assemblyName = _assembly.FullName;
            string dllname = assemblyName.Substring(0, assemblyName.IndexOf(','));

            AssemblyAndPath.AddAssembly(dllname, new FileInfo(asseblyPath).DirectoryName);
            AssemblyUtils.Default.GetTypeInfos(_assembly).ForEach(m=>_typeInfos.Add(m));
        }
    }
}
