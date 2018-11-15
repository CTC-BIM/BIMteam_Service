using BIMService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Services;

namespace BIMService.WebServices.Timesheets
{
    /// <summary>
    /// Summary description for TimesheetService
    /// </summary>
    //[WebService(Namespace = "http://tempuri.org/")]
    [WebService(Namespace = "http://services.cbimtech.com/WebServices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TimesheetService : System.Web.Services.WebService
    {
        private BIMdbContext db = new BIMdbContext();
        /// <summary>
        /// Hàm trả về danh sách công tác thực hiện cho dự án
        /// </summary>
        /// <param name="id">ID dự án</param>
        /// <returns></returns>
        [WebMethod]
        public List<TimesheetOutput> DanhsachTimesheetTheoID(string id)
        {
            if (id == null || id.Trim() == "") id = "0000";
            List<C15_TimeSheet> items = db.C15_TimeSheet.Where(s => s.ProjectID == id).ToList();
            List<TimesheetOutput> listTimesheet = new List<TimesheetOutput>();

            foreach (C15_TimeSheet item in items)
            {
                int wg = 0;
                int hour = 0;
                int OT = 0;
                TimesheetOutput newitem = new TimesheetOutput();
                wg = int.Parse(item.WorkGroup.ToString());
                hour = int.Parse(item.Hour.ToString());
                OT = int.Parse(item.OvertimeHour.ToString());
                newitem.MemberID = item.MemberID;
                newitem.ProjectID = item.ProjectID;
                newitem.ProjectName = item.ProjectName;
                newitem.RecordDate = item.RecordDate;
                newitem.WorkType = item.WorkType;
                newitem.Description = item.Description;
                newitem.Hour = hour;
                newitem.OT = OT;
                newitem.WorkGroup = wg;
                listTimesheet.Add(newitem);
            };
            return listTimesheet;
        }

        [WebMethod]
        public List<TimesheetOutput> DanhsachTimesheetTheoMember(string memberid)
        {

            if (memberid == null || memberid.Trim() == "") memberid = "1";
            int mId = int.Parse(memberid);

            List<C15_TimeSheet> items = db.C15_TimeSheet.Where(s => s.MemberID == mId).ToList();
            List<TimesheetOutput> listTimesheet = new List<TimesheetOutput>();

            foreach (C15_TimeSheet item in items)
            {
                int wg = 0;
                int hour = 0;
                int OT = 0;
                TimesheetOutput newitem = new TimesheetOutput();
                wg = int.Parse(item.WorkGroup.ToString());
                hour = int.Parse(item.Hour.ToString());
                OT = int.Parse(item.OvertimeHour.ToString());
                newitem.MemberID = item.MemberID;
                newitem.MemberName = item.MemberName;
                newitem.ProjectID = item.ProjectID;
                newitem.ProjectName = item.ProjectName;
                newitem.RecordDate = item.RecordDate;
                newitem.WorkType = item.WorkType;
                newitem.Description = item.Description;
                newitem.Hour = hour;
                newitem.OT = OT;
                newitem.WorkGroup = wg;
                listTimesheet.Add(newitem);
            };
            return listTimesheet;
        }

    }


    [DataContract]
    public class TimesheetOutput
    {
        [DataMember]
        public int MemberID { get; set; }

        [DataMember]
        public string MemberName { get; set; }
        
        [DataMember]
        public string ProjectID { get; set; }

        [DataMember]
        public DateTime RecordDate { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string WorkType { get; set; }

        [DataMember]
        public int WorkGroup { get; set; }

        [DataMember]
        public double Hour { get; set; }

        [DataMember]
        public double OT { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

    [DataContract]
    public class TimesheetInput
    {
        [DataMember]
        public int MemberID { get; set; }

        [DataMember]
        public string MemberName { get; set; }

        [DataMember]
        public string ProjectID { get; set; }

        [DataMember]
        public DateTime RecordDate { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string WorkType { get; set; }

        [DataMember]
        public int WorkGroup { get; set; }

        [DataMember]
        public double Hour { get; set; }

        [DataMember]
        public double OT { get; set; }

        [DataMember]
        public string Description { get; set; }
    }

}
