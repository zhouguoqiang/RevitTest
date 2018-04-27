using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitTestCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestFrame.Utils
{
    public sealed class DocumentManager
    {
        public static void OpenDocument(string docPath)
        {
            UIApplication uiapp = Helper.UIApplication;
            Application app = Helper.Application;
            Document oldDoc = uiapp.ActiveUIDocument.Document;

            //if (docPath.EndsWith("rte"))
            //{
                //Document doc = app.NewProjectTemplateDocument(docPath);
            //}
            //else
            //{
                uiapp.OpenAndActivateDocument(docPath);
            //}
            if(oldDoc!=null)
            {
                oldDoc.Close(false);
            }            
        }
    }
}
