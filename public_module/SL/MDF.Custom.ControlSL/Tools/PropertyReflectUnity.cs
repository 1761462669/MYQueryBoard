using System;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;

namespace MDF.Custom.ControlSL.Tools
{
    public static class PropertyReflectUnity
    {
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);
            var p = t.GetType().GetProperties().FirstOrDefault(c => c.Name == propertyname);
            var value = p.GetValue(t, null);

            return value.ToString();
        }
    }
}
