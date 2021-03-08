using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model;
namespace DataAccessLayer.Interface
{
    public interface ICategory
    {
        int addCategory(CategaryModel cat);
        int deleteCategory(int cid);
        int editCategory(CategaryModel cat);
        List<CategaryModel> getAllCategory();
        CategaryModel CategoryDetails(int cid);
    }
}
