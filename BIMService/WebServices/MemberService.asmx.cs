using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Services;
using BIMService.Models;
using System.Data.Entity;

namespace BIMService.WebServices.Members
{
    /// <summary>
    /// Summary description for MemberService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MemberService : System.Web.Services.WebService
    {
        private BIMdbContext db = new BIMdbContext();
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
                    UserStatus = s.UserStatus
                })
                .ToList();
            return items;
        }

        [WebMethod]
        public MemberOutput MemberbyID(int id)
        {
            if (id == null || id < 0) return null;
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
                UserStatus = item.UserStatus
            };
            return member;
        }

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
                UserStatus = item.UserStatus
            };
            return member;
        }

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
                UserStatus = item.UserStatus
            };
            return member;
        }
    }

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
    }
}
