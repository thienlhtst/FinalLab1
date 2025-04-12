using FinalLab1.Entities;
using FinalLab1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FinalLab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : GenericControllerBase<BankAccount,int>
    {
        public BankAccountController(IGenericService<BankAccount,int> service) : base(service)
        {
        }
    }
}
