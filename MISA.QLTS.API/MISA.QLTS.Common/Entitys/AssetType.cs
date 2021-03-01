using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.Common.Entitys
{
    /// <summary>
    /// Class thông tin loại tài sản 
    /// </summary>
    /// CreatedBy: NMDAT(20/02/2021)
    public class AssetType
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid AssetTypeId { get; set; }

        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public string AssetTypeCode { get; set; }
        /// <summary>
        /// Tên Loại tài sản
        /// </summary>
        public string AssetTypeName { get; set; }
        /// <summary>
        /// Miêu tả
        /// </summary>
        public string Description { get; set; }
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
