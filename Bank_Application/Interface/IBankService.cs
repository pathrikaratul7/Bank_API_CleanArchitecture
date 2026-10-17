using Bank_Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Interface
{
    public interface IBankService
    {
        Task<BankResponseDto> AddBankAsynch(CreateBankDto createBankDto, CancellationToken cancellationToken);
    }
}
