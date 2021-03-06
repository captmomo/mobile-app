﻿using System;
using System.Collections.Generic;
using LH.Forcas.Domain.UserData;
using LH.Forcas.Events;
using LH.Forcas.Storage;
using LH.Forcas.Storage.Caching;
using Moq;
using MvvmCross.Plugins.Messenger;
using NUnit.Framework;

namespace LH.Forcas.Tests.Storage.Caching
{
    [TestFixture]
    public class UserDataRepositoryCacheTests
    {
        private Action<TrimMemoryRequestedEvent> trimMemoryEventCallback;

        protected UserDataRepositoryCache Cache;
        protected Mock<IUserDataRepository> RepositoryMock;
        protected Mock<IMvxMessenger> MessengerMock;

        [SetUp]
        public void Setup()
        {
            this.RepositoryMock = new Mock<IUserDataRepository>();
            this.MessengerMock = new Mock<IMvxMessenger>();
            this.MessengerMock.SetupMessengerSubscribe<TrimMemoryRequestedEvent>(action => this.trimMemoryEventCallback = action);

            this.Cache = new UserDataRepositoryCache(
                this.RepositoryMock.Object,
                this.MessengerMock.Object);
        }

        protected void PublishTrimMemoryEvent(TrimMemorySeverity severity)
        {
            var evt = new TrimMemoryRequestedEvent(this)
            {
                Severity = severity
            };

            this.trimMemoryEventCallback.Invoke(evt);
        }

        public class WhenHandlingUserSettings : UserDataRepositoryCacheTests
        {
            [Test]
            public void ThenShouldHandleSubsequentCallsFromCache()
            {
                this.RepositoryMock.Setup(x => x.GetUserSettings()).Returns(new UserSettings());

                this.Cache.GetUserSettings();
                this.Cache.GetUserSettings();

                this.RepositoryMock.Verify(x => x.GetUserSettings(), Times.Once);
            }

            [Test]
            public void ThenSaveShouldInvalidateCache()
            {
                this.RepositoryMock.Setup(x => x.GetUserSettings()).Returns(new UserSettings());

                this.Cache.GetUserSettings();
                this.Cache.SaveUserSettings(new UserSettings());
                this.Cache.GetUserSettings();

                this.RepositoryMock.Verify(x => x.GetUserSettings(), Times.Exactly(2));
            }

            [Test]
            public void ThenSaveShouldCallRepository()
            {
                this.RepositoryMock.Setup(x => x.SaveUserSettings(It.IsAny<UserSettings>()));

                this.Cache.SaveUserSettings(new UserSettings());
                
                this.RepositoryMock.VerifyAll();
            }
        }

        public class WhenHandlingAccounts : UserDataRepositoryCacheTests
        {
            [Test]
            public void ThenShouldHandleSubsequentCallsFromCache()
            {
                this.RepositoryMock.Setup(x => x.GetAccounts()).Returns(new List<Account> { new CashAccount()});

                this.Cache.GetAccounts();
                this.Cache.GetAccounts();

                this.RepositoryMock.Verify(x => x.GetAccounts(), Times.Once);
            }

            [Test]
            public void ThenSaveShouldInvalidateCache()
            {
                this.RepositoryMock.Setup(x => x.GetAccounts()).Returns(new List<Account> { new CashAccount() });

                this.Cache.GetAccounts();
                this.Cache.SaveAccount(new CashAccount());
                this.Cache.GetAccounts();

                this.RepositoryMock.Verify(x => x.GetAccounts(), Times.Exactly(2));
            }

            [Test]
            public void ThenSaveShouldCallRepository()
            {
                this.RepositoryMock.Setup(x => x.SaveAccount(It.IsAny<Account>()));

                this.Cache.SaveAccount(new CashAccount());

                this.RepositoryMock.VerifyAll();
            }

            [Test]
            public void ThenDeleteShouldInvalidateCache()
            {
                this.RepositoryMock.Setup(x => x.GetAccounts()).Returns(new List<Account> { new CashAccount() });

                this.Cache.GetAccounts();
                this.Cache.SoftDeleteAccount(Guid.NewGuid());
                this.Cache.GetAccounts();

                this.RepositoryMock.Verify(x => x.GetAccounts(), Times.Exactly(2));
            }

            [Test]
            public void ThenDeleteShouldCallRepository()
            {
                var id = Guid.NewGuid();
                this.RepositoryMock.Setup(x => x.SoftDeleteAccount(id));

                this.Cache.SoftDeleteAccount(id);

                this.RepositoryMock.VerifyAll();
            }
        }

        public class WhenTrimmingMemory : UserDataRepositoryCacheTests
        {
            [Test]
            public void ThenShouldTrimUserSettingsOnReleaseAllRequest()
            {
                this.RepositoryMock.Setup(x => x.GetUserSettings()).Returns(new UserSettings());

                this.Cache.GetUserSettings();

                this.PublishTrimMemoryEvent(TrimMemorySeverity.ReleaseAll);

                this.Cache.GetUserSettings();

                this.RepositoryMock.Verify(x => x.GetUserSettings(), Times.Exactly(2));
            }
        }
    }
}