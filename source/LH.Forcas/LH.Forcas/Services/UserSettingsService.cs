﻿namespace LH.Forcas.Services
{
    using Domain.UserData;
    using Storage;

    public class UserSettingsService : IUserSettingsService
    {
        private readonly IRefDataService refDataService;
        private readonly IDeviceService deviceService;
        private readonly IUserDataRepository dataRepository;

        public UserSettingsService(IUserDataRepository dataRepository, IRefDataService refDataService, IDeviceService deviceService)
        {
            this.dataRepository = dataRepository;
            this.refDataService = refDataService;
            this.deviceService = deviceService;
        }

        public UserSettings Settings { get; private set; }

        public void Initialize()
        {
            this.Settings = this.dataRepository.GetUserSettings();

            if (this.Settings == null)
            {
                // TODO: Handle when device can't return country code -> switch to UK/English or cs-CZ
                var country = this.refDataService.GetCountry(this.deviceService.CountryCode);

                this.Settings = new UserSettings();
                this.Settings.DefaultCountryId = country.CountryId;
                this.Settings.DefaultCurrencyId = country.DefaultCurrencyId;

                this.dataRepository.SaveUserSettings(this.Settings);
            }
        }

        public void Save()
        {
            this.dataRepository.SaveUserSettings(this.Settings);
        }
    }
}