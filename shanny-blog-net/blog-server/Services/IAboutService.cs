using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Services
{
    public interface IAboutService
    {
        Result<List<AboutVO>> GetAboutMe();

        Result<AboutVO> GetAboutMeByShow();

        Result<AboutVO> AddAbout(AboutDTO aboutDTO);

        Result<AboutVO> UpdateAbout(AboutDTO aboutDTO);

        Result<string> DeleteAboutById(long id);
    }
}
