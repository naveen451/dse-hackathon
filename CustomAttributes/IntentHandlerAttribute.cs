using System;

namespace DSEHackatthon.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]   
    public class IntentHandlerAttribute : Attribute
    {
        public string Name{get;private set;}
        public IntentHandlerAttribute(string name)
        {
           this.Name=name;
        }
    }
}