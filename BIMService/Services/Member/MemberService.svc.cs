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
                    UserType = s.UserType
                })
                .ToList();
            return items;
        }

        public MemberOutput MemberbyID(int id)
        {
            throw new NotImplementedException();
        }

        public MemberOutput MemberByName(string name)
        {
            throw new NotImplementedException();
        }

        public MemberOutput MemberbyUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
