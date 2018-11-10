using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BIMService.Models;

namespace BIMService.Services.Member
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MemberService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MemberService.svc or MemberService.svc.cs at the Solution Explorer and start debugging.
    public class MemberService : IMemberService
    {
        private BIMdbContext db = new BIMdbContext();
        public List<MemberOutput> DanhSachMember()
        {
            List<MemberOutput> items = db.C02_BIMstaff
                .Select(s => new MemberOutput {
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

        public MemberOutput MemberbyID(int id)
        {
            if (id == null || id < 0) return null;
            C02_BIMstaff item = db.C02_BIMstaff.Find(id);
            if (item == null) return null;
            MemberOutput member = new MemberOutput() {
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
}
