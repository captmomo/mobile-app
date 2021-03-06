﻿using System;
using System.Linq;
using Chance.MvvmCross.Plugins.UserInteraction;
using LH.Forcas.Banking;
using LH.Forcas.Domain.UserData;
using LH.Forcas.Extensions;
using LH.Forcas.RefDataContract;
using LH.Forcas.Services;
using LH.Forcas.ViewModels.Accounts;
using Moq;
using MvvmCross.Core.Navigation;
using NUnit.Framework;

namespace LH.Forcas.Tests.ViewModels.Accounts
{
    //public class AccountsDetailPageViewModelTests
    //{
    //    public AccountsDetailPageViewModelTests()
    //    {
    //        this.AccountId = Guid.NewGuid();

    //        this.RefDataServiceMock = new Mock<IRefDataService>();
    //        this.AccountingServiceMock = new Mock<IAccountingService>();
    //        this.UserInteractionMock = new Mock<IUserInteraction>();
    //        this.NavigationServiceMock = new Mock<IMvxNavigationService>();
    //        this.UserSettingsServiceMock = new Mock<IUserSettingsService>();

    //        this.ViewModel = new AccountsDetailPageViewModel(
    //            this.AccountingServiceMock.Object,
    //            this.RefDataServiceMock.Object,
    //            this.UserSettingsServiceMock.Object,
    //            this.NavigationServiceMock.Object,
    //            this.UserInteractionMock.Object);

    //        var banks = new[]
    //        {
    //            new Bank { BankId = "TestBank", Name = "Test Bank Name", CountryId = "CZ" },
    //            new Bank { BankId = "OtherBank", Name = "Test Bank Name", CountryId = "GB" }
    //        };

    //        var countries = new[]
    //        {
    //            new Country { CountryId = "GB", DefaultCurrencyId = "GBP" },
    //            new Country { CountryId = "CZ", DefaultCurrencyId = "CZK" }
    //        };

    //        var currencies = new[]
    //        {
    //            new Currency { CurrencyId  = "CZK", IsActive = true, DisplayFormat = "{0} Kč" },
    //            new Currency { CurrencyId  = "GBP", IsActive = true, DisplayFormat = "£{0}" },
    //        };

    //        this.UserSettingsServiceMock.Setup(x => x.Settings).Returns(new UserSettings { DefaultCountryId = "CZ" });
    //        this.RefDataServiceMock.Setup(x => x.GetBanks()).Returns(banks);
    //        this.RefDataServiceMock.Setup(x => x.GetCountries()).Returns(countries);
    //        this.RefDataServiceMock.Setup(x => x.GetCurrencies()).Returns(currencies);
    //    }

    //    protected Guid AccountId { get; }

    //    protected Mock<IRefDataService> RefDataServiceMock { get; }

    //    protected Mock<IAccountingService> AccountingServiceMock { get; }

    //    protected Mock<IUserInteraction> UserInteractionMock { get; }

    //    protected Mock<IMvxNavigationService> NavigationServiceMock { get; }

    //    protected Mock<IUserSettingsService> UserSettingsServiceMock { get; }

    //    protected AccountsDetailPageViewModel ViewModel { get; }

    //    public class FullScenarioTests : AccountsDetailPageViewModelTests
    //    {
    //        [Test]
    //        public void EditAndSaveBankAccountTest()
    //        {
    //            var differentAccount = this.GetDifferentBankAccount();

    //            this.NavigationServiceMock.Setup(x => x.GoBackAsync(null, null, true)).ReturnsAsync(true);

    //            this.AccountingServiceMock.Setup(x => x.SaveAccount(It.Is<Account>(acc => this.CompareAccounts(differentAccount, acc, false))));
    //            this.AccountingServiceMock.Setup(x => x.GetAvailableRemoteAccounts(It.IsAny<string>())).ReturnsAsync(new[] { this.GetRemoteAccount() });

    //            this.NavigateToEdit(this.GetValidBankAccount());

    //            var differentAccountBank = this.ViewModel.Banks.Single(x => x.BankId == differentAccount.BankId);

    //            this.ViewModel.AccountName = differentAccount.Name;
    //            this.ViewModel.SelectedCountry = this.ViewModel.Countries.Single(x => x.CountryId == differentAccountBank.CountryId);
    //            this.ViewModel.SelectedBank = differentAccountBank;

    //            Assert.True(this.ViewModel.SaveCommand.CanExecute());
    //            this.ViewModel.SaveCommand.Execute();

