using blog_common.Constant;
using blog_common.Result;
using blog_db;
using blog_db.Data;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Service.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly _DbContext _dbContext;

        public CategoryService(_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<CategoryVO>>> GetCategories()
        {
            var list = await _dbContext.Set<Category>().ToListAsync();
            var voList = list.Select(x => MapEntityToVo(x)).ToList();
            return Result<List<CategoryVO>>.Success(voList);
        }

        public async Task<Result<CategoryVO>> AddCategory(CategoryDTO categoryDTO)
        {
            Category entity = MapDtoToEntity(categoryDTO);

            _dbContext.Set<Category>().Add(entity);
            await _dbContext.SaveChangesAsync();

            CategoryVO vo = MapEntityToVo(entity);
            return Result<CategoryVO>.Success(ResultMsg.InsertSuccess, vo);
        }

        public async Task<Result<CategoryVO>> UpdateCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO.Id <= 0)
            {
                return Result<CategoryVO>.Error(ResultMsg.UpdateFail);
            }

            var dbModel = await _dbContext.Set<Category>().FindAsync(categoryDTO.Id);
            if (dbModel == null)
            {
                return Result<CategoryVO>.Error(ResultMsg.UpdateFail);
            }

            MapDtoCoverEntity(categoryDTO, dbModel);
            await _dbContext.SaveChangesAsync();

            CategoryVO vo = MapEntityToVo(dbModel);
            return Result<CategoryVO>.Success(ResultMsg.UpdateSuccess, vo);
        }

        public async Task<Result<string>> DeleteCategoryById(long id)
        {
            var model = await _dbContext.Set<Category>().FindAsync(id);
            if (model != null)
            {
                _dbContext.Set<Category>().Remove(model);
                await _dbContext.SaveChangesAsync();
            }
            return Result<string>.Success(ResultMsg.DeleteSuccess);
        }

        #region 映射方法
        private CategoryVO MapEntityToVo(Category source)
        {
            return new CategoryVO
            {
                Id = source.Id,
                Name = source.Name,
                NameEn = source.NameEn,
                Sort = source.Sort,
                Type = source.Type
            };
        }

        private Category MapDtoToEntity(CategoryDTO source)
        {
            return new Category
            {
                Id = source.Id > 0 ? source.Id : 0L,
                Name = source.Name,
                NameEn = source.NameEn,
                Sort = source.Sort,
                Type = source.Type
            };
        }

        private void MapDtoCoverEntity(CategoryDTO dto, Category target)
        {
            if (!string.IsNullOrEmpty(dto.Name))
                target.Name = dto.Name;

            if (dto.Sort > 0)
                target.Sort = dto.Sort;

            if (!string.IsNullOrEmpty(dto.NameEn))
                target.NameEn = dto.NameEn;

            target.Type = dto.Type;
        }
        #endregion
    }
}