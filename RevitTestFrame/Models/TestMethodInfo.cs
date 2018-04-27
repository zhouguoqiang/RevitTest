using System.Reflection;

namespace RevitTestFrame.Models
{
    public class TestMethodInfo:ViewModelBase
    {
        private string _detail = string.Empty;
        public string Detail
        {
            get
            {
                return _detail;
            }
            set
            {
                _detail = value;
                RaisePropertyChanged();
                //RaisePropertyChanged(nameof(DetailCommand));
            }
        }

        private bool? _testResult = null;
        public string TestResult
        {
            get
            {
                string str = null;
                if(_testResult==null)
                {
                    str = "Waiting";
                }
                if(_testResult.HasValue)
                {
                    if(_testResult.Value)
                    {
                        str = "Success";
                    }
                    else
                    {
                        str = "Failed";
                    }
                }
                return str;
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
                RaisePropertyChanged();
            }
        }

        private MethodInfo _methodInfo = null;
        public MethodInfo MethodInfo
        {
            get
            {
                return _methodInfo;
            }
            set
            {
                _methodInfo = value;
                RaisePropertyChanged();
            }
        }

        public TestMethodInfo(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        public void SetResult(bool result)
        {
            _testResult = result;
            RaisePropertyChanged(nameof(TestResult));
        }

    }
}
