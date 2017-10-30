using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace aca_helper
{
    [Serializable]
    public class TranslationService
    {
        private string AccessToken { get; set; }
        private DateTime AccessTokenCreated { get; set; }

        public TranslationService()
        {
            this.AccessToken = this.GetAccessToken();
        }

        private string GetAccessToken()
        {
            try
            {
                string accessToken;
                WebRequest accessTokenRequest = WebRequest.Create("https://api.cognitive.microsoft.com/sts/v1.0/issueToken");
                accessTokenRequest.Method = "POST";
                accessTokenRequest.Headers.Add("Ocp-Apim-Subscription-Key", "165633e45bb544eca10f0d63c3486145");
                accessTokenRequest.ContentLength = 0;
                WebResponse accessTokenResponse = accessTokenRequest.GetResponse();
                Stream dataStream = accessTokenResponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                accessToken = responseFromServer;
                reader.Close();
                accessTokenResponse.Close();
                return accessToken;
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                }

                return "";
            }
        }

        public string DetectLanguage(string text)
        {
            if (!string.IsNullOrEmpty(this.AccessToken))
            {
                var currentTime = new DateTime();

                if (currentTime.Subtract(this.AccessTokenCreated).TotalMinutes >= 10)
                {
                    this.GetAccessToken();
                }

                string textToTranslate = text;

                var queryParameters = String.Format("?appid=Bearer {0}&text={1}", this.AccessToken, textToTranslate);

                WebRequest translationRequest = WebRequest.Create("https://api.microsofttranslator.com/v2/http.svc/Detect" + queryParameters);
                translationRequest.Method = "GET";

                try
                {
                    using (WebResponse translationResponse = translationRequest.GetResponse())
                    {
                        Stream translationDataStream = translationResponse.GetResponseStream();
                        StreamReader translationReader = new StreamReader(translationDataStream);
                        string translationResponseFromServer = translationReader.ReadToEnd();
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(translationResponseFromServer);
                        string translation = xmldoc.FirstChild.InnerText;
                        translationReader.Close();
                        translationResponse.Close();
                        return translation;
                    }
                }
                catch (WebException e)
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        return "";
                    }
                }
            }
            else
            {
                return "";
            }
        }

        public string TranslateText(string text, string languageCode)
        {
            if (!string.IsNullOrEmpty(this.AccessToken))
            {
                if (string.IsNullOrEmpty(text))
                {
                    return text;
                }

                var currentTime = new DateTime();

                if (currentTime.Subtract(this.AccessTokenCreated).TotalMinutes >= 10)
                {
                    this.GetAccessToken();
                }

                string textToTranslate = HttpUtility.UrlEncode(text);
                string toLanguage = HttpUtility.UrlEncode(languageCode);

                var queryParameters = String.Format("?appid=Bearer {0}&text={1}&to={2}", this.AccessToken, textToTranslate, toLanguage);

                WebRequest translationRequest = WebRequest.Create("https://api.microsofttranslator.com/v2/http.svc/Translate" + queryParameters);
                translationRequest.Method = "GET";
                try
                {
                    using (WebResponse translationResponse = translationRequest.GetResponse())
                    {
                        Stream translationDataStream = translationResponse.GetResponseStream();
                        StreamReader translationReader = new StreamReader(translationDataStream);
                        string translationResponseFromServer = translationReader.ReadToEnd();
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(translationResponseFromServer);
                        string translation = xmldoc.FirstChild.InnerText;
                        translationReader.Close();
                        translationResponse.Close();
                        return translation;
                    }
                }
                catch (WebException e)
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        return text;
                    }
                }
            }
            else
            {
                return text;
            }
        }

        public List<string> GetLanguagesForTranslate()
        {
            if (!string.IsNullOrEmpty(this.AccessToken))
            {
                var currentTime = new DateTime();

                if (currentTime.Subtract(this.AccessTokenCreated).TotalMinutes >= 10)
                {
                    this.GetAccessToken();
                }

                var queryParameters = String.Format("?appid=Bearer {0}", this.AccessToken);

                WebRequest translationRequest = WebRequest.Create("https://api.microsofttranslator.com/v2/http.svc/GetLanguagesForTranslate" + queryParameters);
                translationRequest.Method = "GET";
                //translationRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                try
                {
                    using (WebResponse translationResponse = translationRequest.GetResponse())
                    {
                        Stream translationDataStream = translationResponse.GetResponseStream();
                        StreamReader translationReader = new StreamReader(translationDataStream);
                        string translationResponseFromServer = translationReader.ReadToEnd();
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.LoadXml(translationResponseFromServer);
                        var languages = new List<string>();

                        foreach (XmlNode childnode in xmldoc.FirstChild.ChildNodes)
                        {
                            languages.Add(childnode.FirstChild.InnerText);
                        }
                        translationReader.Close();
                        translationResponse.Close();
                        return languages;
                    }
                }
                catch (WebException e)
                {
                    using (WebResponse response = e.Response)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)response;
                        return new List<string>();
                    }
                }
            }
            else
            {
                return new List<string>();
            }
        }
    }
}