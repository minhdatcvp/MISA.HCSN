using MISA.QLTS.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.DataLayer.Interface
{
    /// <summary>
    /// Interface tham chiếu tới DbConnectionAsset
    /// </summary>
    /// CreatedBy: NMDAT(19/02/2021)
    public interface IDbConnectionAsset: IBaseData<Asset>
    {
        /// <summary>
        /// Kiểm tra mã tài sản đã tồn tại hay chưa
        /// </summary>
        /// <param name="customerCode">mã tài sản</param>
        /// <returns>true là tồn tại - false là chưa tồn tại</returns>
        bool CheckAssetCodeExits(string assetCode);
    }
}
