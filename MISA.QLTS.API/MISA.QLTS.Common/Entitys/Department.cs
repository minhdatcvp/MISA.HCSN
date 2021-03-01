using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.Common.Entitys
{
    /// <summary>
    /// Class thông tin phòng ban
    /// </summary>
    /// CreatedBy: NMDAT(20/02/2021)
    public class Department
    {
        #region Property
        /// <summary>
        /// Khóa chính phòng ban
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
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
