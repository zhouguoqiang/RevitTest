using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RevitTestFrame.Models
{
    public class TestTypeInfo:ViewModelBase
    {
        private string _documentPath = null;
        public string DocumentPath
        {
            get
            {
                return _documentPath;
            }
        }

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
                RaisePropertyChanged();//(nameof(IsChecked));                
            }
        }

        private Type _type = null;
        public Type Type
        {
            get
            {
               // _type.Name
                return _type;
            }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<TestMethodInfo> _methodInfos = new ObservableCollection<TestMethodInfo>();
        public ObservableCollection<TestMethodInfo> MethodInfos
        {
            get
            {
                return _methodInfos;
            }
            set
            {
                _methodInfos = value;
                RaisePropertyChanged();
            }
        }

        public TestTypeInfo(Type type, List<TestMethodInfo> methodInfos,string documentPath=null)
        {
            _type = type;
            methodInfos.ForEach(m=>_methodInfos.Add(m));
            _documentPath = documentPath;
        }
    }
}
