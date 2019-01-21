using BIMService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Services;

namespace BIMService.WebServices.Members
{
    #region DataContract
    [DataContract]
    public class MemberOutput
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string SoftName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string UserType { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string UserStatus { get; set; }
        [DataMember]
        public string Image { get; set; }
    }
    #endregion

    #region Member Services
    /// <summary>
    /// Services cung cấp thông tin Member
    /// Bao gồm
    /// 1. Danh sách nhân viên
    /// 2. Tìm kiếm nhân viên theo Tên hoặc UserName hoặc MãNV-ID hoặc Tình trạng làm việc
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://services.cbimtech.com/WebServices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MemberService : System.Web.Services.WebService
    {
        private BIMdbContext db = new BIMdbContext();

        /// <summary>
        /// Service lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<MemberOutput> DanhSachMember()
        {
            List<MemberOutput> items = db.C02_BIMstaff
                .Select(s => new MemberOutput
                {
                    ID = s.BIMstaffID,
                    SoftName = s.Sortname,
                    UserName = s.Username,
                    Password = s.Password,
                    Department = s.Deparment,
                    UserType = s.UserType,
                    Image = s.Image,
                    UserStatus = s.UserStatus
                })
                .ToList();
            return items;
        }

        /// <summary>
        /// Service tìm kiếm nhân viên theo Mã nhân viên
        /// </summary>
        /// <param name="id">Mã nhân viên - kiểu INT - từ 1 => 100</param>
        /// <returns></returns>
        [WebMethod]
        public MemberOutput MemberbyID(int id)
        {
            if (id < 0) return null;
            C02_BIMstaff item = db.C02_BIMstaff.Find(id);
            if (item == null) return null;
            MemberOutput member = new MemberOutput()
            {
                ID = item.BIMstaffID,
                SoftName = item.Sortname,
                UserName = item.Username,
                Password = item.Password,
                Department = item.Deparment,
                UserType = item.UserType,
                Image = item.Image,
                UserStatus = item.UserStatus
            };
            return member;
        }

        /// <summary>
        /// Service tìm kiếm nhân viên theo Tên nhân viên nhập vào
        /// </summary>
        /// <param name="name">Tên nhân viên - VD: "Tui nè"</param>
        /// <returns></returns>
        [WebMethod]
        public MemberOutput MemberByName(string name)
        {
            if (name == null || name.Trim() == "") return null;
            C02_BIMstaff item = db.C02_BIMstaff.FirstOrDefault(s => s.Sortname == name);
            if (item == null) return null;
            MemberOutput member = new MemberOutput()
            {
                ID = item.BIMstaffID,
                SoftName = item.Sortname,
                UserName = item.Username,
                Password = item.Password,
                Department = item.Deparment,
                UserType = item.UserType,
                Image = item.Image,
                UserStatus = item.UserStatus
            };
            return member;
        }


        /// <summary>
        /// Service tìm kiếm nhân viên theo Username
        /// </summary>
        /// <param name="username">VD: "nhantc"</param>
        /// <returns></returns>
        [WebMethod]
        public MemberOutput MemberbyUsername(string username)
        {
            if (username == null || username.Trim() == "") return null;
            C02_BIMstaff item = db.C02_BIMstaff.FirstOrDefault(s => s.Username == username);
            if (item == null) return null;
            MemberOutput member = new MemberOutput()
            {
                ID = item.BIMstaffID,
                SoftName = item.Sortname,
                UserName = item.Username,
                Password = item.Password,
                Department = item.Deparment,
                UserType = item.UserType,
                Image = item.Image,
                UserStatus = item.UserStatus
            };
            return member;
        }

        /// <summary>
        /// Service tìm kiếm nhân viên theo Tình trạng làm việc
        /// </summary>
        /// <param name="username">"0": đang làm việc; "-1": đã nghỉ việc</param>
        /// <returns></returns>
        [WebMethod]
        public List<MemberOutput> MemberbyTinhtrangLamViec(string tinhtrang)
        {
            if (tinhtrang == null || tinhtrang.Trim() == "") return null;
            List<C02_BIMstaff> items = db.C02_BIMstaff.Where(s => s.UserStatus == tinhtrang).ToList();
            if (items == null || items.Count == 0) return null;
            List<MemberOutput> lstMember = new List<MemberOutput>();
            foreach (C02_BIMstaff item in items)
            {
                MemberOutput it = new MemberOutput();
                it.ID = item.BIMstaffID;
                it.SoftName = item.Sortname;
                it.UserName = item.Username;
                it.Password = item.Password;
                it.Department = item.Deparment;
                it.UserType = item.UserType;
                it.UserStatus = item.UserStatus;
                it.Image = item.Image;
                lstMember.Add(it);
            }
            items.Clear();
            return lstMember;
        }


        [WebMethod]
        public MemberOutput Login(string userName, string password)
        {
            try
            {
                if (userName == null || userName.Trim() == "" || password == null) return null;
                var userLogin = db.C02_BIMstaff.Where(s => s.Username == userName && s.Password == password).Select(c => new MemberOutput
                {
                    ID = c.BIMstaffID,
                    UserName = c.Username,
                    SoftName = c.Sortname,
                    Password = c.Password,
                    UserType = c.UserType,
                    Department = c.Deparment,
                    UserStatus = c.UserStatus,
                    Image = c.Image
                });
                if (userLogin == null) return null;
                return userLogin;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose(); // Xóa luôn biến db
            }
            base.Dispose(disposing);
        }

    }

    #endregion

}
