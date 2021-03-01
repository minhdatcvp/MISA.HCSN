using MISA.QLTS.Common.Entitys;
using MISA.QLTS.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.DataLayer.Entitys
{
    /// <summary>
    /// Kết nối với Database bảng AssetType
    /// </summary>
    /// CreatedBy: NMDAT (20/02/2021)
    public class DbConnectionAssetType : BaseDBConnection<AssetType> , IDbConnectionAssetType
    {
        // Khởi tạo tham chiếu interface
        private readonly IDbContext<AssetType> _dbContext;

        #region Contructor
        public DbConnectionAssetType(IDbContext<AssetType> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
    }
}
