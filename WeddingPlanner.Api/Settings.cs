namespace WeddingPlanner.Api
{
    public class Settings
    {
		public string DbConnectionServer { get; set; }

		public string DbConnectionLogin { get; set; }

		public string DbConnectionPassword { get; set; }

		public int JwtExpireMinutes { get; set; }

		public string JwtIssuer { get; set; }

		public string JwtSecretKey { get; set; }

		public string CertificatePassword { get; set; }
	}
}
