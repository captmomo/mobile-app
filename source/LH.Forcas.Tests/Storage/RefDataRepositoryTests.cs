﻿using System.Linq;
using System.Threading.Tasks;
using LH.Forcas.Domain.RefData;
using LH.Forcas.Storage;
using NUnit.Framework;

namespace LH.Forcas.Tests.Storage
{
    [TestFixture]
    public class RefDataRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
            AutoMapperConfig.Configure();

            this.dbManager = new TestsDbManager();
            this.dbManager.Initialize();

            this.refDataRepository = new RefDataRepository(this.dbManager);
        }

        [TearDown]
        public void TearDown()
        {
            this.dbManager.Dispose();
        }

        private TestsDbManager dbManager;
        private IRefDataRepository refDataRepository;

        [Test]
        public async Task ShouldLoadSavedEntity()
        {
            var countries = await this.refDataRepository.GetCountriesAsync();

            Assert.IsNotNull(countries);
            Assert.IsFalse(countries.Any());

            await this.SaveCountryUpdate("CZE", 1);

            countries = await this.refDataRepository.GetCountriesAsync();
            var actualCountry = countries.Single();

            Assert.AreEqual("CZE", actualCountry.Code);
        }

        [Test]
        public async Task ShouldNotSaveUpdateIfSameVersionAlreadyExists()
        {
            await this.SaveCountryUpdate("CZE", 1);

            var countries = await this.refDataRepository.GetCountriesAsync();
            Assert.IsTrue(countries.Any());

            await this.SaveCountryUpdate("UK", 1);

            countries = await this.refDataRepository.GetCountriesAsync();

            Assert.IsTrue(countries.Any());
            Assert.AreEqual("CZE", countries.Single().Code);
        }

        private async Task SaveCountryUpdate(string countryCode, int version)
        {
            var country = new Country { Code = countryCode, DefaultCurrencyCode = "CZK" };
            var update = new RefDataUpdate<Country> { TypedData = new[] { country }, DomainType = typeof(Country), Version = version };

            await this.refDataRepository.SaveRefDataUpdates(new IRefDataUpdate[] { update });
        }
    }
}