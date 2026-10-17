using Bank_Application.Dtos;
using Bank_Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;
        public BankController(IBankService bankService)
        {
                _bankService = bankService;

        }
        /// <summary>
        /// this API use to Add Bank Details
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddBankAsynch")]
        public async Task<ActionResult<BankResponseDto>> AddBankAsynch(CreateBankDto dto,CancellationToken cancellationToken)
        {
            var result = await _bankService.AddBankAsynch(dto,cancellationToken);
            return CreatedAtAction(nameof(AddBankAsynch),result);
        
        }
    }
}