    //            this.NavigationServiceMock.VerifyAll();
    //            this.AccountingServiceMock.VerifyAll();
    //        }

    //        [Test]
    //        public void CreateAndSaveBankAccountTest()
    //        {
    //            var validAccount = this.GetValidBankAccount();
    //            // TODO: Setup NavigateBack
    //            //this.NavigationServiceMock.Setup(x => x.Close().GoBackAsync(null, null, true)).ReturnsAsync(true);

    //            this.AccountingServiceMock.Setup(x => x.SaveAccount(It.Is<Account>(acc => this.CompareAccounts(validAccount, acc, true))));
    //            this.AccountingServiceMock.Setup(x => x.GetAvailableRemoteAccounts(It.IsAny<string>())).ReturnsAsync(new[] { this.GetRemoteAccount() });

    //            this.ViewModel.Appearing();

    //            var bank = this.ViewModel.Banks.Single(x => x.BankId == validAccount.BankId);

    //            this.ViewModel.SelectedAccountType = typeof(BankAccount);
    //            this.ViewModel.AccountName = validAccount.Name;
    //            this.ViewModel.SelectedCountry = this.ViewModel.Countries.Single(x => x.CountryId == bank.CountryId);
    //            this.ViewModel.SelectedBank = bank;
    //            this.ViewModel.WaitUntilNotBusy();

    //            this.ViewModel.SelectedRemoteAccount = this.ViewModel.RemoteAccounts.First();

    //            Assert.True(this.ViewModel.SaveCommand.CanExecute(), this.ViewModel.ValidationResults.BuildErrorsString());
    //            this.ViewModel.SaveCommand.Execute();

    //            this.AccountingServiceMock.VerifyAll();
    //            this.NavigationServiceMock.VerifyAll();
    //        }

    //        [Test]
    //        public void EditAndSaveCashAccountTest()
    //        {
    //            var differentAccount = this.GetDifferentCashAccount();
    //            this.AccountingServiceMock.Setup(x => x.SaveAccount(It.Is<Account>(acc => this.CompareAccounts(differentAccount, acc, false))));

    //            this.NavigateToEdit(this.GetValidCashAccount());

    //            this.ViewModel.AccountName = differentAccount.Name;
    //            this.ViewModel.SelectedCurrency = this.ViewModel.Currencies.Single(x => x.CurrencyId == differentAccount.CurrencyId);

    //            Assert.True(this.ViewModel.SaveCommand.CanExecute(), this.ViewModel.ValidationResults.BuildErrorsString());
    //            this.ViewModel.SaveCommand.Execute();

    //            this.AccountingServiceMock.VerifyAll();
    //        }

    //        [Test]
    //        public void CreateAndSaveCashAccountTest()
    //        {
    //            var validAccount = this.GetValidCashAccount();

    //            this.AccountingServiceMock.Setup(x => x.SaveAccount(It.Is<Account>(acc => this.CompareAccounts(validAccount, acc, true))));

    //            this.ViewModel.OnNavigatingTo(null);

    //            this.ViewModel.SelectedAccountType = typeof(CashAccount);
    //            this.ViewModel.AccountName = validAccount.Name;
    //            this.ViewModel.SelectedCurrency = this.ViewModel.Currencies.Single(x => x.CurrencyId == validAccount.CurrencyId);

    //            Assert.True(this.ViewModel.SaveCommand.CanExecute(), this.ViewModel.ValidationResults.BuildErrorsString());
    //            this.ViewModel.SaveCommand.Execute();

    //            this.AccountingServiceMock.VerifyAll();
    //        }

    //        [Test]
    //        public void EditAccountWithInvalidValueTest()
    //        {
    //            this.NavigateToEdit(this.GetValidCashAccount());
    //            this.ViewModel.AccountName = null;

    //            Assert.False(this.ViewModel.SaveCommand.CanExecute());
    //        }

    //        [Test]
    //        public void EditAccountSaveFailsTest()
    //        {
    //            this.AccountingServiceMock.Setup(x => x.SaveAccount(It.IsAny<Account>())).Throws<Exception>();
    //            this.UserInteractionMock.SetupConfirm();

    //            this.NavigateToEdit(this.GetValidCashAccount());
    //            this.ViewModel.AccountName = "Different Name";

    //            Assert.True(this.ViewModel.SaveCommand.CanExecute());
    //            this.ViewModel.SaveCommand.Execute();

