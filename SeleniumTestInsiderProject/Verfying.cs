using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace SeleniumTestInsiderProject
{
    class Verfying
    {
        HttpWebRequest webRequest;
        HttpWebResponse response;
        HttpStatusCode statusCode;
        public Boolean HttpRequestStatusCode(String url) {
            webRequest = (HttpWebRequest)WebRequest.Create(url);
            
            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                statusCode = response.StatusCode;
                Console.WriteLine(statusCode.ToString());
                return true;

            }

            catch (WebException we)
            {
                Console.WriteLine(we.Status);
                return false;
                

            }

            


        }
    }
}
