using MISA.QLTS.Common.Entitys;
using MISA.QLTS.DataLayer.Interface;
using MISA.QLTS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.Service.Service
{
    /// <summary>
    /// Service cho thực thể phòng ban (Department)
    /// </summary>
    /// CreatedBy: NMDAT(20/02/2021)
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        //Khởi tạo tham chiếu tới DbConnectionAsset
        private readonly IDbConnectionDepartment _dbConnectionDepartment;
        #region Contructor
        public DepartmentService(IBaseData<Department> baseData, IDbConnectionDepartment dbConnectionDepartment) : base(baseData)
        {
            _dbConnectionDepartment = dbConnectionDepartment;
        }
        #endregion
    }
}
