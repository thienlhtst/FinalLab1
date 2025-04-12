using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : GenericControllerBase<Event,int>
    {
        public EventController(IGenericService<Event,int> service) : base(service)
        {
        }
    }
}
