using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMES.AspNetFramework.MVC
{
    public class LinkModel
    {
        public string Url
        { get; set; }

        public string Title
        { get; set; }

        public string Target
        { get; set; }

        public LinkModel()
        {

        }

        public LinkModel(string url, string title, string target)
        {
            Url = url;
            Title = title;
            Target = target;
        }
    }
}
