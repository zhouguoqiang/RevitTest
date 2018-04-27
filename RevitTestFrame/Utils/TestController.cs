using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UIFrameworkServices;

namespace RevitTestFrame.Models
{
    public class TestController
    {
        private static Autodesk.Windows.RibbonButton ribbonButton = null;
        private object locker = new object();

        private List<Action> _actions = null;
        private List<Action>.Enumerator actionItor;

        private Action _action = null;
        public Action TestAction
        {
            get
            {
                return _action;
            }
        }

        private TestController() { }

        public void RunTest()
        {
            lock (locker)
            {
                while (actionItor.MoveNext())
                {
                    _action = actionItor.Current;
                    Autodesk.Windows.ComponentManager.Ribbon.Dispatcher.Invoke(() =>
                    {
                        ExternalCommandHelper.executeExternalCommand(ribbonButton.Id);
                    });
                }
            }
        }

        public static void SetItem(PushButton btn)
        {
            if (ribbonButton == null)
            {
                MethodInfo method = btn.GetType().GetMethod("getRibbonButton", BindingFlags.NonPublic | BindingFlags.Instance);
                ribbonButton = method.Invoke(btn, null) as Autodesk.Windows.RibbonButton;
            }
        }

        public void SetTests(List<Action> actions)
        {
            _actions = actions;
            actionItor = _actions.GetEnumerator();
        }

        private static TestController _default = null;
        public static TestController Default
        {
            get
            {
                return _default;
            }
        }
        public static void CreateInstance()
        {
            _default = new TestController();
        }
    }
}
