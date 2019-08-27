using System.Threading.Tasks;
using rv_core.Base;
using rv_core.Inters;
using rvcore.Model;

namespace Fancs.Models
{
    public class Account : IStarInter
    {
        public async Task<HttpRes> Star(AbsAccount account, AbsUser user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<HttpRes> Login()
        {
            throw new System.NotImplementedException();
        }
    }
}