using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using System.IO;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;

namespace CalChecker
{
    public partial class TestCaller : System.Web.UI.Page
    {
        private static readonly string[] scopes = new[] { CalendarService.Scope.Calendar };
        //CalendarService service;

        protected void Page_Load(object sender, EventArgs e)
        {
            string client_id = "getclientidfromcloud";
            string client_secret = "getclientsecretfromcould";
            string useremailaccount = "whosecal";

            //****This code will get credential for google account, it opens a new browser window/tab, so this is only
            //****needed once, per user, on some sort of auth-account-sync side; this will get accesstoken and refreshtoken and you 
            //**** should save it in a longer term data store., maybe in a DB; the accesstoken will expire in 1 hour
            //****we can use refreshtoken to get new accesstoken and update the db.
            UserCredential credential;
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = client_id,
                ClientSecret = client_secret
            }, scopes, useremailaccount, System.Threading.CancellationToken.None, new FileDataStore("Tempo")).Result;

            string keepAccessToken = credential.Token.AccessToken;
            string keepRefreshToken = credential.Token.RefreshToken;

            //should store the keepAccessToken and keepRefreshToken in some sort of longer store
            //todo: shove in session for sample proj



        }
    }
}


        

