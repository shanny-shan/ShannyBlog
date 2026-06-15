using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Service
{
    public interface ICategoryService
    {
        Task<Result<List<CategoryVO>>> GetCategories();
        Task<Result<CategoryVO>> AddCategory(CategoryDTO categoryDTO);
        Task<Result<CategoryVO>> UpdateCategory(CategoryDTO categoryDTO);
        Task<Result<string>> DeleteCategoryById(long id);
    }
}