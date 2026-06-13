using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Entities;
using blog_pojo.Vos;

namespace blog_server.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryMapper _categoryMapper;

        public CategoryService(CategoryMapper categoryMapper)
        {
            _categoryMapper = categoryMapper;
        }

        public Result<List<CategoryVO>> GetCategories()
        {
            List<Category> list = _categoryMapper.GetAll();
            List<CategoryVO> voList = new();
            foreach (var item in list)
            {
                CategoryVO vo = MapEntityToVo(item);
                voList.Add(vo);
            }
            return Result<List<CategoryVO>>.Success(voList);
        }

        public Result<CategoryVO> AddCategory(CategoryDTO categoryDTO)
        {
            Category entity = MapDtoToEntity(categoryDTO);
            entity.Name = categoryDTO.Name;
            entity.Sort = categoryDTO.Sort;
            entity.NameEn = categoryDTO.NameEn;
            entity.Type = categoryDTO.Type;

            _categoryMapper.InsertCategory(entity);
            CategoryVO vo = MapEntityToVo(entity);
            return Result<CategoryVO>.Success(vo);
        }

        public Result<CategoryVO> UpdateCategory(CategoryDTO categoryDTO)
        {
            if (categoryDTO.Id == null)
            {
                return Result<CategoryVO>.Error("UPDATE_FAIL");
            }
            Category entity = MapDtoToEntity(categoryDTO);
            _categoryMapper.UpdateCategory(entity);
            CategoryVO vo = MapEntityToVo(entity);
            return Result<CategoryVO>.Success(vo);
        }

        public Result<string> DeleteCategoryById(long id)
        {
            _categoryMapper.DeleteById(id);
            return Result<string>.Success("DELETE_SUCCESS");
        }

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
                Id = source.Id,
                Name = source.Name,
                NameEn = source.NameEn,
                Sort = source.Sort,
                Type = source.Type
            };
        }
    }
}