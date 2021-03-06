﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace BIMService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProjectService" in both code and config file together.
    #region DataContract
    [DataContract(IsReference = true)]
    public class DuAnOutput
    {
        [DataMember]
        public string MaDuAn { get; set; }

        [DataMember]
        public string TenDuAn { get; set; }

        [DataMember]
        public string BIMmember { get; set; }

        [DataMember]
        public string ProjectState { get; set; }

        [DataMember]
        public string BIMMEP { get; set; }

        [DataMember]
        public string ARCmodel { get; set; }

        [DataMember]
        public string STRmodel { get; set; }

        [DataMember]
        public string MEPmodel { get; set; }

        //[DataMember]
        //public string Outsource { get; set; }

        //[DataMember]
        //public string propjectStatus { get; set; }

        //[DataMember]
        //public string BIMtarget { get; set; }

        //[DataMember]
        //public int ProjectType { get; set; }

        //[DataMember]
        //public int ProjectManager { get; set; }



        //[DataMember]
        //public DateTime Startdate { get; set; }

        //[DataMember]
        //public string TrackingDetail { get; set; }

        //[DataMember]
        //public string Problems { get; set; }

        //[DataMember]
        //public string Solution { get; set; }

        //[DataMember]
        //public int TongSoDuAn { get; set; }

        //[DataMember]
        //public string projectYear { get; set; }

    }

    #endregion

    #region Service contract
    [ServiceContract]
    public interface IProjectService
    {
        /// <summary>
        /// Hàm trả về danh sách tất ca dự án
        /// <returns></returns>
        [OperationContract]
        List<DuAnOutput> DanhSachDuAn();

        /// <summary>
        /// Hàm trả về thông tin dự án theo tên nhập vào
        /// </summary>
        /// <param name="name">Tên dự án</param>
        /// <returns></returns>
        [OperationContract]
        DuAnOutput TimDuAnTheoTen(string name);

        /// <summary>
        /// Hàm trả về thông tin dự án theo ID-Mã dự án- nhập vào
        /// </summary>
        /// <param name="id">Mã dự án</param>
        /// <returns></returns>
        [OperationContract]
        DuAnOutput TimDuAnTheoId(string id);

    }
    #endregion
}
