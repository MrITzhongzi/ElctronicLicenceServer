using System;
using System.Linq;
using System.Threading.Tasks;
using ElectronicLicenceServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ElectronicLicenceServer.Controllers
{
    [Route("[controller]")]
    public class XszController: Controller
    {
        private readonly ElectronicLicContext _db;
        private readonly ILogger<AccountController> _log;
        private readonly Util _util;

        public XszController(ElectronicLicContext db, ILogger<AccountController> log, Util util)
        {
            _db = db;
            _log = log;
            _util = util;
        }

        public async Task<ActionResult> GetXsz(string idNum)
        {
            // 两种查询区别 ：1、where 如果lamda表达式为true 的话，变量接收到 对应的 对象
            //                 2、select 如果lamda表达式 最后返回什么， 变量接收到的就是什么
//            var xszList = _db.Xsz.Where(x => x.IdNum == idNum);
            var xszList = _db.Xsz.Select(c => c.IdNum == idNum ? c : null);
            Console.WriteLine(JsonConvert.SerializeObject(xszList));
            return Ok(new
            {
                staus = "test",
                xsz = xszList
            });
        }

        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            var aaa = await GetXsz("370523199403311011");
            return aaa;
        }
    }
}