﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//     <manual-edited>
//         The WebTest1Coded.cs file was indeed first generated by a tool,
//         and then was later edited manually to add secure parameters, and to
//         apply the manual updated required IPV4 address required for networking
//         from home at home.klump-pdx.com.
//         This has now worked in this fashion for many years as long as this
//         Web Performance Test recorded script is kept uptodate with the changes
//         if they ever occur at www.webhost4life.com address record edits.
//         -Matthew (J)ames Klump
//     </manual-edited>
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeDNSUpdate
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.WebTesting;
    using Microsoft.VisualStudio.TestTools.WebTesting.Rules;
    using System.Data;
    using System.Data.SqlClient;

    [DataSource("MySecureParameters", "System.Data.SqlClient", "Data Source=(local);Initial Catalog=MySecureParameters;Integrated Security=True",
        Microsoft.VisualStudio.TestTools.WebTesting.DataBindingAccessMethod.Sequential,
        Microsoft.VisualStudio.TestTools.WebTesting.DataBindingSelectColumns.SelectOnlyBoundColumns, "PersonalInformation")]
    [DataBinding("MySecureParameters", "PersonalInformation", "UserName", "MySecureParameters.PersonalInformation.UserName")]
    [DataBinding("MySecureParameters", "PersonalInformation", "Password", "MySecureParameters.PersonalInformation.Password")]
    public class WebTest1Coded : WebTest
    {

        public WebTest1Coded()
        {
            this.Context.Add("WebServer1", "http://www.webhost4life.com");
            this.Context.Add("WebServer2", "https://secure.webhost4life.com");
            this.Context.Add("WebServer3", "http://eig.evergage.com");
            this.Context.Add("WebServer4", "http%3A%2F%2Fwww.webhost4life.com");
            this.PreAuthenticate = true;
        }

        public override IEnumerator<WebTestRequest> GetRequestEnumerator()
        {
            // Initialize validation rules that apply to all requests in the WebTest
            if ((this.Context.ValidationLevel >= Microsoft.VisualStudio.TestTools.WebTesting.ValidationLevel.Low))
            {
                ValidateResponseUrl validationRule1 = new ValidateResponseUrl();
                this.ValidateResponse += new EventHandler<ValidationEventArgs>(validationRule1.Validate);
            }
            if ((this.Context.ValidationLevel >= Microsoft.VisualStudio.TestTools.WebTesting.ValidationLevel.Low))
            {
                ValidationRuleResponseTimeGoal validationRule2 = new ValidationRuleResponseTimeGoal();
                validationRule2.Tolerance = 0D;
                this.ValidateResponseOnPageComplete += new EventHandler<ValidationEventArgs>(validationRule2.Validate);
            }

            WebTestRequest request1 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/default.asp"));
            request1.ThinkTime = 1;
            request1.ExpectedResponseUrl = (this.Context["WebServer1"].ToString() + "/join/index.bml?AffID=1");
            request1.QueryStringParameters.Add("refid", "100", false, false);
            yield return request1;
            request1 = null;

            WebTestRequest request2 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/"));
            request2.ThinkTime = 23;
            request2.Encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            request2.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/join/index.bml?AffID=1")));

            ExtractHiddenFields extractionRule1 = new ExtractHiddenFields();
            extractionRule1.Required = true;
            extractionRule1.HtmlDecode = true;
            extractionRule1.ContextParameterName = "1";
            request2.ExtractValues += new EventHandler<ExtractionEventArgs>(extractionRule1.Extract);
            yield return request2;
            request2 = null;

            WebTestRequest request3 = new WebTestRequest((this.Context["WebServer2"].ToString() + "/secureLogin"));
            request3.Method = "POST";
            request3.Encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            request3.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/")));
            FormPostHttpBody request3Body = new FormPostHttpBody();
            request3Body.FormPostParameters.Add("destination", this.Context["$HIDDEN1.destination"].ToString());

            request3Body.FormPostParameters.Add("credential_0", this.Context["MySecureParameters.PersonalInformation.UserName"].ToString());
            request3Body.FormPostParameters.Add("credential_1", this.Context["MySecureParameters.PersonalInformation.Password"].ToString());

            request3Body.FormPostParameters.Add("Log In", "Log In");
            request3.Body = request3Body;
            yield return request3;
            request3 = null;

            WebTestRequest request4 = new WebTestRequest((this.Context["WebServer2"].ToString() + "/controlpanel/"));
            request4.Encoding = System.Text.Encoding.GetEncoding("Windows-1252");
            request4.ExpectedResponseUrl = "http://www.webhost4life.com/controlpanel/beta/";

            WebTestRequest request4Dependent1 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/generalAppC/scriptcat/6b3282df631c62b8766798834a5232db.1"));
            request4Dependent1.ThinkTime = 1;
            request4Dependent1.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/beta/")));
            request4.DependentRequests.Add(request4Dependent1);

            WebTestRequest request4Dependent2 = new WebTestRequest((this.Context["WebServer3"].ToString() + "/api/dataset/vDeckTest/siteConfig"));
            request4Dependent2.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/beta/")));
            request4Dependent2.QueryStringParameters.Add("_ak", "eig", false, false);
            request4Dependent2.QueryStringParameters.Add("callback", "getSiteConfigCallback", false, false);
            request4.DependentRequests.Add(request4Dependent2);

            WebTestRequest request4Dependent3 = new WebTestRequest((this.Context["WebServer3"].ToString() + "/twreceiver"));
            request4Dependent3.ThinkTime = 6;
            request4Dependent3.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/beta/")));
            request4Dependent3.QueryStringParameters.Add("_callback", "jQuery18009525464451500745_1377204896915", false, false);
            request4Dependent3.QueryStringParameters.Add("_ak", "eig", false, false);
            request4Dependent3.QueryStringParameters.Add("_ds", "vDeckTest", false, false);
            request4Dependent3.QueryStringParameters.Add("_r", "385428", false, false);
            request4Dependent3.QueryStringParameters.Add("_clientTS", "1377204897178", false, false);
            request4Dependent3.QueryStringParameters.Add("company", "WebHost4Life", false, false);
            request4Dependent3.QueryStringParameters.Add("userId", "mklump", false, false);
            request4Dependent3.QueryStringParameters.Add("action", "Customer+Login", false, false);
            request4Dependent3.QueryStringParameters.Add("url", (this.Context["WebServer4"].ToString() + "%2Fcontrolpanel%2Fbeta%2F"), false, false);
            request4Dependent3.QueryStringParameters.Add("title", "Control+Panel", false, false);
            request4Dependent3.QueryStringParameters.Add("_", "1377204897178", false, false);
            request4.DependentRequests.Add(request4Dependent3);
            yield return request4;
            request4 = null;

            WebTestRequest request5 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/controlpanel/my_favorites/update_favorites.cmp"));
            request5.Method = "POST";
            request5.Headers.Add(new WebTestRequestHeader("Accept", "*/*"));
            request5.Headers.Add(new WebTestRequestHeader("X-Requested-With", "XMLHttpRequest"));
            request5.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/beta/")));

            FormPostHttpBody request5Body = new FormPostHttpBody();
            request5Body.FormPostParameters.Add("async", "false");
            request5Body.FormPostParameters.Add("recent", " DomainCentral|SEP|/controlpanel/domaincentral/");
            request5.Body = request5Body;
            yield return request5;
            request5 = null;

            WebTestRequest request6 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/"));
            request6.ThinkTime = 1;
            request6.Encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            request6.ExpectedResponseUrl = (this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/index.bml");
            request6.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/beta/")));

            WebTestRequest request6Dependent1 = new WebTestRequest((this.Context["WebServer3"].ToString() + "/api/dataset/vDeckTest/siteConfig"));
            request6Dependent1.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/index.bml")));
            request6Dependent1.QueryStringParameters.Add("_ak", "eig", false, false);
            request6Dependent1.QueryStringParameters.Add("callback", "getSiteConfigCallback", false, false);
            request6.DependentRequests.Add(request6Dependent1);

            WebTestRequest request6Dependent2 = new WebTestRequest((this.Context["WebServer3"].ToString() + "/twreceiver"));
            request6Dependent2.ThinkTime = 11;
            request6Dependent2.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/index.bml")));
            request6Dependent2.QueryStringParameters.Add("_callback", "jQuery18005031814928228379_1377204906716", false, false);
            request6Dependent2.QueryStringParameters.Add("_ak", "eig", false, false);
            request6Dependent2.QueryStringParameters.Add("_ds", "vDeckTest", false, false);
            request6Dependent2.QueryStringParameters.Add("_r", "273941", false, false);
            request6Dependent2.QueryStringParameters.Add("_clientTS", "1377204906938", false, false);
            request6Dependent2.QueryStringParameters.Add("urlref", (this.Context["WebServer4"].ToString() + "%2Fcontrolpanel%2Fbeta%2F"), false, false);
            request6Dependent2.QueryStringParameters.Add("company", "WebHost4Life", false, false);
            request6Dependent2.QueryStringParameters.Add("userId", "mklump", false, false);
            request6Dependent2.QueryStringParameters.Add("action", "Purchase+Domains+from+DomainCentral", false, false);
            request6Dependent2.QueryStringParameters.Add("url", (this.Context["WebServer4"].ToString() + "%2Fcontrolpanel%2Fdomaincentral%2Fbeta%2Findex.bml"), false, false);
            request6Dependent2.QueryStringParameters.Add("title", "DomainCentral", false, false);
            request6Dependent2.QueryStringParameters.Add("_", "1377204906942", false, false);
            request6.DependentRequests.Add(request6Dependent2);

            WebTestRequest request6Dependent3 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/overview.cmp"));
            request6Dependent3.ThinkTime = 8;
            request6Dependent3.Method = "POST";
            request6Dependent3.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/index.bml")));
            request6Dependent3.QueryStringParameters.Add("domain", "klump-pdx.com", false, false);
            request6Dependent3.QueryStringParameters.Add("amp;section", "overview", false, false);
            request6Dependent3.QueryStringParameters.Add("amp;inreg", "1", false, false);
            request6Dependent3.QueryStringParameters.Add("", "amp;undefined", false, false);

            FormPostHttpBody request6Dependent3Body = new FormPostHttpBody();
            request6Dependent3Body.FormPostParameters.Add("1", "1");
            request6Dependent3.Body = request6Dependent3Body;
            request6.DependentRequests.Add(request6Dependent3);

            WebTestRequest request6Dependent4 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/dns.cmp"));
            request6Dependent4.ThinkTime = 50;
            request6Dependent4.Method = "POST";
            request6Dependent4.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/index.bml")));
            request6Dependent4.QueryStringParameters.Add("domain", "klump-pdx.com", false, false);
            request6Dependent4.QueryStringParameters.Add("amp;section", "dns", false, false);
            request6Dependent4.QueryStringParameters.Add("amp;inreg", "1", false, false);
            request6Dependent4.QueryStringParameters.Add("", "amp;undefined", false, false);

            FormPostHttpBody request6Dependent4Body = new FormPostHttpBody();
            request6Dependent4Body.FormPostParameters.Add("1", "1");
            request6Dependent4.Body = request6Dependent4Body;
            request6.DependentRequests.Add(request6Dependent4);

            WebTestRequest request6Dependent5 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/m.cmp"));
            request6Dependent5.ThinkTime = 45;
            request6Dependent5.Method = "POST";
            request6Dependent5.Headers.Add(new WebTestRequestHeader("X-Requested-With", "XMLHttpRequest"));
            request6Dependent5.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() + "/controlpanel/domaincentral/beta/index.bml")));

            FormPostHttpBody request6Dependent5Body = new FormPostHttpBody();
            request6Dependent5Body.FormPostParameters.Add("cmd", "dns");
            request6Dependent5Body.FormPostParameters.Add("domain", "klump-pdx.com");
            request6Dependent5Body.FormPostParameters.Add("params",
                @";count=9;action=editA;" +
                "id3=199459390;name3=mx;content3=66.96.142.50;" +
                "id4=199459388;name4=mx;content4=66.96.142.51;" +
                "id5=199459387;name5=webmail;content5=66.96.146.20;" +
                "id6=199459389;name6=email;content6=66.96.146.20;" +
                "id7=199459385;name7=*;content7=66.96.146.85;" +
                "id8=199459384;name8=klump-pdx.com;content8=66.96.146.85;" +

                "id9=600135075;name9=home;content9=" + GetIPv4() + ";"); // Perform actual IP Address Update here.

            request6Dependent5.Body = request6Dependent5Body;
            request6.DependentRequests.Add(request6Dependent5);
            yield return request6;
            request6 = null;

            WebTestRequest request7 = new WebTestRequest((this.Context["WebServer1"].ToString() + "/utils/logout.bml"));
            request7.Encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            request7.ExpectedResponseUrl = (this.Context["WebServer1"].ToString() + "/logout.bml?");
            request7.Headers.Add(new WebTestRequestHeader("Referer", (this.Context["WebServer1"].ToString() +
                "/controlpanel/domaincentral/beta/index.bml?error=You+must+select+at+least+one+domain+to+continue.")));
            yield return request7;
            request7 = null;
        }

        /// <summary>
        /// Helper function retrieves specifically the most current up to date detected Wide Area Network IPv4 Address.
        /// </summary>
        /// <returns>Returns the detected IPv4 address that was saved in the Data Base MySecureParameters.</returns>
        private string GetIPv4()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=MATT-SERVER;Initial Catalog=MySecureParameters;Integrated Security=True";
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetLoginAndIP";
            command.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50,
                ParameterDirection.Input, false, (byte)10, (byte)0, "", DataRowVersion.Current,
                this.Context["WebTestConnection.PersonalInformation.UserName"].ToString()));
            command.Parameters.Add(new SqlParameter("@RETURN_VALUE", SqlDbType.NVarChar, 50,
                ParameterDirection.Output, false, ((byte)10), ((byte)0), "", DataRowVersion.Current,
                string.Empty));
            int rowsAffected = command.ExecuteNonQuery();
            return (string)command.Parameters["@RETURN_VALUE"].Value;
        }
    }
}