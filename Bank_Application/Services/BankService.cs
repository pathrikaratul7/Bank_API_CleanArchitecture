using Bank_Application.Dtos;
using Bank_Application.Interface;
using Bank_Domain.Entities;
using Bank_Domain.Services;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Bank_Application.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;
        private readonly IValidator<CreateBankDto> _createBankDtoValidator;
        private readonly IValidator<UpdateBankDto> _updateBankDtoValidator;
        private readonly IBankDomainService _bankDomainService;

        public BankService(
            IBankRepository bankRepository, 
            IValidator<CreateBankDto> validationRule,
            IBankDomainService bankDomainService,
            IValidator<UpdateBankDto> updateBankValidatorDto)
        {
            _bankRepository = bankRepository;
            _createBankDtoValidator = validationRule;
            _bankDomainService = bankDomainService;
            _updateBankDtoValidator = updateBankValidatorDto;
        }

        public async Task<BankResponseDto> AddBankAsynch(CreateBankDto createBankDto, CancellationToken cancellationToken)
        {
            // Step 1: Validate DTO (Application Layer responsibility)
            var validator = await _createBankDtoValidator.ValidateAsync(createBankDto, cancellationToken);
            if (!validator.IsValid)
            {
                throw new ValidationException(validator.Errors);
            }

            // Step 2: Use Domain Service to create entity (Domain Layer responsibility)
            var bank = _bankDomainService.ValidateDomainBank(
                createBankDto.Bankname, 
                createBankDto.BankType, 
                createBankDto.IsActive);

            // Step 3: Persist to repository (Infrastructure Layer responsibility)
            await _bankRepository.AddBankAsynch(bank, cancellationToken);

            // Step 4: Return response (Application Layer responsibility)
            return new BankResponseDto
            {
                Bank_ID  = bank.Bank_ID,
                Bankname = bank.BankName,
                BankType = bank.BankType,
                IsActive = bank.IsActive,
            };
        }
        public async Task<BankResponseDto> UpdateBankAsynch(UpdateBankDto update, CancellationToken cancellationToken)
        { 
            var validator = await _updateBankDtoValidator.ValidateAsync(update, cancellationToken);
            if (!validator.IsValid)
            {
                throw new ValidationException(validator.Errors);
            }
            var Updatebank = _bankDomainService.ValidateDomainBank(update.Bankname, update.BankType, update.IsActive, update.Bank_ID);
            await _bankRepository.UpdateBankAsynch(Updatebank, cancellationToken);

            return new BankResponseDto
            {
                Bank_ID = Updatebank.Bank_ID,
                Bankname = Updatebank.BankName,
                BankType = Updatebank.BankType,
                IsActive = Updatebank.IsActive,
            };
        
        }
    }
}
