using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WeddingPlanner.Models.Dtos
{
	public class PasswordResult
	{
		public double BitsOfEntropy { get; set; }

		public bool IsError => Error != PasswordError.None;

		[JsonConverter(typeof(StringEnumConverter))]
		public PasswordError Error { get; set; } = PasswordError.None;
	}

	[Flags]
	public enum PasswordError
	{
		None = 0,
		TooShort = 1,
		TooCommon = 2,
		NotComplexEnough = 4
	}
}
