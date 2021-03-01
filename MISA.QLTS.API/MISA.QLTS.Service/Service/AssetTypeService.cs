using MISA.QLTS.Common.Entitys;
using MISA.QLTS.DataLayer.Interface;
using MISA.QLTS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.Service.Service
{
    /// <summary>
    /// Service cho thực thể loại Tài sản (AssetType)
    /// </summary>
    /// CreatedBy: NMDAT(20/02/2021)
    public class AssetTypeService : BaseService<AssetType>,IAssetTypeService
    {
        //Khởi tạo tham chiếu tới DbConnectionAsset
        private readonly IDbConnectionAssetType _dbConnectionAssetType;
        #region Contructor
        public AssetTypeService(IBaseData<AssetType> baseData, IDbConnectionAssetType dbConnectionAssetType) : base(baseData)
        {
            _dbConnectionAssetType = dbConnectionAssetType;
        }
        #endregion
    }
}
