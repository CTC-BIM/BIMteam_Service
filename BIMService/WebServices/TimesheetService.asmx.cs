using BIMService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Services;
using System.IO;
using System.ComponentModel;

namespace BIMService.WebServices.Timesheets
{

    #region DataContract
    /// <summary>
    /// Gói dữ liệu TImesheet trả về cho Request
    /// </summary>
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

    /// <summary>
    /// Enity WorkName đọc ra từ Database
    /// </summary>
    [DataContract]
    public class WorkNameOutput
    {
        [DataMember]
        public int WorkID { get; set; }
        [DataMember]
        public string WorkName { get; set; }
        [DataMember]
        public int? WorkGroup { get; set; }
    }

    /// <summary>
    /// Enity Timesheet ghi vào Database
    /// Các Fields truyền về: MemberID,ProjectID,RecordDate,WorkID,Hour,OT,Description
    /// Các Fields không truyền về: MemberName,ProjectName,WorkGroup,WorkType
    /// </summary>
    [DataContract]
    public class TimesheetInput
    {
        [DataMember]
        [Required(ErrorMessage = "Không để trống MemberID")]
        public int MemberID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Không để trống ProjectID")]
        public string ProjectID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Không để trống ngày ghi công tác")]
        public DateTime RecordDate { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Không để trống công tác trong ngày")]
        public int WorkID { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Không được để trống")]
        [Range(1, 8, ErrorMessage = "Không được lớn hơn 8h/ngày")]
        public int Hour { get; set; }

        [DataMember]
        [Range(1, 8, ErrorMessage = "Không được lớn hơn 12h/ngày")]
        public int OT { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Không để trống, Cần có ghi chú công việc")]
        public string Description { get; set; }

        //Không truyền về server
        //[DataMember]
        //public int WorkGroup { get; set; }

        //[DataMember]
        //public int WorkType { get; set; }

        //[DataMember]
        //public string ProjectName { get; set; }

        //[DataMember]
        //public string MemberName { get; set; }
    }


    #endregion

    #region Timesheet Services

    /// <summary>
    /// Services cung cấp thông tin về WorkDone
    /// Bao gồm
    /// 1. Danh sách công tác đã làm cho 1 dự án nào đó
    /// 2. Danh sách công tác của 1 thành viên nào đó
    /// 3. Cung cấp danh sách Tên công tác
    /// 4. Tìm kiếm công tác nào đó theo ID
    /// 5. Tìm kiếm các công việc theo nhóm ID
    /// 6. Ghi Công việc hoàn tất vào Database ban BIM
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
        #region Nhóm Result services         
        //1
        /// <summary>
        /// Service trả về danh sách công tác thực hiện cho dự án
        /// Sắp xếp theo ngày mới nhất đầu tiên
        /// </summary>
        /// <param name="id">ID dự án - VD: 1702</param>
        /// <returns></returns>
        [WebMethod]
        public List<TimesheetOutput> DanhsachTimesheetTheoProjectID(string id)
        {
            if (id == null || id.Trim() == "") id = "0000";
            List<C15_TimeSheet> items = db.C15_TimeSheet.Where(s => s.ProjectID == id).OrderByDescending(s => s.RecordDate).ToList();
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
            items.Clear();
            return listTimesheet;
        }

        //2
        /// <summary>
        /// Service tìm kiếm Timesheet theo MemberID
        /// Sắp xếp theo ngày mới nhất đầu tiên
        /// </summary>
        /// <param name="memberid">ID của Member - VD: 02</param>
        /// <returns></returns>
        [WebMethod]
        public List<TimesheetOutput> DanhsachTimesheetTheoMemberID(string memberid)
        {

            if (memberid == null || memberid.Trim() == "") memberid = "1";
            int mId = int.Parse(memberid);

            List<C15_TimeSheet> items = db.C15_TimeSheet.Where(s => s.MemberID == mId).OrderByDescending(s => s.RecordDate).ToList();
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
            items.Clear();
            return listTimesheet;
        }

        //3
        /// <summary>
        /// Service trả về danh sách Công tác trong ngày
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<WorkNameOutput> WorkNameList()
        {
            List<C16_WorkType> items = db.C16_WorkType.ToList();
            List<WorkNameOutput> lstWorkName = new List<WorkNameOutput>();
            foreach (C16_WorkType item in items)
            {
                WorkNameOutput wn = new WorkNameOutput();
                wn.WorkID = item.WorkID;
                wn.WorkName = item.WorkName;
                wn.WorkGroup = item.WorkGroup;
                lstWorkName.Add(wn);
            }
            items.Clear();
            return lstWorkName;
        }

        //4
        /// <summary>
        /// Service tìm kiếm Công tác theo ID công việc
        /// </summary>
        /// <param name="id">Int - ID công tác</param>
        /// <returns></returns>
        [WebMethod]
        public WorkNameOutput FindWorkNameByID(int id)
        {
            if (id < 0) return null;
            var item = db.C16_WorkType.FirstOrDefault(s => s.WorkID == id);
            if (item == null) return null;
            WorkNameOutput wn = new WorkNameOutput();
            wn.WorkID = item.WorkID;
            wn.WorkName = item.WorkName;
            wn.WorkGroup = item.WorkGroup;
            return wn;
        }

        //5
        /// <summary>
        /// Service tìm kiếm các công việc theo ID nhóm công tác
        /// </summary>
        /// <param name="groupID">Mã nhóm</param>
        /// <returns></returns>
        [WebMethod]
        public List<WorkNameOutput> FindWorkInGroup(int groupID)
        {
            if (groupID < 0) groupID = 1;
            List<C16_WorkType> items = db.C16_WorkType.Where(s => s.WorkGroup == groupID).ToList();
            List<WorkNameOutput> lstWorkName = new List<WorkNameOutput>();
            foreach (C16_WorkType item in items)
            {
                WorkNameOutput wn = new WorkNameOutput();
                wn.WorkID = item.WorkID;
                wn.WorkName = item.WorkName;
                wn.WorkGroup = item.WorkGroup;
                lstWorkName.Add(wn);
            }
            items.Clear();
            return lstWorkName;
        }

        #endregion

        #region Nhóm CRUD
        /// <summary>
        /// Service thêm công việc hoàn tất trong ngày
        /// </summary>
        /// <param name="enity"></param>
        /// <returns>string Kết quả = TRUE or FALSE</returns>
        [WebMethod]
        public string AddWorkDone(TimesheetInput enity)
        {
            if (enity == null) throw new Exception("False because is NULL value");
            string username, ProjectName, workName;
            int? workGroup;
            bool checkUserHour = false;
            //Lấy UserName từ Database
            var user = db.C02_BIMstaff.FirstOrDefault(s => s.BIMstaffID == enity.MemberID);
            username = user.Sortname;
            //Lấy ra ProjectName
            var project = db.C01_DesignProject.FirstOrDefault(s => s.ProjectID == enity.ProjectID);
            ProjectName = project.ProjectName;
            //Lấy ra WorkName và WorkGroup từ WorkID truyền về
            var work = db.C16_WorkType.FirstOrDefault(s => s.WorkID == enity.WorkID);
            workName = work.WorkName;
            workGroup = work.WorkGroup != null ? work.WorkGroup : 1;
            checkUserHour = KiemtraSoGioTrongNgay(enity.MemberID,enity.RecordDate, enity.Hour, enity.OT);
            try
            {
                string kq = "Tổng số giờ trong ngày vượt quá 8h hoặc giờ Overtime quá nhiều trong ngày";
                if (checkUserHour)
                {
                    C15_TimeSheet newRecord = new C15_TimeSheet();
                    newRecord.MemberID = enity.MemberID;
                    newRecord.MemberName = username;
                    newRecord.ProjectID = enity.ProjectID;
                    newRecord.RecordDate = enity.RecordDate;
                    newRecord.ProjectName = ProjectName;
                    newRecord.WorkType = workName;
                    newRecord.WorkGroup = workGroup;
                    newRecord.Hour = enity.Hour;
                    newRecord.OvertimeHour = enity.OT;
                    newRecord.Description = enity.Description;
                    db.C15_TimeSheet.Add(newRecord);
                    db.SaveChanges();
                    kq = "Thêm công tác Thành công";
                    return kq;
                }
                else
                {
                    return kq;
                }
            }
            catch (Exception ex)
            {
                return "Thêm công tác Thất bại do " + ex.Message;
            }

        }


        /// <summary>
        /// Kiểm tra số giờ Record trong ngày
        /// </summary>
        /// <param name="recordDate">Ngày Record</param>
        /// <param name="hour">Số giờ chính</param>
        /// <param name="oT">Số giờ tăng ca</param>
        /// <returns>True: có thể thêm Record, FALSE: do Hour > 8h hoặc OT > 15h</returns>
        private bool KiemtraSoGioTrongNgay(int memberID, DateTime recordDate, int hour, int oT)
        {
            bool kq = false;
            int TotalHour = 0;
            int TotalOT = 0;
            //Lấy ra số Record trong ngày
            List<C15_TimeSheet> recordInDate = db.C15_TimeSheet.Where(s => s.RecordDate == recordDate).ToList();
            if (recordInDate == null || recordInDate.Count == 0) return kq = true;

            //Lấy ra số Record của Member cần tìm
            List<C15_TimeSheet> items = recordInDate.Where(s => s.MemberID == memberID).ToList();
            if (items == null || items.Count == 0) return kq = true;

            foreach (var item in items)
            {
                TotalHour += item.Hour;
                TotalOT += item.OvertimeHour;
            }
            if (TotalHour < 9 && TotalOT < 15) return kq = true;
            else return kq;
        }


        #endregion



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
