namespace LH.Forcas.Domain.RefData
{
    public class Bank : IIsActive
    {
        public string BankId { get; set; }

        public string Name { get; set; }

        public string CountryId { get; set; }

        public int BBAN { get; set; }

        public BankAuthorizationScope AuthorizationScheme { get; set; }

        public bool IsActive { get; set; }
    }
}