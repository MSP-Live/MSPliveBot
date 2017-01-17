using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MSPliveBot
{
    public class Vk
    {
        public static string PostMessage(string message)
        {
            string url =
                "https://api.vk.com/method/wall.post?access_token=1eeecee15eac0987e40c9ca0a5a172bec2ceeede97aa083f4eb5c1c8baf39db9160686558a4ed47dd4124&=owner_id=-137871833&from_group=1&message=" +
                message;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;

            StreamReader reader = new StreamReader(response.GetResponseStream());

            StringBuilder output = new StringBuilder();
            output.Append(reader.ReadToEnd());
            return output.ToString();
        }
    }
}