    //            this.UserInteractionMock.VerifyAll();
    //            this.AccountingServiceMock.VerifyAll();
    //        }

    //        // Cash and Bank
    //        // Full & valid save (edit)
    //        // Full & valid save (new)
    //        // On save -> validate
    //        // Handle save failure
    //        // Handle init failure
    //    }

    //    public class InitializationTests : AccountsDetailPageViewModelTests
    //    {
    //        [Test]
    //        public void ShouldLoadAccountTypes()
    //        {
    //            this.ViewModel.OnNavigatingTo(null);

    //            Assert.NotNull(this.ViewModel.AccountTypes);
    //            Assert.AreEqual(2, this.ViewModel.AccountTypes.Count);
    //        }

    //        [Test]
    //        public void ShouldLoadRefData()
    //        {
    //            this.ViewModel.OnNavigatingTo(null);

    //            Assert.NotNull(this.ViewModel.Banks);
    //            Assert.AreEqual(2, this.ViewModel.Banks.Count);

    //            Assert.NotNull(this.ViewModel.Currencies);
    //            Assert.AreEqual(2, this.ViewModel.Currencies.Count);

    //            Assert.NotNull(this.ViewModel.Countries);
    //            Assert.AreEqual(2, this.ViewModel.Countries.Count);
    //        }

    //        [Test]
    //        public void PreferedCountryShouldBeSelectedWhenCreatingAccount()
    //        {
    //            this.ViewModel.OnNavigatingTo(null);

    //            Assert.NotNull(this.ViewModel.SelectedCountry);
    //            Assert.AreEqual("CZ", this.ViewModel.SelectedCountry.CountryId);
    //        }

    //        [Test]
    //        public void ShouldSwitchToNewAccountModeIfNavigatedWithoutParams()
    //        {
    //            this.ViewModel.OnNavigatingTo(null);

    //            Assert.IsNotEmpty(this.ViewModel.Title);
    //            Assert.True(string.IsNullOrEmpty(this.ViewModel.AccountName));
    //            Assert.True(this.ViewModel.CanEditAccountType);
    //        }

    //        [Test]
    //        public void ShouldLoadExistingAccountIfNavigatedWithParams()
    //        {
    //            var editedAccount = this.GetValidBankAccount();

    //            this.NavigateToEdit(editedAccount);

    //            Assert.AreEqual(editedAccount.Name, this.ViewModel.Title);
    //            Assert.AreEqual(editedAccount.Name, this.ViewModel.AccountName);
    //            Assert.AreEqual(typeof(BankAccount), this.ViewModel.SelectedAccountType);
    //            Assert.AreEqual(editedAccount.BankId, this.ViewModel.SelectedBank.BankId);
    //            Assert.AreEqual(this.ViewModel.SelectedBank.CountryId, this.ViewModel.SelectedCountry.CountryId);

    //            Assert.False(this.ViewModel.CanEditAccountType);

    //            this.AccountingServiceMock.VerifyAll();
    //        }

    //        [Test]
    //        public void ShouldDisplayErrorAndNavigateBackIfAccountNotFound()
    //        {
    //            var account = this.GetValidBankAccount();
    //            var parameters = NavigationExtensions.CreateAccountDetailParameters(account.Id);

    //            this.AccountingServiceMock.Setup(x => x.GetAccount(account.Id)).Returns(default(Account));
    //            this.UserInteractionMock.Setup(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAwaitable();
    //            this.NavigationServiceMock.Setup(x => x.GoBackAsync(null, null, true)).ReturnsAsync(true);

    //            this.ViewModel.OnNavigatingTo(parameters);

    //            this.UserInteractionMock.VerifyAll();
    //            this.NavigationServiceMock.VerifyAll();
    //        }

    //        [Test]
    //        public void ShouldDisplayErrorAndNavigateBackIfInitFails()
    //        {
    //            var account = this.GetValidBankAccount();
    //            var parameters = NavigationExtensions.CreateAccountDetailParameters(account.Id);

    //            this.AccountingServiceMock.Setup(x => x.GetAccount(account.Id)).Throws<Exception>();
    //            this.UserInteractionMock.Setup(x => x.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAwaitable();
    //            this.NavigationServiceMock.Setup(x => x.GoBackAsync(null, null, true)).ReturnsAsync(true);

    //            this.ViewModel.OnNavigatingTo(parameters);

    //            this.UserInteractionMock.VerifyAll();
    //            this.NavigationServiceMock.VerifyAll();
    //        }
    //    }

