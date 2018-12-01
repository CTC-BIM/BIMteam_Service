using System;
using System.IO;
using System.Web.Services;

namespace BIMService.WebServices
{
    /// <summary>
    /// Summary description for FilesUploadService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://services.cbimtech.com/WebServices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class FilesUploadService : System.Web.Services.WebService
    {

        /// <summary>
        /// Service Truyền file từ Local đến Server
        /// </summary>
        /// <param name="fileName">Tên File</param>
        /// <param name="f">Data truyền</param>
        /// <param name="dest">Nơi lưu file: BCFs | BEPs | Images | Reports</param>
        /// <param name="ProjectID">Mã dự án</param>
        /// <returns>Thành công sẽ báo: "Upload File Success" - File sẽ được lưu với tên: MaDuAn-FileType-TenFile</returns>
        [WebMethod]
        public string UploadFiles(string fileName, byte[] f, string dest, string ProjectID)
        {
            try
            {
                //TODO:
                //Thay đổi tên FileUpload
                //[Project]-[Type]-[fileName]-[Date]
                MemoryStream ms = new MemoryStream(f);

                string FileNameUpload = ProjectID + "-" + dest + "-" + fileName;
                string path = Server.MapPath("../FileStorage/" + dest + "/" + FileNameUpload);//Cần chỉnh sửa filename trước khi Write
                //Thêm Sercurity
                Path.Combine(path);
                System.Security.AccessControl.FileSecurity fileSecurity = new System.Security.AccessControl.FileSecurity();
                File.SetAccessControl(path, fileSecurity);

                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                WriteFileToStorage(ms, fs);
                //ms.WriteTo(fs);

                // clean up
                ms.Close();
                fs.Close();
                fs.Dispose();

                return "Upload File Success";
            }
            catch (Exception ex)
            {
                return "Upload false: " + ex.Message.ToString();
            }
        }

        private static void WriteFileToStorage(MemoryStream ms, FileStream fs)
        {
            // write the memory stream containing the original
            // file as a byte array to the filestream            
            ms.WriteTo(fs);
        }
    }
}
