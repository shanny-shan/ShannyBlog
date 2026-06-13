using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Services
{
    public interface ICategoryService
    {
        Result<List<CategoryVO>> GetCategories();

        Result<CategoryVO> AddCategory(CategoryDTO categoryDTO);

        Result<CategoryVO> UpdateCategory(CategoryDTO categoryDTO);

        Result<string> DeleteCategoryById(long id);
    }
}
