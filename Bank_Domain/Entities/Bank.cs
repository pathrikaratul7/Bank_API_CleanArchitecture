using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Domain.Entities
{
    public class Bank 
    {
        public long Bank_ID { get; private set; }
        public string BankName { get; private set; } = null!;
        public string BankType { get; private set;} = null!;
        public bool IsActive { get; private set; }

        public Bank(string BankName , string BankType , bool IsActive)
        {
            if (string.IsNullOrEmpty(BankName))
                throw new ArgumentException("Bank Name is require");
            if (string.IsNullOrEmpty(BankType))
                throw new ArgumentException("Bank Type is require");



            this.BankName = BankName;
            this.BankType = BankType;
            this.IsActive = IsActive;
        }

        public  void UpdateBank(Bank bnk)
        {
            if (bnk == null) throw new ArgumentNullException("bank entities should not be empty");

            if (string.IsNullOrEmpty(bnk.BankName))
                throw new ArgumentException("Bank name is require");
            if (string.IsNullOrEmpty(bnk.BankType))
                throw new ArgumentException("Bank Type is require");

            
            BankName = bnk.BankName;
            BankType = bnk.BankType;
            IsActive = bnk.IsActive;
           
        }
    }
}
