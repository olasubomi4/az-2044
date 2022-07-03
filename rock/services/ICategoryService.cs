using rock.Models;

namespace rock.services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        void insert(Category category);
       
      
    }
}