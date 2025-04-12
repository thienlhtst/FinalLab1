using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FinalLab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : GenericControllerBase<Category,int>
    {
        public CategoryController(IGenericService<Category,int> service) : base(service)
        {
        }
    }
}
