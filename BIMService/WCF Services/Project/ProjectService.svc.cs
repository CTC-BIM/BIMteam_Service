using BIMService.Models;
using System.Collections.Generic;
using System.Linq;

namespace BIMService.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProjectService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProjectService.svc or ProjectService.svc.cs at the Solution Explorer and start debugging.
    public class ProjectService : IProjectService
    {
        private BIMdbContext db = new BIMdbContext();

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
                ARCmodel=item.Modeling_ARC_main,
                STRmodel = item.Modeling_STR,
                MEPmodel = item.Modeling_MEP
                
            };
            return project;
        }

        public List<DuAnOutput> DanhSachDuAn()
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
    }
}
