using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BashrLeadCreateAPI.Models.InputRequest
{
    public class InputRequest
    {
        [Required]
        public ApplicationArea applicationArea
        {
            get;
            set;
        }
        [Required]
        public DataArea dataArea
        {
            get;
            set;
        }
    }
    
    public class ApplicationArea
    {
        public string CountryOfOrigin
        {
            get;
            set;
        }
        [Required]
        public string SenderId
        {
            get;
            set;
        }
        [Required]
        public DateTime TransactionDateTime
        {
            get;
            set;
        }
        [Required]
        public string TransactionId
        {
            get;
            set;
        }
      
    }
    public class DataArea
    {
        [Required]
        public DateTime AppDateTime
        {
            get;
            set;
        }
        [Required]
        [StringLength(100, MinimumLength =10,  ErrorMessage = "This field must be 100 characters")]
        public string CompanyName
        {
            get;
            set;
        }
        [Required]
        [StringLength(20, MinimumLength = 10,ErrorMessage = "This field must be 20 characters")]
        public string CompanyShortName
        {
            get;
            set;
        }
        [Required]
        public DateTime CompanyEstabilshmentDate
        {
            get;
            set;
        }
        [Required]
        public string CountryOfIncorporation
        {
            get;
            set;
        }
        [Required]
        public string ResidentialStatus
        {
            get;
            set;
        }
        [Required]
        public string CustomerClassification
        {
            get;
            set;
        }
        [Required]
        public string EconomicActivity
        {
            get;
            set;
        }
        [Required]
        public string BusinessDurationinUAE
        {
            get;
            set;
        }
        [Required]
        public int NoOfEmployees
        {
            get;
            set;
        }
        [Required]
        public string tncAccepted
        {
            get;
            set;
        }
        [Required]
        public bool DoyouTradeWithListedCountries
        {
            get;
            set;
        }
        [Required]
        public Identification[] Identification
        {
            get;
            set;
        }
        [Required]
        public Address[] Address
        {
            get;
            set;
        }
        [Required]
        public Spoc[] Spoc
        {
            get;
            set;
        }
        [Required]
        public Party[] Parties
        {
            get;
            set;
        }
        [Required]
        public StakeHolders[] StakeHolders
        {
            get;
            set;
        }
        [Required]
        public SourceofIncome[] SourceOfIncome
        {
            get;
            set;
        }
        [Required]
        public KeyCounterParties[] KeyCounterParties
        {
            get;
            set;
        }
        [Required]
        public string ReferenceNumber
        {
            get;
            set;
        }
    }
    public class Identification
    {
        [Required]
        public string IdentificationNumber
        {
            get;
            set;
        }      
        public enum IdType
        {
            TradeLicense=0,
            NationalId=1,
            Passport= 2,

        }
        [Required]
        public string IdentityIssuePlace
        {
            get;
            set;
        }
        [Required]
        public DateTime IdentityIssueDate
        {
            get;
            set;
        }
        [Required]
        public DateTime IdentityExpiryDate
        {
            get;
            set;
        }
       
    }
    public class Address
    {
        public enum AddressType
        {
            Home = 0,
            Company = 1,
            Personal = 2,

        }
        [Required]
        public string AddressPreference
        {
            get;
            set;
        }
        [Required]
        public string AddressLine1
        {
            get;
            set;
        }
        [Required]
        public string AddressLine2
        {
            get;
            set;
        }
        [Required]
        public string City
        {
            get;
            set;
        }
        [Required]
        public string POBox
        {
            get;
            set;
        }
        [Required]
        public string PostalCode
        {
            get;
            set;
        }
        [Required]
        public string Country
        {
            get;
            set;
        }
    }
    public class Spoc
    {
        [Required]
        public string Name
        {
            get;
            set;
        }
        [Required]
        public string Designation
        {
            get;
            set;
        }
        [Required]
        public string MotherName
        {
            get;
            set;
        }
       
        [Required]
        public Contacts[] Contacts
        {
            get;
            set;
        }
    }
    public class Contacts
    {
        [Required]
        public string PhoneNumber
        {
            get;
            set;
        }
        [Required]
        [StringLength(10,MinimumLength =10,ErrorMessage ="Please Enter 10 Digits in Number")]
        public string MobileNumber
        {
            get;
            set;
        }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email
        {
            get;
            set;
        }
        [Required]
        public string Website
        {
            get;
            set;
        }
        [Required]
        public string ContactType
        {
            get;
            set;
        }
        [Required]
        public string PreferredMedium
        {
            get;
            set;
        }
    }
    public class Party
    {
        [Required]
        public string PartyType
        {
            get;
            set;
        }
        [Required]
        public string Name
        {
            get;
            set;
        }
        [Required]
        public Identification Identify
        {
            get;
            set;
        }
        [Required]
        public DateTime birthDate
        {
            get;
            set;
        }
        [Required]
        public string PlaceOfBirthCountry
        {
            get;
            set;
        }
        [Required]
        public string ResidentialStatus
        {
            get;
            set;
        }
        [Required]
        public string Nationality
        {
            get;
            set;
        }
        [Required]
        public bool IsBusinessRunByParty
        {
            get;
            set;
        }
        [Required]
        public Address Address
        {
            get;
            set;
        }
    }
    public class StakeHolders
    {
        [Required]
        public int ShareHolderPercentage
        {
            get;
            set;
        }
        [Required]
        public decimal CompanyTurnOver
        {
            get;
            set;
        }
        [Required]
        public decimal ProjectedCompanyTurnOver
        {
            get;
            set;
        }
        [Required]
        public decimal ExpectedAnnualTurnOver
        {
            get;
            set;
        }
        [Required]
        public string ExpectedTransactionType
        {
            get;
            set;
        }
        [Required]
        public int ExpectedMonthlyTransactions
        {
            get;
            set;
        }
        [Required]
        public int InwardTransferMonthly
        {
            get;
            set;
        }
        [Required]
        public int OutTransferMonthly
        {
            get;
            set;
        }
    }
    public class SourceofIncome
    {
        [Required]
        public string SecureFundDetails
        {
            get;
            set;
        }
        [Required]
        public string CountryOfSourceOfIncome
        {
            get;
            set;
        }
        [Required]
        public string DepositTypeSourceOfIncome
        {
            get;
            set;
        }
        [Required]
        public string PresentCapital
        {
            get;
            set;
        }
    }
    public class KeyCounterParties
    {
        [Required]
        public string SupplierCountry
        {
            get;
            set;
        }
        [Required]
        public string SupplierCompanyName
        {
            get;
            set;
        }
        [Required]
        public string SupplierPercentage
        {
            get;
            set;
        }
        [Required]
        public string SupplierPaymentMode
        {
            get;
            set;
        }
        [Required]
        public string CustomersCountry
        {
            get;
            set;
        }
        [Required]
        public string CustomersCompanyName
        {
            get;
            set;
        }
        [Required]
        public string CustomersPercentage
        {
            get;
            set;
        }
        [Required]
        public string CustomersPaymentMode
        {
            get;
            set;
        }
    }
}