using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Dtos
{
    public class BankResponseDto : CreateBankDto
    {
        public long UserID {  get; set; }
    }
}
