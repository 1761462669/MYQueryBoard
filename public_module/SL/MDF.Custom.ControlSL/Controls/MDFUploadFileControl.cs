using SMES.Framework.ConfigTools;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MDF.Custom.ControlSL.Controls
{
    public class MDFUploadFileControl:ContentControl
    {
        public int ChunkSize = 4194304;
        public Stream data;
        public double send = 0;
        public string filename;
        public Dispatcher UIDispatcher;

        MDFButton btnUpload;
        TextBlock lblText;
        TextBlock lblPrecentage;
        Rectangle rect;
        double rectWidthTemp = 0;
        string serviceUrl = "";


        public string FileDriKey
        {
            get { return (string)GetValue(FileDriKeyProperty); }
            set { SetValue(FileDriKeyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileDriKey.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileDriKeyProperty =
            DependencyProperty.Register("FileDriKey", typeof(string), typeof(MDFUploadFileControl), new PropertyMetadata(""));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MDFUploadFileControl), new PropertyMetadata(""));


        
        
        public MDFUploadFileControl()
        {
            this.DefaultStyleKey = typeof(MDFUploadFileControl);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DesignerProperties.IsInDesignTool) return;

            btnUpload = this.GetTemplateChild("btnUpload") as MDFButton;
            lblText = this.GetTemplateChild("lblText") as TextBlock;
            lblPrecentage = this.GetTemplateChild("lblPrecentage") as TextBlock;
            rect = this.GetTemplateChild("rect") as Rectangle;

            btnUpload.Click += btnUpload_Click;

            var sourceUrl = WebHelp.WebUrl;
            serviceUrl = sourceUrl + "/SLUpload/FileReceive.ashx";
        }

        #region 文件上传

        void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FileDriKey)) return;

            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "all files|*.*",
                Multiselect = false
            };
            if ((bool)dialog.ShowDialog())
            {
                send = 0;
                data = dialog.File.OpenRead();
                filename = dialog.File.Name;

                this.lblText.Text = dialog.File.Name;
                Text = dialog.File.Name;

                UIDispatcher = this.Dispatcher;
                StartUpload();
            }
        }

        private void StartUpload()
        {
            double dataToSend = data.Length - send;
            bool isLastChunk = dataToSend <= ChunkSize;
            bool isFirstChunk = send == 0;

            UriBuilder httpHandlerUrlBuilder = new UriBuilder(new Uri(serviceUrl, UriKind.Absolute));
            httpHandlerUrlBuilder.Query = string.Format("{5}file={0}&offset={1}&last={2}&first={3}&param={4}", filename, send, isLastChunk, isFirstChunk, "", string.IsNullOrEmpty(httpHandlerUrlBuilder.Query) ? "" : httpHandlerUrlBuilder.Query.Remove(0, 1) + "&");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(httpHandlerUrlBuilder.Uri);
            webRequest.Method = "POST";
            webRequest.BeginGetRequestStream(new AsyncCallback(WriteToStreamCallback), webRequest);

        }

        private void WriteToStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            Stream requestStream = webRequest.EndGetRequestStream(asynchronousResult);

            byte[] buffer = new Byte[4096];
            int bytesRead = 0;
            int tempTotal = 0;

            while (tempTotal + bytesRead < ChunkSize && (bytesRead = data.Read(buffer, 0, buffer.Length)) != 0
            )
            {
                requestStream.Write(buffer, 0, bytesRead);
                requestStream.Flush();

                send += bytesRead;
                tempTotal += bytesRead;
                this.UIDispatcher.BeginInvoke(delegate()
                {
                    OnProgressChanged();
                });
            }

            requestStream.Close();
            webRequest.BeginGetResponse(new AsyncCallback(ReadHttpResponseCallback), webRequest);
        }

        private void ReadHttpResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
            StreamReader reader = new StreamReader(webResponse.GetResponseStream());

            string responsestring = reader.ReadToEnd();
            reader.Close();

            if (send < data.Length)
            {
                StartUpload();
            }
            else
            {
                data.Close();
                data.Dispose();
            }
        }
        private void OnProgressChanged()
        {
            double progress = send / (double)(data.Length);

            //this.lblPrecentage.Text = (Math.Round(progress, 2) * 100).ToString() + "%";
            var value = (double)progress * this.Width;
            this.lblPrecentage.Text = (send / 1024 / 1024).ToString("0.0").Replace(".0", "") + "MB " + (progress * 100).ToString("0.0").Replace(".0", "") + "%";

            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();
            frames.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(0),
                Value = rectWidthTemp
            });

            frames.KeyFrames.Add(new EasingDoubleKeyFrame()
            {
                KeyTime = TimeSpan.FromSeconds(1),
                Value = this.Width * progress
            });

            rectWidthTemp = this.Width * progress;

            Storyboard.SetTarget(frames, rect);
            Storyboard.SetTargetProperty(frames, new PropertyPath("(FrameworkElement.Width)"));
            Storyboard story = new Storyboard() { Children = { frames } };

            story.Begin();
        }

        #endregion
    }
}
