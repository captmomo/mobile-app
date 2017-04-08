﻿using System;

namespace LH.Forcas
{
    using System.Globalization;
    using LH.Forcas.Extensions;
    using LH.Forcas.Localization;
    using LH.Forcas.Services;
    using LH.Forcas.Views.Dashboard;
    using LH.Forcas.Views.Root;
    using Microsoft.Practices.Unity;
    using Prism.Unity;
    using Storage;
    using Storage.Data;
    using Views;
    using Views.Accounts;
    using Views.Categories;
    using Views.Settings;

    public partial class App : IApp
    {
        public Version AppVersion { get; private set; }

        public static CultureInfo CurrentCultureInfo { get; private set; }

        protected override void OnInitialized()
        {
            this.InitializeComponent();

            NavigationExtensions.InitializeNavigation();

            this.Container.Resolve<IPathResolver>().Initialize();
            var dbManager = this.Container.Resolve<IDbManager>();
            dbManager.Initialize();

            this.AmountToCurrencyStringConverter.RefDataService = this.Container.Resolve<IRefDataService>();

#if DEBUG
            TestData.InsertTestData(dbManager);
#endif

            this.Container.Resolve<IUserSettingsService>().Initialize();

            CurrentCultureInfo = this.Container.Resolve<ILocale>().GetCultureInfo();

            #pragma warning disable 4014
            this.NavigationService.NavigateToDashboard();
            #pragma warning restore 4014
        }

        protected override void RegisterTypes()
        {
            this.Container.RegisterType<IDbManager, DbManager>(new ContainerControlledLifetimeManager());

            this.Container.RegisterType<IRefDataRepository, RefDataRepository>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IUserDataRepository, UserDataRepository>(new ContainerControlledLifetimeManager());

            this.Container.RegisterType<IAccountingService, AccountingService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IRefDataService, RefDataService>(new ContainerControlledLifetimeManager());
            this.Container.RegisterType<IUserSettingsService, UserSettingsService>(new ContainerControlledLifetimeManager());

            this.Container.RegisterTypeForNavigation<RootSideMenuPage>();
            this.Container.RegisterTypeForNavigation<RootTabPage>();
            this.Container.RegisterTypeForNavigation<RootSideMenuPage>();
            this.Container.RegisterTypeForNavigation<GenericNavigationPage>();

            this.Container.RegisterTypeForNavigation<DashboardPage>();
            this.Container.RegisterTypeForNavigation<DashboardNavigationPage>();

            this.Container.RegisterTypeForNavigation<AccountsListPage>();
            this.Container.RegisterTypeForNavigation<AccountsDetailPage>();
            this.Container.RegisterTypeForNavigation<AccountsNavigationPage>();

            this.Container.RegisterTypeForNavigation<CategoriesListPage>();
            this.Container.RegisterTypeForNavigation<CategoriesDetailPage>();

            this.Container.RegisterTypeForNavigation<SettingsPage>();
        }
    }
}
