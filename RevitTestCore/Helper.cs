using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;

namespace RevitTestCore
{
    public class Helper
    {
        private static UIApplication _uiapplication = null;
        public static UIApplication UIApplication
        {
            get
            {
                if(_uiapplication == null)
                {
                    _uiapplication = _commandData.Application;
                }
                return _uiapplication;
            }
        }

        public static Application Application
        {
            get
            {
                return _uiapplication.Application;
            }
        }

        public static Document Document
        {
            get
            {
                return _uiapplication.ActiveUIDocument.Document;
            }
        }

        public static UIDocument UIDocument
        {
            get
            {
                return _uiapplication.ActiveUIDocument;
            }
        }

        private static ExternalCommandData _commandData = null;
        public static ExternalCommandData CommandData
        {
            get
            {
                return _commandData;
            }
            set
            {
                _commandData = value;
            }
        }

        public static string Message = null;


        public static ElementSet ElementSet = null;

    }
}
