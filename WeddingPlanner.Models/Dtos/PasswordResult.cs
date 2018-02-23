using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeddingPlanner.Models.Enums;

namespace WeddingPlanner.Models.Dtos
{
	public class PasswordResult
	{
		public float BitsOfEntropy { get; set; }

		public PasswordStrength Strength { get; set; }

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
