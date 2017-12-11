using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace ASPNetMVCDemo.Models
{
    public class ExistingFilesModel
    {
        private DataTable UploadedFiles;

        public ExistingFilesModel()
        {
            UploadedFiles = new DataTable();
            DataColumn IDColumn = UploadedFiles.Columns.Add("ID", Type.GetType("System.Int32"));
            IDColumn.AutoIncrement = true;
            IDColumn.AutoIncrementSeed = 1;
            IDColumn.AutoIncrementStep = 1;
            DataColumn[] keys = new DataColumn[1];
            keys[0] = IDColumn;
            UploadedFiles.PrimaryKey = keys;

            UploadedFiles.Columns.Add("File Name", Type.GetType("System.String"));
            UploadedFiles.Columns.Add("File Size", Type.GetType("System.Int32"));
            UploadedFiles.Columns.Add("Context Type", Type.GetType("System.String"));
            UploadedFiles.Columns.Add("Time Uploadeded", Type.GetType("System.DateTime"));
            UploadedFiles.Columns.Add("File Data", Type.GetType("System.Byte[]"));
        }

        public DataTable GetUploadedFiles() { return UploadedFiles; }

        public void AddAFile(string FileName, int Size, string ContentType, Byte[] FileData)
        {
            DataRow row = UploadedFiles.NewRow();
            UploadedFiles.Rows.Add(row);

            row["File Name"] = FileName;
            row["File Size"] = Size;
            row["Context Type"] = ContentType;
            row["Time Uploadeded"] = System.DateTime.Now;
            row["File Data"] = FileData;
        }

        public void DeleteAFile(int ID)
        {
            DataRow RowToDelete = UploadedFiles.Rows.Find(ID);
            if (RowToDelete != null)
            {
                UploadedFiles.Rows.Remove(RowToDelete);
            }
        }
    }
}
