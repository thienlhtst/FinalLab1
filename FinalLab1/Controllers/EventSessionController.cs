using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FinalLab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventSessionController : GenericControllerBase<EventSession,int>
    {
        public EventSessionController(IGenericService<EventSession,int> service) : base(service)
        {
        }
    }
}
