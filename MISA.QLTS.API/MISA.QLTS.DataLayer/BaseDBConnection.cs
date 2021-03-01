using Dapper;
using MISA.QLTS.DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.DataLayer
{
    /// <summary>
    /// Base tầng datalayer thực hiện các chức năng thêm sửa xóa với database
    /// </summary>
    /// <typeparam name="T">Bảng database</typeparam>
    public class BaseDBConnection<T> : IBaseData<T>
    {
        // Khởi tạo tham chiếu interface
        private readonly IDbContext<T> _dbContext;

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo BaseConnect khai báo dbContext
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseDBConnection(IDbContext<T> dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy dữ liệu theo tham số truyền vào nếu không truyền vào mặc định là lấy tất cả
        /// </summary>
        /// <typeparam name="T">Đối tượng cần lấy dữ liệu</typeparam>
        /// <param name="sqlCommand">Tên Store lấy dữ liệu</param>
        /// <param name="parameters">Dữ liệu truyền vào( lấy theo id,code,...)</param>
        /// <param name="commandType">Kiểu lấy dữ liệu</param>
        /// <returns>Trả về thông tin cần lấy</returns>
        public IEnumerable<T> GetAll(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text)
        {
            var procName = "Proc_Get" + typeof(T).Name;
            var listEntity = _dbContext.Query(procName, commandType: CommandType.StoredProcedure);
            return listEntity;
        }
        /// <summary>
        /// Thực thi proceduce lấy theo id
        /// Đặt tên proc theo kiểu Proc_Get...ById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Danh sách thực thể</returns>
        public T GetById(Guid id)
        {
            var className = typeof(T).Name;
            var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = '{id}'";
            var listEntity = _dbContext.QueryFirst(sqlCommand);
            return listEntity;
        }
        /// <summary>
        /// Thực thi proceduce insert
        /// Đặt tên proc theo kiểu Proc_Insert...
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public int Insert(T entity)
        {
            var procName = "Proc_Insert" + typeof(T).Name;
            var parameters = new DynamicParameters(entity);
            var result = _dbContext.Excute(procName, parameters, CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Thực thi proceduce update
        /// Đặt tên proc theo kiểu Proc_Update...
        /// </summary>
        /// <param name="entity">Thực thể cần sửa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public int Update(T entity)
        {
            var procName = "Proc_Update" + typeof(T).Name;
            var parameters = new DynamicParameters(entity);
            var result = _dbContext.Excute(procName, parameters, CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// Thực thi proceduce delete
        /// Đặt tên proc theo kiểu Proc_Delete...
        /// </summary>
        /// <param name="entity">Thực thể cần xóa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public int Delete(Guid id)
        {
            // lấy tên bảng
            var className = typeof(T).Name;
            // câu lệnh truy vấn
            var sqlCommand = $"DELETE FROM {className} WHERE {className}Id = '{id}'";
            var result = _dbContext.Excute(sqlCommand);
            return result;
        }
        /// <summary>
        /// Xóa nhiều bản ghi cùng lúc
        /// </summary>
        /// <param name="param">Mảng các id</param>
        /// <returns></returns>
        public int Deletes(string param)
        {
            // lấy tên bảng
            var className = typeof(T).Name;
            var paramId = param.Split(",");
            var id = string.Empty;
            foreach(var item in paramId)
            {
                id += "," + "'" + $"{item}" + "'";
            }
            id = id.Remove(0,1);
            // câu lệnh truy vấn
            var sqlCommand = $"DELETE FROM {className} WHERE {className}Id IN ({id})";
            var result = _dbContext.Excute(sqlCommand);
            return result;
        }
        /// <summary>
        /// Check trùng mã
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public T GetEntityByCode(string code)
        {
            string tableName = typeof(T).Name;
            //lấy mã 
            string fieldCode = tableName + "Code";

            var sql = $"SELECT * FROM {tableName} WHERE {fieldCode} = '{code}'";
            var result = _dbContext.QueryFirst(sql);
            return result;
        }
        #endregion

    }
}
