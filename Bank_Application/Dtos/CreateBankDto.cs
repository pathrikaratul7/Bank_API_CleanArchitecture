using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Dtos
{
    public class CreateBankDto
    {
        public string Bankname { get; set; } = null!;
        public string BankType { get; set; } = null!;
        public bool IsActive { get; set; }= false;
    }
}
