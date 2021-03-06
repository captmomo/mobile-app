﻿using System;
using LH.Forcas.Domain.UserData;
using LH.Forcas.Domain.UserData.Authorization;

namespace LH.Forcas.Banking.Providers.Dummy
{
    [BankProviderInfo(typeof(StaticTokenAuthorization), "RB", "UCB")]
    public class DummyBankProvider : IBankProvider
    {
        public void Initialize(BankAuthorizationBase authorizationBase) { }

        public RemoteAccountInfo[] FetchAccounts()
        {
            return new[]
            {
                new RemoteAccountInfo()
                //{
                //    CurrencyId = "CZK",
                //    CurrentBalance = 56.88m,
                //    AccountNumber = AccountNumber.Parse("11-12316456456/5500"),
                //    Type = BankAccountType.Checking
                //},
                //new BankAccount
                //{
                //    CurrencyId = "EUR",
                //    CurrentBalance = 12.88m,
                //    AccountNumber = AccountNumber.Parse("11-12316456488/5500"),
                //    Type = BankAccountType.CreditCard
                //},
                //new BankAccount
                //{
                //    CurrencyId = "CZK",
                //    CurrentBalance = 99.88m,
                //    AccountNumber = AccountNumber.Parse("11-12316456499/5500"),
                //    Type = BankAccountType.Savings
                //}
            };
        }

        public Transaction[] FetchTransactions(Account account, DateTime lastDownloadTime)
        {
            throw new NotImplementedException();
        }
    }
}