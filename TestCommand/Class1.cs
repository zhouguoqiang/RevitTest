using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace TestCommand
{
    [Transaction(TransactionMode.Manual)]
    public class TestCommand1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            Transaction trans = new Transaction(doc, "create");
            trans.Start();
            Line line = Line.CreateBound(XYZ.Zero, XYZ.BasisX * 10);

            FilteredElementCollector lvls = new FilteredElementCollector(doc);
            Level level = lvls.OfClass(typeof(Level)).First() as Level;

            Wall wall = Wall.Create(doc, line, level.Id, false);
            trans.Commit();
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    public class TestCommand2 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            Transaction trans = new Transaction(doc, "create");
            trans.Start();
            Line line = Line.CreateBound(XYZ.Zero, XYZ.BasisX * 10);

            FilteredElementCollector lvls = new FilteredElementCollector(doc);
            Level level = lvls.OfClass(typeof(Level)).First() as Level;

            Wall wall = Wall.Create(doc, line, level.Id, false);
            trans.Commit();
            return Result.Succeeded;
        }
    }
}
