using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using BIMService.Models;

namespace BIMService.Services.Member
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMemberService" in both code and config file together.
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



    [ServiceContract]
    public interface IMemberService
    {
        /// <summary>
        /// Hàm trả về danh sách các thành viên hiện có trong ban
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<MemberOutput> DanhSachMember();

        /// <summary>
        /// Hàm tìm kiếm thông tin Member theo id nhập vào
        /// </summary>
        /// <param name="id">ID của member</param>
        /// <returns></returns>
        [OperationContract]
        MemberOutput MemberbyID(int id);


        /// <summary>
        /// Hàm tìm kiếm thông tin Member theo Tên member nhập vào
        /// </summary>
        /// <param name="name">Tên ngắn gọn</param>
        /// <returns></returns>
        [OperationContract]
        MemberOutput MemberByName(string name);

        /// <summary>
        /// Hàm tìm kiếm thông tin Member theo username của member nhập vào
        /// </summary>
        /// <param name="username">Username dùng Login</param>
        /// <returns></returns>
        [OperationContract]
        MemberOutput MemberbyUsername(string username);

    }
}
