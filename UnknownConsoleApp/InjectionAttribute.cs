using System;
using System.Collections.Generic;
using System.Text;

namespace UnknownConsoleApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false)]
    public class InjectionAttribute : Attribute
    {
    }
}
