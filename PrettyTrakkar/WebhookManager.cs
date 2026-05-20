using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PrettyTrakkar
{
    class Webhook
    {
        public string Name;
        public string AvatarURL;
        public string Url;

        private static readonly HttpClient client = new HttpClient();
        public async void SendMessage(string msg)
        {
            try
            {
                string targetUrl = Url.Trim();
                if (!targetUrl.Contains("with_components=true"))
                {
                    if (targetUrl.Contains("?"))
                    {
                        targetUrl += "&with_components=true";
                    }
                    else
                    {
                        targetUrl += "?with_components=true";
                    }
                }

                var content = new StringContent(msg, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(targetUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    UnityEngine.Debug.LogError($"[Pretty Trakkar] Webhook message failed | {response.StatusCode}: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"[Pretty Trakkar] Caught exception while sending a message: {ex}");
            }
        } 
    }
}
