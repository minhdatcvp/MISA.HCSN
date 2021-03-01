using Dapper;
using Microsoft.AspNetCore.Mvc;
using MISA.Common.Model;
using MISA.QLTS.Common.Model;
using MISA.Service;
using MISA.Service.Interface;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.CukCuk.Api.Controllers
{
    /// <summary>
    /// Controller Asset được kế thừa từ basecontroller để quản lý các phương thức gọi API Asset
    /// </summary>
    /// CreatedBy: NMDAT(20/02/2021)
    public class AssetsController : BasesController<Asset>
    {
        public AssetsController(IAssetService _assetService) :base(_assetService)
        {

        }
    }
}
