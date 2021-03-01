using MISA.QLTS.Common.Entitys;
using MISA.QLTS.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.DataLayer.Entitys
{
    /// <summary>
    /// Kết nối với Database bảng Department
    /// </summary>
    /// CreatedBy: NMDAT (20/02/2021)
    public class DbConnectionDepartment : BaseDBConnection<Department>, IDbConnectionDepartment
    {
        // Khởi tạo tham chiếu interface
        private readonly IDbContext<Department> _dbContext;

        #region Contructor
        public DbConnectionDepartment(IDbContext<Department> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
    }
}
