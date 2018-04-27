using RevitTestCore;
using RevitTestFrame.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitTestFrame.Utils
{
    ///// <summary>
    ///// test a asemmbly
    ///// </summary>
    //public interface ITesAssemblytUtils
    //{
    //    void TestAssembly(TestAssemblyInfo _assemblyInfo);
    //}
    //public class TesAssemblytUtils : ITesAssemblytUtils
    //{
    //    public void TestAssembly(TestAssemblyInfo _assemblyInfo)
    //    {
    //        foreach(TestTypeInfo tpInfo in _assemblyInfo.TypeInfos)
    //        {
    //            _testTypeUtils.TestType(tpInfo);
    //        }
    //    }
    //    //public TesAssemblytUtils() { }
    //    public TesAssemblytUtils(ITestTypeUtils testTypeUtils)
    //    {
    //        _testTypeUtils = testTypeUtils;
    //    }

    //    private ITestTypeUtils _testTypeUtils = null;
    //}

    ///// <summary>
    ///// test a type
    ///// </summary>
    //public interface ITestTypeUtils
    //{
    //    void TestType(TestTypeInfo typeInfo);
    //}
    //public class TestTypeUtils : ITestTypeUtils
    //{
    //    public void TestType(TestTypeInfo typeInfo)
    //    {
    //        if (typeInfo.IsChecked)
    //        {
    //            string str = GetDocumentPath(typeInfo.Type);
    //            if (!string.IsNullOrWhiteSpace(str))
    //            {
    //                if (!File.Exists(str))
    //                {
    //                    throw new FileNotFoundException(str);
    //                }
    //                DocumentManager.OpenDocument(str);
    //            }
    //            else
    //            {
    //                //Helper.Document.PathName
    //            }
    //            object obj = TypeInstanceUtils.Default.GetInstance(typeInfo.Type);
    //            foreach (TestMethodInfo mdInfo in typeInfo.MethodInfos)
    //            {
    //                if (mdInfo.IsChecked)
    //                {
    //                    _testMethodUtils.TestMethod(mdInfo, obj);
    //                }
    //            }
    //        }
    //    }

    //    //public TestTypeUtils(){}

    //    public TestTypeUtils(ITestMethodUtils testMethodUtils)
    //    {
    //        _testMethodUtils = testMethodUtils;
    //    }

    //    private string GetDocumentPath(Type type)
    //    {
    //        TestClassAttribute testClassAttribute = type.GetCustomAttribute(typeof(TestClassAttribute)) as TestClassAttribute;
    //        return testClassAttribute.DocumentPath;
    //    }
    //    private ITestMethodUtils _testMethodUtils = null;
    //}

    /// <summary>
    /// test a method
    /// </summary>
    public interface ITestMethodUtils
    {
        void TestMethod(TestMethodInfo minfo,object target);
    }
    public class TestMethodUtils : ITestMethodUtils
    {
        private Window _win = null;
        public void TestMethod(TestMethodInfo minfo, object target)
        {
            try
            {
                minfo.MethodInfo.Invoke(target, null);
                _win.Dispatcher.Invoke(() =>
                {
                    minfo.SetResult(true);
                });
            }
            catch (Exception ex)
            {
                Exception e = getException(ex);
                string msg = e.Message;
                _win.Dispatcher.Invoke(() =>
                {
                    minfo.SetResult(false);
                    minfo.Detail = msg + Environment.NewLine + e.StackTrace;
                });

            }
        }

        private Exception getException(Exception ex)
        {
            Exception e = ex;
            while (e.InnerException!=null)
            {
                e = e.InnerException;
            }
            return e;
        }


        public TestMethodUtils(Window win)
        {
            _win = win;
        }
    }
}
