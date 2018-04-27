using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using System.Threading;
using System.Reflection;
using System.IO;
using RevitTestCore;
using RevitTestFrame.Utils;
using RevitTestFrame.Views;
using RevitTestFrame.Models;

namespace RevitTestFrame
{
    [Transaction(TransactionMode.Manual)]
    public class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string tabName = "TestTab";
            string tabPanel = "TestPanel";

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            application.CreateRibbonTab(tabName);

            RibbonPanel panel = application.CreateRibbonPanel(tabName, tabPanel);

            PushButtonData data = new PushButtonData("TestCommand", "Unit Testing", this.GetType().Assembly.Location, "RevitTestFrame.TestCommand");

            panel.AddItem(data);

            data = new PushButtonData("DelegateCommand", "dbCommand", this.GetType().Assembly.Location, "RevitTestFrame.DelegateCommand");

            PushButton dbItem = panel.AddItem(data) as PushButton;
            dbItem.Visible = false;
            TestController.SetItem(dbItem);

            return Result.Succeeded;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyName = args.Name;
            string dllname = assemblyName.Substring(0, assemblyName.IndexOf(','));

            if (dllname.EndsWith("resources"))
                return null;

            if (args.Name == "RevitTestCore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
                return typeof(Helper).Assembly;


            Assembly currentAssembly = args.RequestingAssembly;

            if (File.Exists(currentAssembly.Location))
            {
                FileInfo f = new FileInfo(currentAssembly.Location);
                string dir = f.DirectoryName;
                return Assembly.LoadFile(Path.Combine(dir, dllname + ".dll"));
            }
            else
            {

                string requestAssemblyName = currentAssembly.FullName.Substring(0, currentAssembly.FullName.IndexOf(','));

                if (AssemblyAndPath.AssemblyAndPathDict.ContainsKey(requestAssemblyName))
                {
                    string requestAssemblyPath = Path.Combine(AssemblyAndPath.AssemblyAndPathDict[requestAssemblyName], dllname + ".dll");
                    return Assembly.LoadFile(requestAssemblyPath);
                }
            }
            return null;

        }
    }

    [Transaction(TransactionMode.Manual)]
    public class TestCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Helper.CommandData = commandData;
            MainView mainView = new MainView();

            MainViewModel vm = new MainViewModel();

            mainView.DataContext = vm;

            mainView.Show();

            return Result.Succeeded;
        }

        public TestCommand() {
            TestController.CreateInstance();
        }

    }

    [Transaction(TransactionMode.Manual)]
    public class DelegateCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            
            Helper.CommandData = commandData;
            Helper.Message = message;
            Helper.ElementSet = elements;

            if (TestController.Default.TestAction!= null)
            {
                TestController.Default.TestAction();
            }
            return Result.Cancelled;
        }
    }

}
