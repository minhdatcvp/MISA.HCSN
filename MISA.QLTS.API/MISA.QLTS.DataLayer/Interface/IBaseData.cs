using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.DataLayer.Interface
{
    /// <summary>
    /// Interface tham chiếu tới các phiên bản kết nối database : DbConnectionV1
    /// </summary>
    /// <typeparam name="T">Bảng kết nối</typeparam>
    /// CreatedBy: NMDAT(19/02/2021)
    public interface IBaseData<T>
    {
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// Lấy theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Guid id);

        /// <summary>
        /// Thêm mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(T entity);

        /// <summary>
        /// Sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Delete(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        int Deletes(string param);

        /// <summary>
        /// Check trùng mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        T GetEntityByCode(string code);
    }
}
