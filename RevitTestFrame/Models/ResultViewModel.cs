using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RevitTestFrame.Models
{
    public class ResultViewModel:ViewModelBase
    {
        private ObservableCollection<TestMethodInfo> _results = new ObservableCollection<TestMethodInfo>();
        public ObservableCollection<TestMethodInfo> Results
        {
            get
            {                
                return _results;
            }
        }

        private TestMethodInfo _current = null;
        public TestMethodInfo Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ErrorInfo));
            }
        }

        private RelayCommand _closeCommand = null;
        public RelayCommand CloseCommand
        {
            get
            {
                if(_closeCommand==null)
                {
                    _closeCommand = new RelayCommand(p => 
                    {
                        Window win = p as Window;
                        if (win != null)
                            win.Close();
                    });
                }
                return _closeCommand;
            }
        }

        public string ErrorInfo
        {
            get
            {                
                return _current?.Detail;
            }

        }

        public ResultViewModel(ObservableCollection<TestAssemblyInfo> assemblyInfos)
        {
            foreach(TestAssemblyInfo abInfo in assemblyInfos)
            {
                foreach(TestTypeInfo tpInfo in abInfo.TypeInfos)
                {
                    foreach(TestMethodInfo mdInfo in tpInfo.MethodInfos)
                    {
                        if(mdInfo.IsChecked)
                        {
                            _results.Add(mdInfo);
                        }
                    }
                }
            }
        }

    }
}
