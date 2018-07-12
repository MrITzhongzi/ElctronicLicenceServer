using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ElectronicLicenceServer.Models;

namespace ElectronicLicenceServer.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ElectronicLicContext _db;
        private readonly ILogger<AccountController> _log;

        public AccountController(ElectronicLicContext db, ILogger<AccountController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login(string data)
        { 
            dynamic loginInfo = JsonConvert.DeserializeObject(Uri.UnescapeDataString(data));
            var idNum = (string) loginInfo.idNum;
            var name = (string) loginInfo.name;
            var phone = (string) loginInfo.phone;

            var user = await _db.User.FirstOrDefaultAsync(x => x.IdNum == idNum);
            var token = Guid.NewGuid().ToString().Replace("-", "");

            if (user == null)
            {
                user = new User
                {
                    Token = token,
                    IdNum = idNum,
                    Name = name,
                    Phone = phone,
                };
                await _db.User.AddAsync(user);
            }
            else
            {
                if (user.Name != name || user.IdNum != idNum)
                {
                    _log.LogWarning("登录用户信息与数据库中的不符，数据库中信息为：{0}", user);
                }

                user.Token = token;
            }

            var xszStr = await  UpdateXszList("370523199403311011");
            var jszStr = await UpdateJsz("370523199403311011");

            await _db.SaveChangesAsync();
            return Ok(new
            {
                status = "ok",
                info = new
                {
                    user.Token,
                    user.IdNum,
                    user.Name,
                    user.Phone,
                    Jsz = jszStr,
                    Xsz = xszStr
                }
            });
        }

       
        /***
         * 获取数据库中行驶证列表 并返回
         */
        private async Task<string> UpdateXszList(string idNum)
        {
            var Xsz = _db.Xsz.Where(x => x.IdNum == idNum);
            return JsonConvert.SerializeObject(Xsz);
        }
        /***
         * 获取数据库中 驾驶证返回 
         */
        private async Task<string> UpdateJsz(string idNum)
        {
            var Jsz = _db.Jsz.Where(x => x.IdNum == idNum);
            return JsonConvert.SerializeObject(Jsz);
        }
    }
}