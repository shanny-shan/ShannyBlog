using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_db.Data;
using Microsoft.EntityFrameworkCore;
using blog_db;

namespace blog_server.Service.Impl
{
    public class AboutService : IAboutService
    {
        private readonly _DbContext _dbContext;

        public AboutService(_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<AboutVO>>> GetAboutMe()
        {
            List<About> list = await _dbContext.Set<About>().ToListAsync();
            List<AboutVO> voList = new();
            foreach (var item in list)
            {
                AboutVO vo = new();
                CopyEntityToVo(item, vo);
                voList.Add(vo);
            }
            return Result<List<AboutVO>>.Success(voList);
        }

        public async Task<Result<AboutVO>> GetAboutMeByShow()
        {
            About? model = await _dbContext.Set<About>().FirstOrDefaultAsync(x => x.IsActive == true);
            AboutVO vo = new();
            if (model != null)
            {
                CopyEntityToVo(model, vo);
            }
            return Result<AboutVO>.Success(vo);
        }

        public async Task<Result<AboutVO>> AddAbout(AboutDTO aboutDTO)
        {
            About entity = new();
            CopyDtoToEntity(aboutDTO, entity);

            About? oldActive = await _dbContext.Set<About>().FirstOrDefaultAsync(x => x.IsActive == true);
            if (oldActive != null)
            {
                entity.IsActive = false;
            }
            else
            {
                entity.IsActive = true;
            }

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int randomNum = rand.Next(1, 7);
            entity.Avatar = $"{src}{randomNum}.jpg";

            _dbContext.Set<About>().Add(entity);
            await _dbContext.SaveChangesAsync();

            AboutVO vo = new();
            CopyEntityToVo(entity, vo);
            return Result<AboutVO>.Success(vo);
        }

        public async Task<Result<AboutVO>> UpdateAbout(AboutDTO aboutDTO)
        {
            if (aboutDTO.Id <= 0)
            {
                return Result<AboutVO>.Error("UPDATE_FAIL");
            }

            About? entityDb = await _dbContext.Set<About>().FindAsync(aboutDTO.Id);
            if (entityDb == null)
            {
                return Result<AboutVO>.Error("UPDATE_FAIL");
            }

            CopyDtoToEntity(aboutDTO, entityDb);
            await _dbContext.SaveChangesAsync();

            AboutVO vo = new();
            CopyEntityToVo(entityDb, vo);
            return Result<AboutVO>.Success(vo);
        }

        public async Task<Result<string>> DeleteAboutById(long id)
        {
            About? entity = await _dbContext.Set<About>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<About>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            return Result<string>.Success("DELETE_SUCCESS");
        }

        #region 拷贝方法不变
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