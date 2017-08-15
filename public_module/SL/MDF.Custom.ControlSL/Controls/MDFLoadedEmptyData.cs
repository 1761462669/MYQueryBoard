using System;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFLoadedEmptyData:ContentControl
    {
        public MDFLoadedEmptyData()
        {
            this.DefaultStyleKey = typeof(MDFLoadedEmptyData);

            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (ItemSource == null) return;

            PagedCollectionView collection = new PagedCollectionView(ItemSource);

            if (collection.Count == 0)
            {
                
            }
            else
            {

            }
        }


        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(IEnumerable), typeof(MDFLoadedEmptyData), new PropertyMetadata(null, new PropertyChangedCallback((sender, e) =>
                {
                    
                })));
        
    }
}
