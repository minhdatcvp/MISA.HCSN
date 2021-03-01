using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CukCuk.Api.Controllers;
using MISA.QLTS.Common.Entitys;
using MISA.QLTS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.API.Controllers
{
    /// <summary>
    /// Controller Asset được kế thừa từ basecontroller để quản lý các phương thức gọi API Asset
    /// </summary>
    /// CreatedBy: NMDAT(20/02/2021)
    public class DepartmentsController : BasesController<Department>
    {
        public DepartmentsController(IDepartmentService _departmentService) : base(_departmentService)
        {

        }
    }
}
