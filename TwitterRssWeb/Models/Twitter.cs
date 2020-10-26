using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterRssWeb.Models
{
    public class Twitter
    {
        public string guid { get; set; }
        public string title { get; set; }
        public DateTime pubdate { get; set; }
        public DateTime dbhour { get; set; }
        public int checkeded { get; set; }

        //

    }
}