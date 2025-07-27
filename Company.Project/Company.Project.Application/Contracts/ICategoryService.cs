using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Project.Application.Contracts
{
   public  interface ICategoryService
    {
        // search
        Task<IEnumerable<CategoryDto>> SearchAsync(string query);


    }
}
