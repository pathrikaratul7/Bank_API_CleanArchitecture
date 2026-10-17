using Bank_Application.Dtos;
using Bank_Application.Interface;
using Bank_Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Application.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository = null!;
        private readonly IValidator<CreateBankDto> _createBankDtoValidator;

        public BankService(IBankRepository bankRepository, IValidator<CreateBankDto> validationRule)
        {
                _bankRepository = bankRepository;
            _createBankDtoValidator = validationRule;
        }
        public async Task<BankResponseDto> AddBankAsynch(CreateBankDto createBankDto, CancellationToken cancellationToken)
        {

            var validator = await _createBankDtoValidator.ValidateAsync(createBankDto, cancellationToken);
            if (!validator.IsValid)
            {

                throw new ValidationException(validator.Errors);
            
            }

            var Bank = new Bank(createBankDto.Bankname, createBankDto.BankType, createBankDto.IsActive);
            await _bankRepository.AddBankAsynch(Bank,cancellationToken);



            return new BankResponseDto
            {
                UserID = Bank.Bank_ID,
                Bankname = Bank.BankName,
                BankType = Bank.BankType,
                IsActive = Bank.IsActive,
            };




        }
    }
}
