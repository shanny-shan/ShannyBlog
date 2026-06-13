using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Entities;
using blog_pojo.Vos;

namespace blog_server.Services.Impl
{
    public class AboutService : IAboutService
    {
        private readonly AboutMapper _aboutMapper;

        public AboutService(AboutMapper aboutMapper)
        {
            _aboutMapper = aboutMapper;
        }

        public Result<List<AboutVO>> GetAboutMe()
        {
            List<About> list = _aboutMapper.GetAll();
            List<AboutVO> voList = new();
            foreach (var item in list)
            {
                AboutVO vo = new();
                CopyEntityToVo(item, vo);
                voList.Add(vo);
            }
            return Result<List<AboutVO>>.Success(voList);
        }

        public Result<AboutVO> GetAboutMeByShow()
        {
            About model = _aboutMapper.GetByShow(true);
            AboutVO vo = new();
            CopyEntityToVo(model, vo);
            return Result<AboutVO>.Success(vo);
        }

        public Result<AboutVO> AddAbout(AboutDTO aboutDTO)
        {
            About entity = new();
            CopyDtoToEntity(aboutDTO, entity);

            About oldActive = _aboutMapper.GetByShow(true);
            if (oldActive != null)
            {
                entity.IsActive = false;
            }

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int randomNum = rand.Next(1, 7);
            entity.Avatar = $"{src}{randomNum}.jpg";

            _aboutMapper.InsertAbout(entity);

            AboutVO vo = new();
            CopyEntityToVo(entity, vo);
            return Result<AboutVO>.Success(vo);
        }

        public Result<AboutVO> UpdateAbout(AboutDTO aboutDTO)
        {
            if (aboutDTO.Id == null)
            {
                return Result<AboutVO>.Error("UPDATE_FAIL");
            }

            About entity = new();
            CopyDtoToEntity(aboutDTO, entity);
            _aboutMapper.UpdateAbout(entity);

            AboutVO vo = new();
            CopyEntityToVo(entity, vo);
            return Result<AboutVO>.Success(vo);
        }

        public Result<string> DeleteAboutById(long id)
        {
            _aboutMapper.DeleteById(id);
            return Result<string>.Success("DELETE_SUCCESS");
        }

        #region 拷贝方法（替代BeanUtils）
        private void CopyEntityToVo(About source, AboutVO target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Introduce = source.Introduce;
            target.Avatar = source.Avatar;
            target.IsActive = source.IsActive;
        }

        private void CopyDtoToEntity(AboutDTO source, About target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Introduce = source.Introduce;
        }
        #endregion
    }
}
