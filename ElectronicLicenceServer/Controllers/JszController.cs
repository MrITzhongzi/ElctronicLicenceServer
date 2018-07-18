using System;
using System.Threading.Tasks;
using ElectronicLicenceServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ElectronicLicenceServer.Controllers
{
    [Route("[controller]")]
    public class JszController : Controller
    {
        private readonly ElectronicLicContext _db;
        private readonly ILogger<AccountController> _log;
        private readonly Util _util;

        public JszController(ElectronicLicContext db, ILogger<AccountController> log, Util util)
        {
            _db = db;
            _log = log;
            _util = util;
        }

        public async Task<Jsz> GetJsz(string idNum)
        {
            var Jsz = await _db.Jsz.FirstOrDefaultAsync(x => x.IdNum == idNum);
            return Jsz;
        }

        [HttpGet("JszApply")]
        public async Task<ActionResult> JszApply()
        {
            var user = await _util.GetUserByRequest(Request);
            if (user == null)
            {
                return Ok(new
                {
                    status = "Unauthorized"
                });
            }

            var jszInfo = await GetJsz(user.IdNum);
            if (jszInfo == null || string.IsNullOrEmpty(jszInfo.IdNum))
            {
                return Ok(new
                {
                    status = "NoLic"
                });
            }

            user.Jsz = true;
            await _db.SaveChangesAsync();

            return Ok(new
            {
                status = "ok"
            });
        }

        [HttpGet("GetJsz")]
        public async Task<ActionResult> GetJsz()
        {
            var user = await _util.GetUserByRequest(Request);
            if (user == null)
            {
                return Ok(new {status = "Unauthorized"});
            }

            var jsz = await _db.Jsz.FirstOrDefaultAsync(x => x.IdNum == user.IdNum && !_util.TransformToBool(x.Delete));
            if (jsz == null)
            {
                return Ok(new
                {
                    status = "isDelete"
                });
            }
            
            return Ok(new
            {
                status = "ok",
                info = jsz
            });
        }

        [HttpGet("DeleteJsz")]
        public async Task<ActionResult> DeleteJsz()
        {
            var user = await _util.GetUserByRequest(Request);
            if (user == null)
            {
                return Ok(new
                {
                    status = "Unauthorized"
                });
            }

            user.Jsz = false;
            var jsz = await _db.Jsz.FirstOrDefaultAsync(x => x.IdNum == user.IdNum);
            if (jsz != null)
            {
//                _db.Jsz.Update(jsz);
                jsz.Delete = true;
            }

            await _db.SaveChangesAsync();

            return Ok(new {status = "ok"});
        }
        
    }
}