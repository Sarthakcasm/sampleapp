using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.IO;
using System.Web.Mvc;
using ASPNetMVCDemo.Models;

namespace ASPNetMVCDemo.Controllers
{
    public class FileUploadExampleController : Controller
    {
        private ExistingFilesModel GetModelFromSession()
        {
            ExistingFilesModel theModel = (ExistingFilesModel)Session["ExistingFilesModel"];
            if (theModel == null)
            {
                theModel = new ExistingFilesModel();
                DataTable aTable = theModel.GetUploadedFiles();
                aTable.Columns["File Data"].ColumnMapping = MappingType.Hidden;

                Session["ExistingFilesModel"] = theModel;
            }

            return theModel;
        }

        public ActionResult FileUpload()
        {
            ExistingFilesModel theModel = GetModelFromSession();
            DataTable aTable = theModel.GetUploadedFiles();

            ViewData["ExistingFileList"] = aTable.DefaultView;
            return View();
        }

        public ActionResult DeleteFile()
        {
            string ID = Request.QueryString["ID"];
            int intID;

            if (!Int32.TryParse(ID, out intID))
            {
                ViewData["ERROR"] = "Please provide a valid student ID";
                return View("../Shared/Error");
            }

            ExistingFilesModel theModel = GetModelFromSession();
            theModel.DeleteAFile(intID);

            return RedirectToAction("FileUpload");
        }

        public ActionResult SaveFile()
        {
            ExistingFilesModel theModel = GetModelFromSession();
            HttpPostedFileBase File = Request.Files["FileToLoad"];

            if (File != null)
            {
                int Size = File.ContentLength;

                if (Size <= 0)
                {
                    ViewData["ERROR"] = "You uploaded an empty file, please browse a valid file to upload";
                    return View("../Shared/Error");
                }

                string FileName = File.FileName;
                int Position = FileName.LastIndexOf("\\");
                FileName = FileName.Substring(Position + 1);
                string ContentType = File.ContentType;
                byte[] FileData = new byte[Size];
                File.InputStream.Read(FileData, 0, Size);

                theModel.AddAFile(FileName, Size, ContentType, FileData);
            }

            return RedirectToAction("FileUpload");
        }

        public ActionResult GetAFile()
        {
            string Attachment = Request.QueryString["ATTACH"];
            string ID = Request.QueryString["ID"];
            int intID;

            if (!Int32.TryParse(ID, out intID))
            {
                ViewData["ERROR"] = "Please provide a valid student ID";
                return View("../Shared/Error");
            }

            ExistingFilesModel theModel = GetModelFromSession();
            DataTable aTable = theModel.GetUploadedFiles();
            DataRow FileRow = aTable.Rows.Find(intID);
            if (FileRow == null)
            {
                ViewData["ERROR"] = "Please provide a valid student ID";
                return View("../Shared/Error");
            }

            string FileName = (string) FileRow["File Name"];
            int Size = (int) FileRow["File Size"];
            string ContentType = (string) FileRow["Context Type"];
            Byte[] Data = (Byte[]) FileRow["File Data"];


            Response.ContentType = ContentType;
            StringBuilder SB = new StringBuilder();
            if (Attachment == "YES")
            {
                SB.Append("attachment; ");
            }
            SB.Append("filename=");
            SB.Append(FileName);

            Response.AddHeader("Content-Disposition", SB.ToString());
            Response.BinaryWrite(Data);
            Response.Flush();
            Response.End();

            return new EmptyResult();
        }

    }
}
