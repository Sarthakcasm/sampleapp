using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ASPNetMVCDemo.Utilities;

namespace ASPNetMVCDemo.Views.FileUploadExample
{
    public class FileUpload : System.Web.Mvc.ViewPage
    {
        protected Literal litApplicationName;
        protected Literal litAuthorInformation;
        protected Literal litExistingFiles;
        protected Literal litLinkTojQuery;
        protected Literal litAppStyle;
        protected HtmlInputHidden hidFileUploadAction;
        protected HtmlInputHidden hidImageBase;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            
            Models.ApplicationSettings appsettings = Models.ApplicationSettings.GetInstance();

            litApplicationName.Text = appsettings.ApplicationName;
            hidFileUploadAction.Value
                = ApplicationUtility.FormatURL("/FileUploadExample/SaveFile");
            hidImageBase.Value = ApplicationUtility.FormatURL("/FileUploadExample/GetAFile");

            litLinkTojQuery.Text = ApplicationUtility.jQueryLink();
            litAppStyle.Text = ApplicationUtility.AppStylelink();
            
            StringBuilder SB = new StringBuilder();
            SB.Append("Developed by ");
            SB.Append(appsettings.Author);
            SB.Append(" on ");
            SB.Append(appsettings.DevelopmentTime);
            litAuthorInformation.Text = SB.ToString();

            SB.Remove(0, SB.Length);
            DataView FileView = (DataView)ViewData["ExistingFileList"];
            SB.Append("<table style=\"width: 99%;\" ");
            SB.Append("rules=\"all\" border=\"1px\" ");
            SB.Append("cellspacing=\"0px\" cellpadding=\"4px\">");

            SB.Append("<tr style=\"background-color: Silver; color: white; ");
            SB.Append("font-weight: bold\">");
            foreach (DataColumn aColumn in FileView.Table.Columns)
            {
                if (aColumn.ColumnMapping == MappingType.Hidden)
                {
                    continue;
                }

                SB.Append("<td>");
                SB.Append(aColumn.ColumnName);
                SB.Append("</td>");
            }
            SB.Append("<td>&nbsp;</td>");
            SB.Append("<td>&nbsp;</td>");
            SB.Append("</tr>");

            foreach (DataRowView aRowView in FileView)
            {
                SB.Append("<tr>");
                foreach (DataColumn aColumn in FileView.Table.Columns)
                {
                    if (aColumn.ColumnMapping == MappingType.Hidden)
                    {
                        continue;
                    }

                    SB.Append("<td>");
                    if (aColumn.ColumnName == "ID")
                    {
                        SB.Append("<span class=\"ImagePopLink\">");
                        SB.Append(aRowView[aColumn.ColumnName].ToString());
                        SB.Append("</span>");
                    }
                    else
                    {
                        SB.Append(aRowView[aColumn.ColumnName].ToString());
                    }

                    SB.Append("</td>");
                }

                string ID = aRowView["ID"].ToString();
                SB.Append("<td>");
                SB.Append("<a href=\"");
                SB.Append(ApplicationUtility.FormatURL("/FileUploadExample/GetAFile"));
                SB.Append("?ATTACH=YES&ID=");
                SB.Append(ID);
                SB.Append("\">Download this file</a>");
                SB.Append("</td>");

                SB.Append("<td>");
                SB.Append("<a href=\"");
                SB.Append(ApplicationUtility.FormatURL("/FileUploadExample/DeleteFile"));
                SB.Append("?ID=");
                SB.Append(ID);
                SB.Append("\">Delete this file</a>");
                SB.Append("</td>");

                SB.Append("</tr>");
            }

            SB.Append("</table>");

            litExistingFiles.Text = SB.ToString();
        }
    }
}
