using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetToolkit
{
    public static class ObjectHelper
    {
        public static T ToObject<T>(this Object obj)
        {
            return JObject.FromObject(obj).ToObject<T>();
        }

        public static Object ToObject(this Object obj, Type type)
        {
            return JObject.FromObject(obj).ToObject(type);
        }
    }
}
