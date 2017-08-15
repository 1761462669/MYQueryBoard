using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.Custom.ControlSL.Controls
{

    [TemplatePart(Name = "EditTemplate",Type=typeof(DataTemplate))]
    [TemplatePart(Name = "DisplayTemplate", Type = typeof(DataTemplate))]
    public class MDFEditDisplay:ContentControl
    {
        public MDFEditDisplay()
        {
            this.DefaultStyleKey = typeof(MDFEditDisplay);            
        }


        ContentPresenter editdisplaycontent;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            editdisplaycontent = this.GetTemplateChild("editdisplaycontent") as ContentPresenter;

            ChangeState();
        }



        public EditDisplayState State
        {
            get { return (EditDisplayState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(EditDisplayState), typeof(MDFEditDisplay), new PropertyMetadata(EditDisplayState.Display, new PropertyChangedCallback((sender, arg) =>
            {
                (sender as MDFEditDisplay).ChangeState();
            })));


        private void ChangeState()
        {

            if (editdisplaycontent == null)
                return;

            switch (State)
            { 
                case EditDisplayState.Edit:
                    editdisplaycontent.SetBinding(ContentPresenter.ContentTemplateProperty, new Binding("EditTemplate") { Source = this });
                    //editdisplaycontent.ContentTemplate = EditTemplate;
                    break;
                default:
                    editdisplaycontent.SetBinding(ContentPresenter.ContentTemplateProperty, new Binding("DisplayTemplate") { Source = this });
                    break;
                    
            }
        }

        public DataTemplate EditTemplate
        {
            get { return (DataTemplate)GetValue(EditTemplateProperty); }
            set { SetValue(EditTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EditTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EditTemplateProperty =
            DependencyProperty.Register("EditTemplate", typeof(DataTemplate), typeof(MDFEditDisplay), new PropertyMetadata(default(DataTemplate)));


        public DataTemplate DisplayTemplate
        {
            get { return (DataTemplate)GetValue(DisplayTemplateProperty); }
            set { SetValue(DisplayTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayTemplateProperty =
            DependencyProperty.Register("DisplayTemplate", typeof(DataTemplate), typeof(MDFEditDisplay), new PropertyMetadata(default(DataTemplate)));


        
    }


    public enum EditDisplayState
    { 
        /// <summary>
        /// 编辑状态
        /// </summary>
        Edit=0,

        /// <summary>
        /// 显示状态
        /// </summary>
        Display=1
    }
}
