using Bank_Application.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Validation
{
    public class BankValidator : AbstractValidator<CreateBankDto>
    {
        public BankValidator() {

            RuleFor(x => x.Bankname).NotEmpty().WithMessage("Bank Name is required");
            RuleFor(x => x.BankType).NotEmpty().WithMessage("Bank Type is required");

        
        }
    }
}
