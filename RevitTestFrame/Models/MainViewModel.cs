using System.Linq;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows.Forms;
using RevitTestFrame.Views;
using RevitTestFrame.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace RevitTestFrame.Models
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<TestAssemblyInfo> _assemblyInfos = new ObservableCollection<TestAssemblyInfo>();
        public ObservableCollection<TestAssemblyInfo> AssemblyInfos
        {
            get
            {                
                return _assemblyInfos;
            }
            set
            {
                _assemblyInfos = value;
                RaisePropertyChanged();// (nameof(TypeInfos));
            }
        }

        private RelayCommand _ok_Command = null;
        public RelayCommand OK_Command
        {
            get
            {
                if(_ok_Command==null)
                {
                    _ok_Command = new RelayCommand(p => {

                        ResultViewModel vm = new ResultViewModel(_assemblyInfos);
                        TestResult win = new TestResult();
                        win.DataContext = vm;
                        win.Show();

                        ITestMethodUtils testUtils = new TestMethodUtils(win);

                        List<Action> actions = new List<Action>();

                        foreach (TestAssemblyInfo abInfo in _assemblyInfos)
                        {
                            if (abInfo.IsChecked)
                            {
                                List<TestTypeInfo> testTypeInfos = abInfo.TypeInfos.ToList();
                                foreach (TestTypeInfo testTypeInfo in testTypeInfos)
                                {
                                    if (testTypeInfo.IsChecked)
                                    {
                                        if (File.Exists(testTypeInfo.DocumentPath))
                                        {
                                            actions.Add(new Action(() =>
                                            {
                                                DocumentManager.OpenDocument(testTypeInfo.DocumentPath);
                                            }));
                                        }
                                        object obj = TypeInstanceUtils.Default.GetInstance(testTypeInfo.Type);
                                        List<TestMethodInfo> methodInfos = testTypeInfo.MethodInfos.ToList();
                                        foreach (TestMethodInfo methodInfo in methodInfos)
                                        {
                                            if (methodInfo.IsChecked)
                                            {
                                                actions.Add(new Action(() =>
                                                {
                                                    testUtils.TestMethod(methodInfo, obj);
                                                }));
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        TestController.Default.SetTests(actions);
                        TestController.Default.RunTest();
                    },p=> {                       

                        return true;
                    });
                }

                _ok_Command.RaiseCanExecuteChanged();
                return _ok_Command;
            }
        }

        private RelayCommand _load_Command = null;
        public RelayCommand Load_Command
        {
            get
            {
                if(_load_Command==null)
                {
                    _load_Command = new RelayCommand(p =>
                    {
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Filter = "*|*.dll";
                        if(dialog.ShowDialog()== DialogResult.OK)
                        {
                            string filepath = dialog.FileName;
                            TestAssemblyInfo testAssemblyInfo = new TestAssemblyInfo(filepath);
                            _assemblyInfos.Add(testAssemblyInfo);
                        }
                    }, p =>
                    {
                        return true;
                    });
                }

                _load_Command.RaiseCanExecuteChanged();
                return _load_Command;
            }
        }

        private RelayCommand _remove_Command = null;
        public RelayCommand Remove_Command
        {
            get
            {
                if(_remove_Command==null)
                {
                    _remove_Command = new RelayCommand(p =>
                    {
                        IEnumerator  itor = _assemblyInfos.GetEnumerator();
                        while(itor.MoveNext())
                        {
                            TestAssemblyInfo current = itor.Current as TestAssemblyInfo;
                            if(current.IsChecked)
                            {
                                _assemblyInfos.Remove(current);
                            }
                        }
                    }, p =>
                    {
                        return _assemblyInfos.Count(m => m.IsChecked) != 0;
                    });
                }
                _remove_Command.RaiseCanExecuteChanged();
                return _remove_Command;
            }
        }

        public MainViewModel()//(ITesAssemblytUtils tesAssemblytUtils)
        {
            //_tesAssemblytUtils = tesAssemblytUtils;
        }

        //private ITesAssemblytUtils _tesAssemblytUtils = null;
    }
}
