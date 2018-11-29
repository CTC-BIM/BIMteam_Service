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

        [WebMethod]
        public string UploadFiles(string fileName, byte[] f, string dest)
        {
            try
            {
                //TODO:
                //Thay đổi tên FileUpload
                //[Project]-[Type]-[fileName]-[Date]
                MemoryStream ms = new MemoryStream(f);

                string path = Server.MapPath("../FileStorage/" + dest + "/" + fileName);//Cần chỉnh sửa filename trước khi Write
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
