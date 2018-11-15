using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data.Entity;
using BIMService.Models;
using System.Runtime.Serialization;

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
                MEPmodel = s.Modeling_MEP
            }).ToList();
            return items;
        }


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
                MEPmodel = item.Modeling_MEP

            };
            return project;
        }
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
                MEPmodel = item.Modeling_MEP
            };
            return project;
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

}
