using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicSite.Models
{
    public class CustomMessage
    {
        public string _Message { get; set; }
        public string Title { get; set; }

        public CustomMessage(string title, string message)
        {
            Title = title;
            _Message = message;
        }
    }
}