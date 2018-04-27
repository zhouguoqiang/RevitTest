using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTestCore
{
    public class CommandRunner
    {
        public static Result RunCommand<T>() where T:IExternalCommand
        {
            Result result = Result.Failed;
            Type type = typeof(T);
            IExternalCommand icmd = Activator.CreateInstance(type) as IExternalCommand;

            result = icmd.Execute(Helper.CommandData, ref Helper.Message, Helper.ElementSet);
            return Result.Succeeded;
        }

        public static Result RunCommand<T>(T icmd) where T : IExternalCommand
        {
            Result result = Result.Failed;
            result = icmd.Execute(Helper.CommandData, ref Helper.Message, Helper.ElementSet);
            return Result.Succeeded;
        }
    }
}
