using System;
using System.Threading.Tasks;
using rv_core.Base;
using rvcore.Model;

namespace rv_core.Inters
{
    public interface IStarInter
    {
        //关注
        Task<HttpRes> Star(AbsAccount account, AbsUser user);

        Task<HttpRes> Login();
    }
}