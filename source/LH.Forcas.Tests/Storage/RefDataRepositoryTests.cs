﻿using System.Linq;
using LH.Forcas.Storage;
using NUnit.Framework;

namespace LH.Forcas.Tests.Storage
{
    using Forcas.Domain.RefData;

    [TestFixture]
    public class RefDataRepositoryTests
    {
        private IRefDataRepository refDataRepository;

        [SetUp]
        public void Setup()
        {
            this.refDataRepository = new RefDataRepository();
        }

        [TestFixture]
        public class LoadDataTests : RefDataRepositoryTests
        {
            [Test]
            public void LoadBanks()
            {
                var banks = this.refDataRepository.GetBanks();
                var rb = banks.SingleOrDefault(x => x.BankId == "RB");

                Assert.IsNotNull(rb);
                Assert.IsNotEmpty(rb.Name);
                Assert.IsNotEmpty(rb.CountryId);
                Assert.IsNotEmpty(rb.IbanPrefix);
                Assert.AreNotEqual(0, rb.RoutingCode);
                Assert.AreEqual(BankAuthorizationScheme.PerAccount, rb.AuthorizationScheme);
                Assert.IsTrue(rb.IsActive);
            }

            [Test]
            public void LoadCountries()
            {
                var countries = this.refDataRepository.GetCountries();
                var cze = countries.SingleOrDefault(x => x.CountryId == "CZE");

                Assert.IsNotNull(cze);
                Assert.IsNotEmpty(cze.DefaultCurrencyCode);
            }

            [Test]
            public void LoadCurrencies()
            {
                var currencies = this.refDataRepository.GetCurrencies();
                var cze = currencies.SingleOrDefault(x => x.CurrencyId == "CZK");

                Assert.IsNotNull(cze);
                Assert.IsNotEmpty(cze.DisplayName);
                Assert.IsNotEmpty(cze.Symbol);
                Assert.AreEqual(PrefferedCcySymbolLocation.After, cze.PreferedSymbolPosition);
            }
        }
    }
}