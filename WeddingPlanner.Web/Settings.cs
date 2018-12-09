namespace WeddingPlanner.Web
{
    public class Settings
    {
		public string DbConnectionDbName { get; set; }

		public string DbConnectionServer { get; set; }

		public string DbConnectionLogin { get; set; }

		public string DbConnectionPassword { get; set; }

		public string JwtAudience { get; set; }

		public int JwtExpireMinutes { get; set; }

		public string JwtIssuer { get; set; }

		public string JwtSecretKey { get; set; }
	}
}
