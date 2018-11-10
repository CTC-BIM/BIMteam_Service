using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using BIMService.Models;

namespace BIMService.Services.Timesheet
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TimesheetService
    {
        private BIMdbContext db = new BIMdbContext();
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public List<TimesheetOutput> DanhsachTimesheetTheoID(string id)
        {
            if (id == null || id.Trim() == "") return null;
            List<TimesheetOutput> items = db.C15_TimeSheet
                .Where(s => s.ProjectID == id)
                .Select(s => new TimesheetOutput {
                MemberID = s.MemberID,
                ProjectID = s.ProjectID,
                ProjectName = s.ProjectName,
                RecordDate = s.RecordDate,
                WorkType = s.WorkType,
                Hour = int.Parse(s.Hour.ToString()),
                OT = int.Parse(s.OvertimeHour.ToString()),
                Description = s.Description,
                WorkGroup = int.Parse(s.WorkGroup.ToString())
            }).ToList();
            return items;            
        }

        

        // Add more operations here and mark them with [OperationContract]
    }

    [DataContract]
    public class TimesheetOutput
    {
        [DataMember]
        public int MemberID { get; set; }

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
