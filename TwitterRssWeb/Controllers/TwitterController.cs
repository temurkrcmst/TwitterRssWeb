using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TwitterRssWeb.Actions;
using TwitterRssWeb.Models;

namespace TwitterRssWeb.Controllers
{
    public class TwitterController : ApiController
    {

        [HttpPost]
        public async Task<Twitter> UptadeTwitter(Twitter guid)
        {
            return await TwitterManager.UptadeTwitter(guid);
        }


        [HttpGet]
        public async Task<List<Twitter>> GetTwitter()
        {
            return await TwitterManager.GetTwitter();
        }
   
    }
}
