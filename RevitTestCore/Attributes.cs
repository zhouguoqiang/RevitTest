using System;

namespace RevitTestCore
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestClassAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DocumentPath { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class TestMethodAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
