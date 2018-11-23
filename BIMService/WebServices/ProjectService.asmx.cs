using BIMService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Services;

namespace BIMService.WebServices.Projects
{
    /// <summary>
    /// Summary description for ProjectService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://services.cbimtech.com/WebServices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProjectService : System.Web.Services.WebService
    {
        private BIMdbContext db = new BIMdbContext();

        /// <summary>
        /// Hàm lấy danh sách Toàn bộ các dự án
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<DuAnOutput> GetProjectList()
        {
            var items = db.C01_DesignProject.Select(s => new DuAnOutput
            {
                MaDuAn = s.ProjectID,
                TenDuAn = s.ProjectName,
                BIMmember = s.BIM_staff,
                BIMMEP = s.BIM_MEP_staff,
                ProjectState = s.ProjectState,
                ARCmodel = s.Modeling_ARC_main,
                STRmodel = s.Modeling_STR,
                MEPmodel = s.Modeling_MEP,
                propjectStatus = s.ProjectStatus,
                projectPhase = s.ProjectPhase,
                projectScope = s.ProjectScope
            }).ToList();
            return items;
        }

        /// <summary>
        /// Hàm tìm kiếm dự án theo tên dự án nhập vào
        /// </summary>
        /// <param name="name">Tên dự án - VD: "Berriver"</param>
        /// <returns></returns>
        [WebMethod]
        public DuAnOutput TimDuAnTheoTen(string name)
        {
            if (name == null || name.Trim() == "") return null;
            C01_DesignProject item = db.C01_DesignProject.FirstOrDefault(s => s.ProjectName == name);
            if (item == null) return null;
            DuAnOutput project = new DuAnOutput()
            {
                MaDuAn = item.ProjectID,
                TenDuAn = item.ProjectName,
                BIMmember = item.BIM_staff,
                BIMMEP = item.BIM_MEP_staff,
                ProjectState = item.ProjectState,
                ARCmodel = item.Modeling_ARC_main,
                STRmodel = item.Modeling_STR,
                MEPmodel = item.Modeling_MEP,
                propjectStatus = item.ProjectStatus,
                projectPhase = item.ProjectPhase,
                projectScope = item.ProjectScope

            };
            return project;
        }

        /// <summary>
        /// Hàm tìm kiếm dự án theo Mã dự án nhập vào
        /// </summary>
        /// <param name="id">Mã dự án - VD: "1646" hoặc "T1702"</param>
        /// <returns></returns>
        [WebMethod]
        public DuAnOutput TimDuAnTheoId(string id)
        {
            if (id == null || id.Trim() == "") return null;
            C01_DesignProject item = db.C01_DesignProject.Find(id);
            if (item == null) return null;
            DuAnOutput project = new DuAnOutput()
            {
                MaDuAn = item.ProjectID,
                TenDuAn = item.ProjectName,
                BIMmember = item.BIM_staff,
                BIMMEP = item.BIM_MEP_staff,
                ProjectState = item.ProjectState,
                ARCmodel = item.Modeling_ARC_main,
                STRmodel = item.Modeling_STR,
                MEPmodel = item.Modeling_MEP,
                propjectStatus = item.ProjectStatus,
                projectPhase = item.ProjectPhase,
                projectScope = item.ProjectScope
            };
            return project;
        }

        /// <summary>
        /// Hàm lấy danh sách các dự án thuộc phạm vi của phòng
        /// </summary>
        /// <param name="scope">"Ban BIM CTC" hoặc "Ban BIM MEP"</param>
        /// <returns></returns>
        [WebMethod]
        public List<DuAnOutput> TimDuAnTheoScope(string scope)
        {
            if (scope == null || scope.Trim() == "") return null;
            List<C01_DesignProject> items = db.C01_DesignProject.Where(s => s.ProjectScope == scope).ToList();
            if (items == null || items.Count == 0) return null;
            List<DuAnOutput> lstproject = new List<DuAnOutput>();
            foreach (C01_DesignProject item in items)
            {
                DuAnOutput da = new DuAnOutput();
                da.MaDuAn = item.ProjectID;
                da.TenDuAn = item.ProjectName;
                da.BIMmember = item.BIM_staff;
                da.BIMMEP = item.BIM_MEP_staff;
                da.ProjectState = item.ProjectState;
                da.ARCmodel = item.Modeling_ARC_main;
                da.STRmodel = item.Modeling_STR;
                da.MEPmodel = item.Modeling_MEP;
                da.propjectStatus = item.ProjectStatus;
                da.projectPhase = item.ProjectPhase;
                da.projectScope = item.ProjectScope;
                lstproject.Add(da);
            };
            items.Clear();
            return lstproject;
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

        [DataMember]
        public string propjectStatus { get; set; }

        [DataMember]
        public string projectPhase { get; set; }

        [DataMember]
        public string projectScope { get; set; }

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

}
