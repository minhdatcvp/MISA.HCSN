using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.DataLayer.Interface
{
    /// <summary>
    /// Interface dùng để tham số đến class kết nối database
    /// </summary>
    /// CreatedBy: NMDAT(19/02/2021)
    public interface IDbContext<T>
    {
        // Dùng cho các câu SelectAll
        IEnumerable<T> Query(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
        //Dùng cho SelectByID
        T QueryFirst(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
        //Dùng cho Insert, Update, Delete
        int Excute(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
    }
}
