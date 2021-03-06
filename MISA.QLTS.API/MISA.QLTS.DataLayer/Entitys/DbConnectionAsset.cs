using MISA.QLTS.Common.Model;
using MISA.QLTS.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISA.QLTS.DataLayer.Entitys
{
    /// <summary>
    /// Kết nối với Database bảng Asset
    /// </summary>
    /// CreatedBy: NMDAT (19/02/2021)
    public class DbConnectionAsset : BaseDBConnection<Asset> ,IDbConnectionAsset
    {
        // Khởi tạo tham chiếu interface
        private readonly IDbContext<Asset> _dbContext;

        #region Contructor
        public DbConnectionAsset(IDbContext<Asset> dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Method
        /// <summary>
        /// Kiểm tra mã tài sản đã tồn tại hay chưa
        /// </summary>
        /// <param name="customerCode">mã tài sản</param>
        /// <returns>true là tồn tại - false là chưa tồn tại</returns>
        public bool CheckAssetCodeExits(string assetCode, string assetId = null)
        {
            // sql truy vấn mã tài sản
            var sql = $"SELECT * FROM Asset AS a WHERE a.AssetCode = '{assetCode}' AND a.AssetId != '{assetId}' ";

            // dapper thực hiện truy vấn nếu null là không tồn tại - != null là tồn tại
            var customerCodeExits = _dbContext.Query(sql).FirstOrDefault();
            if (customerCodeExits != null)
                return true;
            else
                return false;
        }
        #endregion
    }
}
