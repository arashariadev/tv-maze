using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace MovieCollection.TVMaze
{
    internal static class Helpers
    {
        internal static async Task<string> DownloadJsonAsync(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            Debug.WriteLine(format: "Sending request to: {0}", url);

            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if (httpResponse.StatusCode == (HttpStatusCode)429)
            {
                throw new Exception("API's rate limit exceeded. Please wait a few seconds and try again.");
            }

            using (System.IO.StreamReader streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
            {
                return await streamReader.ReadToEndAsync();
            }
        }
    }
}
