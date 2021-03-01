using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.Common.Model
{
    /// <summary>
    /// Class thông tin tài sản 
    /// </summary>
    /// CreatedBy: NMDAT(19/02/2021)
    public class Asset
    {
        #region Contructor

        #endregion

        #region Method
        #endregion

        #region Property
        /// <summary>
        /// Khóa chính - kiểu guid có thể tự sinh
        /// </summary>
        public Guid AssetId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string AssetCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// Khóa ngoại -> AssetType
        /// </summary>
        public Guid? AssetTypeId { get; set; }
        /// <summary>
        /// Khóa ngoại -> Department
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTime? IncreaseDate { get; set; }
        /// <summary>
        /// Thời gian sử dụng
        /// </summary>
        public int? TimeUse { get; set; }
        /// <summary>
        /// Tỉ lệ hao mòn
        /// </summary>
        public int? WearRate { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        public int? OriginalPrice { get; set; }
        /// <summary>
        /// Gía trị hao mòn năm
        /// </summary>
        public int? WearValue { get; set; }
        /// <summary>
        /// Sử dụng (1- sử dụng; 0 - không sử dụng)
        /// </summary>
        public int? IsUsed { get; set; }
        /// <summary>
        /// Mã Loại tài sản
        /// </summary>
        public string AssetTypeCode { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public string AssetTypeName { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        #endregion

        #region Other
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }
        #endregion
    }
}
