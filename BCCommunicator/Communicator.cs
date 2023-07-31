using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCCommunicator
{
    public class Communicator
    {
        /// <summary>
        /// This will retrieve a new access token, given tenantID, clientID, clientSecret, and the refresh token
        /// </summary>
        /// <param name="tenantID">Tenant ID you are using for BC</param>
        /// <param name="clientID">ClientID setup in Active Directory</param>
        /// <param name="clientSecret">Client Secret setup in Active Directory</param>
        /// <param name="refreshToken">Saved refresh token</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Token RefreshToken(string tenantID, string clientID, string clientSecret, string refreshToken)
        {
            // create rest client options and point to the base login.microsoftonline.com
            var options = new RestClientOptions("https://login.microsoftonline.com")
            {
                MaxTimeout = -1,
            };

            // create a rest client
            var client = new RestClient(options);

            // we are going to point to the token endpoint (NOTE: the tenantID is part of this endpoint)
            var request = new RestRequest($"/{tenantID}/oauth2/token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // add the parameters for clientID, secret, granttype (refresh_token), and the refresh_token to use
            request.AddParameter("client_id", clientID);
            request.AddParameter("client_secret", clientSecret);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("refresh_token", refreshToken);

            // execute the REST call
            RestResponse response = client.Execute(request);

            // lets do some error handling
            if (response == null) { throw new Exception("Token Response is Null"); }
            if (response.Content == null) { throw new Exception("Token Content is null"); }
            if (response.StatusCode != System.Net.HttpStatusCode.OK) { throw new Exception($"Status not returned as OK ({response.StatusCode}\n{response.Content})"); }
            Token returnToken = null;

            // serialize the returned token (make sure we do some error handling)
            try
            {
                returnToken = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(response.Content);
            }
            catch(Exception ex)
            {
                throw new Exception($"Return Token could not be deserialized\n{ex.Message}");
            }
            if(returnToken == null) { throw new Exception("Return Token is null"); }

            // return the token
            return returnToken;
        }


        /// <summary>
        /// This is a simple test to test the access token returned when refreshing a token. NOTE:  this is just a simple example that does a GET
        /// </summary>
        /// <param name="tenantID">Tenant ID for your BC instance</param>
        /// <param name="accessToken">Access Token passed back when refreshing a token</param>
        /// <param name="endpoint">What endpoint do you want to call</param>
        /// <returns></returns>
        public string RetrieveData(string tenantID, string accessToken, string endpoint)
        {

            // base server URL for business central API
            var options = new RestClientOptions("https://api.businesscentral.dynamics.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            // build total endpoint
            var request = new RestRequest($"/v2.0/{tenantID}/production/api/v2.0/{endpoint}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            RestResponse response = client.Execute(request);

            // we are going to pass the response.Content string to the caller
            return response.Content;
        }
    }
}
