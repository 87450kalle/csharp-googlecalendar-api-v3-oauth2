using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace CalChecker
{
    public partial class simpleEvent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string client_id = "getclientidfromcloud";
            string client_secret = "getclientsecretfromcould";
            string useremailaccount = "whosecal";

            ////////////////////****generate a sample class for event/json.... did not complete
            ////////////////////gEvent evt = new gEvent();
            ////////////////////evt.summary = "Title";
            ////////////////////evt.description = "desc";

            ////////////////////var serializer = new JavaScriptSerializer();
            ////////////////////var json = serializer.Serialize(evt);



            //****This code makes call to API using Authorization passed in header for email account in http request
            //****this call just getting list of events in calender - > GET
            //****to create event, we can POST along with correct json object
            //****TODO:switch out list of events to posting an actual event.
            string accesstoken = "usersaccesstoken";

            var httpWebRequest1 = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/calendar/v3/calendars/" + WebUtility.HtmlEncode(useremailaccount) + "/events");
            httpWebRequest1.ContentType = "text/json";
            httpWebRequest1.Method = "GET";
            httpWebRequest1.Headers["Authorization"] = "Bearer " + accesstoken;
            var httpResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();
            using (var streamReader = new StreamReader(httpResponse1.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }



            //****TODO: add handler to do a refresh when/as needed, will assume in error state for code below
            //****in call to POST event, we need to handle error; if token expired, we need to use refreshtoken
            //****and get a new access token, and then save to database this new token... 
            //****the following will get a new token
            string refreshtoken = "usersrefreshtocken";

            var httpWebRequest2 = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            string postString = string.Format("client_id=" + client_id + "&client_secret=" + client_secret + "&refresh_token=" + refreshtoken + "&grant_type=refresh_token");

            httpWebRequest2.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest2.Method = "POST";
            httpWebRequest2.ContentLength = postString.Length;
            httpWebRequest2.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            httpWebRequest2.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            StreamWriter requestWriter = new StreamWriter(httpWebRequest2.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();

            var httpResponse2 = (HttpWebResponse)httpWebRequest2.GetResponse();
            using (var streamReader = new StreamReader(httpResponse2.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                ///result should have your new token
                ///TODO: write the call to get it out
            }
        }
    }
    //SAMPLE of json call needed... not correct yet
//            {
// "end": {
//  "date": "2013-12-12",
//  "dateTime": "2013-12-12T15:00:00",
//  "timeZone": "Americas/New_York"
// },
// "start": {
//  "date": "2013-12-12",
//  "dateTime": "2013-12-12T15:00:00",
//  "timeZone": "Americas/New_York"
// },
// "summary": "Title",
// "description": "Body"
//}

            //int returnCode = client.executeMethod(method);
        //}

        public class gEvent
        {
            public string summary;
            public string description;
            public EventDbo start;
            public EventDbo end;

        }
        public class EventDbo
        {
            public string date;
            public string dateTime;
            public string timeZone;
        }

        

    }