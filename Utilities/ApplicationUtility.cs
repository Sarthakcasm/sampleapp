using System;
using System.Text;
using ASPNetMVCDemo.Models;

namespace ASPNetMVCDemo.Utilities
{
    public static class ApplicationUtility
    {
        public static string FormatURL(string PathWithoutVirtualDirectoryName)
        {
            ApplicationSettings appInfomation
                = ApplicationSettings.GetInstance();
            string DeploymentVirtualDirectory
                = appInfomation.DeploymentVirtualDirectory;

            if (DeploymentVirtualDirectory == "")
            {
                return PathWithoutVirtualDirectoryName;
            }

            StringBuilder SB = new StringBuilder();
            SB.Append("/");
            SB.Append(appInfomation.DeploymentVirtualDirectory);
            SB.Append("/");
            SB.Append(PathWithoutVirtualDirectoryName);

            return SB.ToString();
        }

        public static string jQueryLink()
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<script src=\"");
            SB.Append(ApplicationUtility.FormatURL("/Scripts/jquery-1.4.1.min.js"));
            SB.Append("\" type=\"text/javascript\"></script>");

            return SB.ToString();
        }

        public static string AppStylelink()
        {
           StringBuilder SB = new StringBuilder();
            SB.Append("<link href=\"");
            SB.Append(ApplicationUtility.FormatURL("/Styles/AppStyles.css"));
            SB.Append("\" rel=\"stylesheet\" type=\"text/css\" />");

            return SB.ToString();
        }
    }
}