namespace WeddingPlanner.Api
{
    public class Settings
    {
		public string DbConnectionServer { get; set; }

		public string DbConnectionLogin { get; set; }

		public string DbConnectionPassword { get; set; }

		public int JwtExpireDays { get; set; }

		public string JwtIssuer { get; set; }

		public string SecretKey { get; set; }
	}
}
