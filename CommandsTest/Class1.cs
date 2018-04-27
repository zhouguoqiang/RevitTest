using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitTestCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommand;
using System.Threading;

namespace CommandsTest
{
    [TestClass(DocumentPath= @"D:\MyGitHub\Src\WindowsApi\TestRvt\墙创建.rvt")]
    public class WallCreateTest
    {
        [TestMethod]
        public void Test()
        {
            Result result = CommandRunner.RunCommand<TestCommand1>();
            if(result!=Result.Succeeded)
            {
                Assert.Fail("失败");
            }
            TaskDialog.Show("test", "test");
            Document doc = Helper.Document;
            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            int n = wallCollector.OfClass(typeof(Wall)).Count();
            if(n!=1)
            {
                Assert.Fail("创建墙数量不对");
            }
        }
    }

    [TestClass(DocumentPath = @"D:\MyGitHub\Src\WindowsApi\TestRvt\项目1.rvt")]
    public class WallCreateTest1
    {
        [TestMethod]
        public void Test()
        {
            Result result = CommandRunner.RunCommand<TestCommand1>();
            if (result != Result.Succeeded)
            {
                Assert.Fail("失败");
            }
            TaskDialog.Show("test", "test");

            Document doc = Helper.Document;
            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            int n = wallCollector.OfClass(typeof(Wall)).Count();
            if (n != 1)
            {
                Assert.Fail("创建墙数量不对");
            }
            Assert.Fail("fail Test");
        }

        [TestMethod]
        public void TestPath()
        {
            string path = Helper.CommandData.Application.ActiveUIDocument.Document.PathName;
            if(path!= @"D:\MyGitHub\Src\WindowsApi\TestRvt\项目1.rvt")
            {
                Assert.Fail("文件不对！");
            }
        }
    }

    [TestClass(DocumentPath = @"C:\ProgramData\Autodesk\RVT 2018\Templates\China\DefaultCHSCHS.rte")]
    public class TemplateTest
    {
        [TestMethod]
        public void Test()
        {

            Result result = CommandRunner.RunCommand<TestCommand1>();
            if (result != Result.Succeeded)
            {
                Assert.Fail("失败");
            }
            TaskDialog.Show("test", "test");
            Document doc = Helper.Document;
            FilteredElementCollector wallCollector = new FilteredElementCollector(doc);
            int n = wallCollector.OfClass(typeof(Wall)).Count();
            if (n != 1)
            {
                Assert.Fail("创建墙数量不对");
            }

            Assert.Fail("fail test");
        }

    }
}
