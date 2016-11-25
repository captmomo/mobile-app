﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http.Testing;
using LH.Forcas.Contract;
using LH.Forcas.Integration;
using Moq;
using NUnit.Framework;

namespace LH.Forcas.Tests.Integration
{
    [TestFixture]
    public class GitHubRefDataDownloaderTests
    {
        private static readonly string CommitsNoLastSyncResponse = Extensions.LoadFileContents("Integration\\GitHubRefDataResponses\\Commits-NoLastSync.json");
        private static readonly string CommitsOneNewCommitResponse = Extensions.LoadFileContents("Integration\\GitHubRefDataResponses\\Commits-OneNewCommit.json");
        private static readonly string CommitsNoNewCommitResponse = Extensions.LoadFileContents("Integration\\GitHubRefDataResponses\\Commits-NoNewCommit.json");

        private static readonly string ContentValidFile = Extensions.LoadFileContents("Integration\\GitHubRefDataResponses\\Content-ValidFile.json");

        [SetUp]
        public void Setup()
        {
            this.flurlTest = new HttpTest();
            this.appProperties = new Dictionary<string, object>();
            this.crashReporter = new Mock<ICrashReporter>(MockBehavior.Strict);

            var appConstantsMock = new Mock<IAppConstants>();
            appConstantsMock.SetupGet(x => x.ConfigDataGitHubRepoUrl).Returns("https://github.com/repos/magic/");
            appConstantsMock.SetupGet(x => x.ConfigDataMaxAge).Returns(TimeSpan.FromMilliseconds(1));
            appConstantsMock.SetupGet(x => x.HttpRequestRetryCount).Returns(3);
            appConstantsMock.SetupGet(x => x.HttpRequestRetryWaitTimeBase).Returns(0);

            var appMock = new Mock<IApp>();
            appMock.SetupGet(x => x.Constants).Returns(appConstantsMock.Object);
            appMock.SetupGet(x => x.Properties).Returns(this.appProperties);
            
            this.downloader = new GitHubRefDataDownloader(appMock.Object, crashReporter.Object);            
        }

        [TearDown]
        public void TearDown()
        {
            this.flurlTest.Dispose();
        }

        private HttpTest flurlTest;
        private Mock<ICrashReporter> crashReporter;
        private GitHubRefDataDownloader downloader;
        private IDictionary<string, object> appProperties;

        [Test]
        public async Task SyncWithNoLastSyncShouldReturnUpdate()
        {
            this.flurlTest.RespondWith(CommitsNoLastSyncResponse);
            this.flurlTest.RespondWith(ContentValidFile);

            var result = await this.downloader.GetUpdatedFiles();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Banks);
        }

        [Test]
        public async Task SyncWithLastSyncShouldReturnUpdate()
        {
            this.flurlTest.RespondWith(CommitsOneNewCommitResponse);
            this.flurlTest.RespondWith(ContentValidFile);

            var result = await this.downloader.GetUpdatedFiles();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Banks);
        }

        [Test]
        public async Task ShouldReturnEmptyIfNoUpdatesAreAvailable()
        {
            this.flurlTest.RespondWith(CommitsNoNewCommitResponse);
            
            var result = await this.downloader.GetUpdatedFiles();

            Assert.IsNull(result);
            Assert.AreEqual(1, this.flurlTest.CallLog.Count);
        }

        [Test]
        public async Task ShouldRetryOnError()
        {
            this.flurlTest.RespondWith(string.Empty, 429);
            this.flurlTest.RespondWith(string.Empty, 404);
            this.flurlTest.RespondWith(CommitsNoNewCommitResponse);

            var results = await this.downloader.GetUpdatedFiles();

            Assert.IsNull(results);
            Assert.AreEqual(3, this.flurlTest.CallLog.Count);
        }

        [Test]
        public async Task ShouldRetryOnTimeout()
        {
            this.flurlTest.SimulateTimeout();
            this.flurlTest.SimulateTimeout();
            this.flurlTest.RespondWith(CommitsNoNewCommitResponse);

            var results = await this.downloader.GetUpdatedFiles();

            Assert.IsNull(results);
            Assert.AreEqual(3, this.flurlTest.CallLog.Count);
        }

        [Test]
        public async Task ShouldReportIfSyncFailingForTooLong()
        {
            this.flurlTest.SimulateTimeout();
            this.flurlTest.SimulateTimeout();
            this.flurlTest.SimulateTimeout();

            var lastSyncTime = DateTime.MinValue.ToUniversalTime();
            this.appProperties.Add(App.LastRefDataSyncPropertyKey, lastSyncTime);

            this.crashReporter
                .Setup(x => x.ReportException(It.IsAny<Exception>()))
                .Returns(Task.FromResult(0));

            var result = await this.downloader.GetUpdatedFiles();

            Assert.IsNull(result);

            this.crashReporter.Verify(x => x.ReportException(It.IsAny<Exception>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotReportOnTransientError()
        {
            this.flurlTest.SimulateTimeout();
            this.flurlTest.SimulateTimeout();
            this.flurlTest.SimulateTimeout();

            var lastSyncTime = DateTime.UtcNow;
            this.appProperties.Add(App.LastRefDataSyncPropertyKey, lastSyncTime);

            var result = await this.downloader.GetUpdatedFiles();

            Assert.IsNull(result);
        }
    }
}