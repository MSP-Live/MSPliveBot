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
                 string.Format(
              "https://api.vk.com/method/wall.post?owner_id={0}&from_group=0&signed=0&access_token={1}&message={2}",
                  "-137865431", "c68cb90b2eeab6c27ec8d21aa62b7fb56e8f99e7988a91fcd9b2d5cf1ac3f34f931f10d058431dc6ea247", message);

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
