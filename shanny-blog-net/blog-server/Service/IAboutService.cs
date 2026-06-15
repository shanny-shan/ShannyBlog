using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Service
{
    public interface IAboutService
    {
        Task<Result<List<AboutVO>>> GetAboutMe();
        Task<Result<AboutVO>> GetAboutMeByShow();
        Task<Result<AboutVO>> AddAbout(AboutDTO aboutDTO);
        Task<Result<AboutVO>> UpdateAbout(AboutDTO aboutDTO);
        Task<Result<string>> DeleteAboutById(long id);
    }
}