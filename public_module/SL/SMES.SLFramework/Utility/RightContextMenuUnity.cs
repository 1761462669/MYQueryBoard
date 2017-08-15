using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SMES.Framework.Utility
{
    /// <summary>
    /// 该类负责向TextBox添加右键菜单，菜单包括复制、粘贴、剪切、选择全部四项
    /// </summary>
    public class RightContextMenuUnity
    {
        // 菜单
         ContextMenu menu;
        //TextBox
         RichTextBox textbox;

        /// <summary>
        /// 为textBox添加右键菜单
        /// </summary>
        /// <param name="textBox">所要添加的TextBox</param>
        public void creatMenu(RichTextBox textBox)
        {
            menu = new ContextMenu();
            menu.Items.Add(getItem("剪切", "../Images/CutHS.png"));
            menu.Items.Add(getItem("复制", "../Images/CopyHS.png"));
            menu.Items.Add(getItem("粘贴", "../Images/PasteHS.png"));
            ContextMenuService.SetContextMenu(textBox, menu);
            textbox = textBox;
        }

        /// <summary>
        /// 得到一个菜单项
        /// </summary>
        /// <param name="header">菜单项的名字</param>
        /// <param name="imagePath">菜单项的图标</param>
        /// <returns></returns>
        private MenuItem getItem(string header, string imagePath)
        {
            MenuItem item = new MenuItem();
            item.Header = header;
            if (imagePath != null)
            {
                Image im = new Image();
                im.Source = (new ImageSourceConverter()).ConvertFromString(imagePath) as ImageSource;
                item.Icon = im;
            }
            item.Click += new RoutedEventHandler(item_Click);
            return item;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            try
            {
                execute(menuItem.Header.ToString());
            }
            catch (Exception)
            {
            }
            textbox.Focus();
            menu.IsOpen = false;
        }

        /// <summary>
        /// 具体执行操作，之所以将其提出来，是为了捕捉异常
        /// </summary>
        /// <param name="selected">菜单项的名称</param>
        void execute(string selected)
        {
            switch (selected)
            {
                case "剪切":
                    Clipboard.SetText(textbox.Selection.Text);
                    textbox.Selection.Text = "";
                    break;
                case "复制":
                    Clipboard.SetText(textbox.Selection.Text);
                    break;
                case "粘贴":
                    textbox.Selection.Text = Clipboard.GetText();
                    break;
                default:
                    break;
            }
        }
    }
}
