using BIMService.WebServices.Members;
using System.Web.Services;

namespace BIMService.WebServices
{
    /// <summary>
    /// Summary description for LoginService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoginService : System.Web.Services.WebService
    {

        [WebMethod]
        public MemberOutput Login(string userName, string password)
        {
            try
            {
                if (userName == null || userName.Trim() == "" || password == null || password.Trim() == "") return null;
                //Viết thêm hàm login
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