    //    public class ValidationTests : AccountsDetailPageViewModelTests
    //    {
    //        [Test]
    //        public void CashAccountValidationTest()
    //        {
    //            this.ViewModel.OnNavigatingTo(null);

    //            this.ViewModel.TestPropertyValidation(x => x.SelectedAccountType, typeof(CashAccount), null);

    //            this.ViewModel.SelectedAccountType = typeof(CashAccount);

    //            this.ViewModel.TestPropertyValidation(x => x.AccountName, "My Account", null);
    //            this.ViewModel.TestPropertyValidation(x => x.SelectedCurrency, this.ViewModel.Currencies.SingleById("CZK"), null);
    //        }

    //        [Test]
    //        public void BankAccountValidationTest()
    //        {
    //            this.ViewModel.OnNavigatingTo(null);

    //            this.ViewModel.TestPropertyValidation(x => x.SelectedAccountType, typeof(BankAccount), null);

    //            this.ViewModel.SelectedAccountType = typeof(BankAccount);

    //            this.ViewModel.TestPropertyValidation(x => x.AccountName, "My Account", null);
    //            this.ViewModel.TestPropertyValidation(x => x.SelectedCurrency, this.ViewModel.Currencies.First(), null);
    //            this.ViewModel.TestPropertyValidation(x => x.SelectedBank, this.ViewModel.Banks.First(), null);
    //            this.ViewModel.TestPropertyValidation(x => x.SelectedCountry, this.ViewModel.Countries.First(), null);

    //            this.ViewModel.SelectedBank = this.ViewModel.Banks.First();
    //            this.ViewModel.WaitUntilNotBusy();

    //            this.ViewModel.TestPropertyValidation(x => x.SelectedRemoteAccount, this.ViewModel.RemoteAccounts.First(), null);
    //        }
    //    }

    //    protected void NavigateToEdit(Account account)
    //    {
    //        var parameters = NavigationExtensions.CreateAccountDetailParameters(this.AccountId);

    //        this.AccountingServiceMock
    //            .Setup(x => x.GetAccount(this.AccountId))
    //            .Returns(account);

    //        this.ViewModel.OnNavigatingTo(parameters);
    //    }

    //    private CashAccount GetValidCashAccount()
    //    {
    //        return new CashAccount
    //        {
    //            Name = "Some Cash Account",
    //            CurrencyId = "CZK"
    //        };
    //    }

    //    private CashAccount GetDifferentCashAccount()
    //    {
    //        return new CashAccount
    //        {
    //            Name = "Some Other Cash Account",
    //            CurrencyId = "GBP"
    //        };
    //    }

    //    private RemoteAccountInfo GetRemoteAccount()
    //    {
    //        return new RemoteAccountInfo
    //        {
    //            AccountNumber = AccountNumber.FromCzLocal("123/5500"),
    //            CurrencyId = "CZK"
    //        };
    //    }

    //    private BankAccount GetValidBankAccount()
    //    {
    //        return new CheckingAccount
    //        {
    //            AccountNumber = AccountNumber.FromCzLocal("123/5500"),
    //            BankId = "TestBank",
    //            CurrencyId = "CZK",
    //            Name = "My Test Account",
    //            CurrentBalance = new Amount(0m, "CZK")
    //        };
    //    }

    //    private BankAccount GetDifferentBankAccount()
    //    {
    //        return new CheckingAccount
    //        {
    //            AccountNumber = AccountNumber.FromCzLocal("456/5577"),
    //            BankId = "OtherBank",
    //            CurrencyId = "GBP",
    //            Name = "Another Account's Name",
    //            CurrentBalance = new Amount(0m, "GBP")
    //        };
    //    }

    //    private bool CompareAccounts(Account expected, Account actual, bool ignoreAccountId)
    //    {
    //        if (!ignoreAccountId)
    //        {
    //            Assert.AreEqual(expected.Id, actual.Id);
    //        }

    //        Assert.AreEqual(expected.Name, actual.Name);
    //        Assert.AreEqual(expected.CurrencyId, actual.CurrencyId);

    //        var expectedBank = expected as BankAccount;
    //        var actualBank = actual as BankAccount;

    //        if (expectedBank != null)
    //        {
    //            Assert.AreEqual(expectedBank.BankId, actualBank.BankId);
    //            Assert.AreEqual(expectedBank.AccountNumber, actualBank.AccountNumber);
    //        }

    //        return true;
    //    }
    //}
}