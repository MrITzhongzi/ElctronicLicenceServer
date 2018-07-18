using System.Net.Cache;
using System.Threading.Tasks;
using ElectronicLicenceServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ElectronicLicenceServer.Controllers
{
    public class Util
    {
        private readonly ElectronicLicContext _db;

        public Util()
        {
        }

        public Util(ElectronicLicContext db)
        {
            _db = db;
        }

        public async Task<User> GetUserByRequest(HttpRequest req)
        {
            var token = req.Headers["token"];
            return string.IsNullOrEmpty(token) ? null : await _db.User.FirstOrDefaultAsync();
        }

        /// <summary>
        /// 将 可空的bool；类型转换为 正常不可空的bool 类型
        /// </summary>
        /// <param name="accept"></param>
        /// <returns></returns>
        public bool TransformToBool(bool? accept)
        {
            bool tmp;
            if (!accept.HasValue)
            {
                tmp = false;
            }

            tmp = (bool) accept;
            return tmp;
        }
    }
}