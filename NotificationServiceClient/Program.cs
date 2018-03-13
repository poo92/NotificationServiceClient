using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json;

namespace NotificationServiceClient
{
    class Program
    {
        private static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var tokenClient = new TokenClient(
                                "https://oauthdev.adradev.com/identity/connect/token",
                                "Setup",
                                "l1rHd7I3zkjTMneZ16DS");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("NotificationHub");
            //Debug.WriteLine(tokenResponse.AccessToken);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            // here operatorengagementID should be passed as RecipientId : NOT a operator Guid
            Guid RecipientId = new Guid("C14E7822-D093-4607-842E-FBBD81142645");

            var notificationRequest = new NotificationRequest()
            {
                RecipientId = RecipientId,
                ApplicationId = "1",
                Subject = "Ownership verification of Adra user account for l l",
                FormattedMessageType = 0,
                FormattedMessage = "<h3>Welcome l l</h3>\r\n<p>This email was sent for you to verify that you are the true owner of the email address registered with your Adra account.</p>\r\n<table width=\"100%\">\r\n    <tr>\r\n        <td align=\"center\" class=\"buttonContainer\">\r\n            <table class=\"button\">\r\n                <tr>\r\n                    <td class=\"buttonContent\">\r\n                        <a href=\"https://localhost:44322/auth/Activation?username=newusertestforemail%40gmail.com&amp;activationToken=EAAAADnZQKhV6c1O8VKL9H9mm7jUxHmx5RioecvKKEEfOZ4F1sRVvnt1hNfoYMRFSH5BqM%2BiF3BKNjp7Nunkj8mazdk%3D\" target=\"_blank\">Activate my account</a>\r\n                    </td>\r\n                </tr>\r\n            </table>\r\n        </td>\r\n    </tr>\r\n</table>",
                PlainMessage = "Welcome l l, \r\nThis message was sent to you in order to verify that you are the true owner of the email address registered with your Adra account. \r\nCopy the link given below on to a browser in order to activate your account: https://localhost:44322/auth/Activation?username=newusertestforemail%40gmail.com&activationToken=EAAAADnZQKhV6c1O8VKL9H9mm7jUxHmx5RioecvKKEEfOZ4F1sRVvnt1hNfoYMRFSH5BqM%2BiF3BKNjp7Nunkj8mazdk%3D"
            };

            var data = JsonConvert.SerializeObject(notificationRequest);

            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            var _url = new Uri("https://notificationhubdev.adradev.com/v3/internal/notifications").ToString();
            _httpRequest.Method = new System.Net.Http.HttpMethod("POST");
            _httpRequest.RequestUri = new System.Uri(_url);
            _httpRequest.Content = new System.Net.Http.StringContent(data, System.Text.Encoding.UTF8);
            _httpRequest.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json; charset=utf-8");

            var result = await client.SendAsync(_httpRequest);
            //Debug.WriteLine(result.StatusCode);
            //Debug.WriteLine(result.ToString());

        }
    }
}