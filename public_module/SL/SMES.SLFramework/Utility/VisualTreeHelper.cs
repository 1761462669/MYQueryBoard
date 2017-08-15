using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SMES.Framework.Utility
{
    public static class VisualTreeHelper
    {
        public static T GetParent<T>(this DependencyObject reference) where T : DependencyObject
        {
            if (reference == null)
                return null;
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(reference) as DependencyObject;
            if (parent == null)
                return null;
            if (parent is T)
                return parent as T;
            else
                return GetParent<T>(parent);

        }


        public static DependencyObject GetParent(this FrameworkElement reference, string name)
        {
            if (reference == null)
                return null;
            var parent = System.Windows.Media.VisualTreeHelper.GetParent(reference) as FrameworkElement;
            if (parent == null)
                return null;
            if (parent.Name == name)
                return parent as DependencyObject;
            else
                return GetParent(parent, name);

        }

        public static T GetChildObject<T>(DependencyObject obj) where T : FrameworkElement
         {
             DependencyObject child = null;
             T grandChild = null;

             for (int i = 0; i < System.Windows.Media.VisualTreeHelper.GetChildrenCount(obj); i++)
             {
                 child = System.Windows.Media.VisualTreeHelper.GetChild(obj, i);
 
                 if (child is T)
                 {
                     return (T)child;
                 }
                 else
                 {
                     grandChild = GetChildObject<T>(child);
                     if (grandChild != null)
                         return grandChild;
                 }
             }
 
             return null;
 
         }

        public static T SearchVisualTree<T>(DependencyObject tarElem, string name) where T : DependencyObject
        {
            var count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(tarElem);
            if (count == 0)
                return null;

            for (int i = 0; i < count; ++i)
            {
                var child = System.Windows.Media.VisualTreeHelper.GetChild(tarElem, i);
                if (child != null && child is T && (child as FrameworkElement).Name == name)
                {
                    return (T)child;
                }
                else
                {
                    var res = SearchVisualTree<T>(child, name);
                    if (res != null)
                    {
                        return res;
                    }
                }
            }

            return null;
        }

    }

    
}
