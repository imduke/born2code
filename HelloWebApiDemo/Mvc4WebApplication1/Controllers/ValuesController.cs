using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc4WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {

        private static List<string> data = InitList();

        private static List<string> InitList()
        {
            var myList = new List<string>() { "value1", "value2", "value3", "value4"};

            return myList;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return data;
        }

        // GET api/values/5
        public HttpResponseMessage Get(int id)
        {
            if (data.Count > 0)
            {
                Request.CreateResponse<string>(HttpStatusCode.OK, data[id]);
            }
            else 
            {
                Request.CreateErrorResponse(HttpStatusCode.NotFound, "Item Not Found");
            }

            return Request.CreateResponse();
        }

        // POST api/values
        public HttpResponseMessage Post([FromBody]string value)
        {
            data.Add(value);
            var msg = Request.CreateResponse(HttpStatusCode.Created);
            msg.Headers.Location = new Uri(Request.RequestUri + (data.Count-1).ToString());
            return msg;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            data[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            data.RemoveAt(id);
        }
    }
}