using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ASPNetMVCDemo.Utilities;

namespace ASPNetMVCDemo.Views.Shared
{
    public class Error : System.Web.Mvc.ViewPage
    {
        protected HtmlInputHidden hidHomeURL;
        protected Literal litApplicationName;
        protected Literal litAuthorInformation;
        protected Literal litErrorMessage;
        protected Literal litLinkTojQuery;
        protected Literal litAppStyle;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            litLinkTojQuery.Text = ApplicationUtility.jQueryLink();
            litAppStyle.Text = ApplicationUtility.AppStylelink();
            hidHomeURL.Value = ApplicationUtility.FormatURL("/FileUploadExample/FileUpload");
            Models.ApplicationSettings appsettings = Models.ApplicationSettings.GetInstance();

            litApplicationName.Text = appsettings.ApplicationName;

            StringBuilder SB = new StringBuilder();
            SB.Append("Developed by ");
            SB.Append(appsettings.Author);
            SB.Append(" on ");
            SB.Append(appsettings.DevelopmentTime);
            litAuthorInformation.Text = SB.ToString();

            litErrorMessage.Text = ViewData["ERROR"].ToString();
        }
    }
}
