﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Service;
using MISA.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers
{
    /// <summary>
    /// Base Class cho Controller các controller kế thừa có thể gọi lại các method từ base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// CeatedByL NMDAT(20/02/2021)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        /// <summary>
        /// Khai báo interface tham chiếu đến BaseService
        /// </summary>
        IBaseService<T> _baseService;
        public BasesController(IBaseService<T> baseService)
        {
            _baseService = baseService;
        }
        /// <summary>
        /// Lấy toàn bộ dữ liệu đối tượng
        /// </summary>
        /// <returns>Danh sách dữ liệu dối tượng</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var res = _baseService.Get();
            var result = res.Data as List<T>;
            // Nếu có kết quả trả về result.Count > 0 thì trả về 200 nếu không trả về 204
            if (result.Count == 0)
                return StatusCode(204, res.Data);
            else
                return StatusCode(200, res.Data);
        }
        /// <summary>
        /// Lấy dữ liệu theo id
        /// </summary>
        /// <param name="id">id thực thể</param>
        /// <returns>Dữ liệu cần lấy theo id</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var res = _baseService.GetById(id);
            var result = res.Data as List<T>;
            // Nếu có kết quả trả về result.Count > 0 thì trả về 200 
            return StatusCode(200, res.Data);

        }
        [HttpGet("Pagination")]
        public IActionResult GetPagination([FromQuery] int limit,int offset)
        {
            var res = _baseService.GetPagination(limit,offset);
            var result = res.Data as List<T>;
            // Nếu có kết quả trả về result.Count > 0 thì trả về 200 nếu không trả về 204
            if (result.Count == 0)
                return StatusCode(204, res.Data);
            else
                return StatusCode(200, res.Data);
        }
        // POST api/<CustomersController>
        /// <summary>
        /// Thêm đối tượng
        /// </summary>
        /// <param name="entity">Thực thể</param>
        /// <returns>Responsive tương ứng cho Client (201- thành công, ...)</returns>
        [HttpPost]
        public IActionResult Post([FromBody] T entity)
        {
            // Gọi đến hàm Insert thực hiện validate -> thêm
            var res = _baseService.Insert(entity);

            return new JsonResult(res);
        }


        // POST api/<CustomersController>
        /// <summary>
        /// Sửa đối tượng
        /// </summary>
        /// <param name="entity">Thực thể</param>
        /// <returns>Responsive tương ứng cho Client (201- thành công, ...)</returns>
        [HttpPut("{id}")]
        public IActionResult Put(T entity,Guid id)
        {
            // Gọi đến hàm Insert thực hiện validate -> Sửa
            var res = _baseService.Update(entity,id);

            return new JsonResult(res);
        }
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Gọi đến hàm Insert thực hiện validate -> Xóa
            var res = _baseService.Delete(id);

            if (res.Success == false)
            {
                return StatusCode(400, res.Data);
            }
            else if (res.Success == true && (int)res.Data > 0)
            {
                return StatusCode(201, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }
        /// <summary>
        /// Xóa nhiều đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<ValuesController>
        [HttpDelete]
        public IActionResult Deletes([FromQuery]string param)
        {
            // Gọi đến hàm Insert thực hiện validate -> Xóa
            var res = _baseService.Deletes(param);

            if (res.Success == false)
            {
                return StatusCode(400, res.Data);
            }
            else if (res.Success == true && (int)res.Data > 0)
            {
                return StatusCode(201, res.Data);
            }
            else
            {
                return StatusCode(200, res.Data);
            }
        }

    }
}
