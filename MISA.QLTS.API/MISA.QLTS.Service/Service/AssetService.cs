using MISA.Common.Model;
using MISA.QLTS.Common.Model;
using MISA.QLTS.DataLayer.Interface;
using MISA.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.Service.Service
{
    /// <summary>
    /// Service cho thực thể Tài sản (Asset)
    /// </summary>
    /// CreatedBy: NMDAT(19/02/2021)
    public class AssetService : BaseService<Asset>, IAssetService
    {
        //Khởi tạo tham chiếu tới DbConnectionAsset
        private readonly IDbConnectionAsset _dbConnectionAsset;
        #region Contructor
        public AssetService(IBaseData<Asset> baseData, IDbConnectionAsset dbConnectionAsset) : base(baseData)
        {
            _dbConnectionAsset = dbConnectionAsset;
        }
        #endregion

        #region Method
        /// <summary>
        /// Override Validate insert từ BaseService
        /// </summary>
        /// <param name="entity">customer</param>
        /// <param name="errorMsg">messenger trả về</param>
        /// <returns>true : dữ liệu hợp lệ - false : dữ liệu không hợp lệ</returns>
        protected override bool Validate(Asset entity, ErrorMsg errorMsg)
        {
            // Khởi tạo biến isValid lưu trạng thái validate
            var isValid = true;

            //Validate dữ liệu(xử lý về nghiệp vụ):
            //1. Validate bắt buộc nhập:
            if (entity.AssetCode == null || entity.AssetCode == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.Common.Properties.Resources.ErrorService_EmptyAssetCode);
                isValid = false;
            }
            if (entity.AssetName == null || entity.AssetName == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.Common.Properties.Resources.ErrorService_EmptyAssetName);
                isValid = false;
            }

            //2. Validate dữ liệu không được phép (trùng): mã khách hàng,  số điện thoại
            // kiểm tra trong database đã tồn tại đã mã kh hay chưa
            var isExits = _dbConnectionAsset.CheckAssetCodeExits(entity.AssetCode);
            if (isExits)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.Common.Properties.Resources.ErrorService_DuplicateCustomerCode);
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Override Validate update từ BaseService
        /// </summary>
        /// <param name="entity">customer</param>
        /// <param name="errorMsg">messenger trả về</param>
        /// <returns>true : dữ liệu hợp lệ - false : dữ liệu không hợp lệ</returns>
        protected override bool ValidateUpdate(Asset entity, ErrorMsg errorMsg)
        {
            // Khởi tạo biến isValid lưu trạng thái validate
            var isValid = true;

            //Validate dữ liệu(xử lý về nghiệp vụ):
            //1. Validate bắt buộc nhập:
            if (entity.AssetCode == null || entity.AssetCode == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.Common.Properties.Resources.ErrorService_EmptyAssetCode);
                isValid = false;
            }
            if (entity.AssetName == null || entity.AssetName == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.Common.Properties.Resources.ErrorService_EmptyAssetName);
                isValid = false;
            }
            return isValid;
        }
        #endregion
    }
}
