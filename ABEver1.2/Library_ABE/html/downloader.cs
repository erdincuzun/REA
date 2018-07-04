using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;

namespace Library_ABE.html
{
    public static class downloader
    {
        public static string GetHtmlContent(string url, Encoding encode)
        {
            try
            {
                string htmlContentText = String.Empty;

                HttpWebRequest httpWebRequest = null;

                HttpWebResponse httpWebResponse = null;

                StreamReader streamReader = null;

                try
                {

                    httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                    httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    httpWebRequest.Timeout = 5000;


                    streamReader = new StreamReader(httpWebResponse.GetResponseStream(), encode); //Encoding.Default veya Encoding.UTF8
                    //Encoding.Default

                    htmlContentText = streamReader.ReadToEnd();

                    streamReader.Close();

                    httpWebResponse.Close();
                }

                catch { }

                finally
                {

                    httpWebResponse.Close();

                    streamReader.Close();
                }

                return htmlContentText;
            }
            catch
            {
                return "Hata var!!!";
            }

        }
    }
}
