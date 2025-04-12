using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FinalLab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecialEventController : GenericControllerBase<SpecialEvent,int>
    {
        public SpecialEventController(IGenericService<SpecialEvent,int> service) : base(service)
        {
        }
    }
}
