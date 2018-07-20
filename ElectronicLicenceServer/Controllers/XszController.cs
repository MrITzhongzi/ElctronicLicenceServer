using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicLicenceServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
//            var aaa = await GetXsz("370523199403311011");
            return Ok(new{status="ok"});
        }

        [HttpGet("GetLicenseStatus")]
        public async Task<ActionResult> GetLicenseStatus()
        {
            var user = await _util.GetUserByRequest(Request);
            if (user == null)
            {
                return Ok(new {status = "Unauthorized"});
            }

            var xsz =  _db.Xsz.Select(x => x.IdNum == user.IdNum ? x : null);
           
            return Ok(new {status = "ok", user.Jsz, xsz});
        }

        [HttpGet("GetXsz")]
        public async Task<ActionResult> GetXsz(string cllx, string hphm)
        {
            var user = await _util.GetUserByRequest(Request);
            if (user == null)
            {
                return Ok(new {status = "Unauthorized"});
            }
            
            //判断当前登录的用户是否有权限查看所申请的行驶证
            var xsz = _db.Xsz.Where(x => x.IdNum == user.IdNum).ToList<dynamic>();
            var num = xsz.Count(x => (string)x.Cllx == cllx && (string)x.Hphm == hphm);
            if (num == 0)
            {
                return Ok(new {status = "Unauthorized"});
            }

            var info = _db.Xsz.FirstOrDefault(x => x.Cllx == cllx && x.Hphm == hphm); 
            return Ok(new {status = "ok",info});
        }

        [HttpGet("DeleteXsz")]
        public async Task<ActionResult> DeleteXsz(string cllx, string hphm)
        {
            var user = await _util.GetUserByRequest(Request);
            if (user == null)
            {
                return Ok(new {status = "Unauthorized"});
            }
            //判断当前登录的用户是否有权限查看所申请的行驶证
            var xsz = _db.Xsz.Where(x => x.IdNum == user.IdNum).ToList<dynamic>();
            var num = xsz.Count(x => (string)x.Cllx == cllx && (string)x.Hphm == hphm);
            if (num == 0)
            {
                return Ok(new {status = "Unauthorized"});
            }

            var xszOne = await _db.Xsz.FirstOrDefaultAsync(x => x.Cllx == cllx && x.Hphm == hphm);
            xszOne.Delete = true;
            await _db.SaveChangesAsync();
            
            return Ok(new {status = "ok", xsz = xszOne});
        }
    }
}