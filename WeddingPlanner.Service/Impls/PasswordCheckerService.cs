using System;
using System.Linq;
using WeddingPlanner.Models.Dtos;
using WeddingPlanner.Service.Interfaces;
using WeddingPlanner.Service.Resources;

namespace WeddingPlanner.Service.Impls
{
	public class PasswordCheckerService : IPasswordCheckerService
	{
		public const string LowerSet = @"abcdefghijklmnopqrstuvwxyz";

		public const string UpperSet = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public const string NumberSet = @"0123456789";

		public const string SymbolSet1 = @"!@#$%^&*()";

		public const string SymbolSet2 = @"`~-_=+[{]}\|;:'"",<.>/?";

		/// <summary>
		/// http://rumkin.com/tools/password/passchk.php
		/// </summary>
		/// <param name="password"></param>
		/// <param name="threshold"></param>
		/// <returns></returns>
		public PasswordResult CheckPassword(string password, double threshold = 50)
		{
			var result = new PasswordResult();
			if (string.IsNullOrWhiteSpace(password) || password.Length < 2)
			{
				result.BitsOfEntropy = 0;
				result.Error = PasswordError.TooShort;
				return result;
			}

			double bits = 0;
			var isCommon = IsCommonPassword(password);
			var len = password.Length;
			var plower = password.ToLowerInvariant();

			var aidx = GetIndex(plower[0]);
			for (var b = 1; b < len; b++)
			{
				var bidx = GetIndex(plower[b]);
				var parseOk = double.TryParse(ResourceFile.FrequencyTable.ToArray()[aidx * 27 + bidx], out var freq);
				var c = 1.0 - (parseOk ? freq : 0);
				bits += Math.Log(GetCharacterSet(password), 2) * c * c;
				aidx = bidx;
			}

			result.BitsOfEntropy = bits;
			if (isCommon) result.Error = PasswordError.TooCommon;
			if (len < 8) result.Error = result.Error | PasswordError.TooShort;
			if (bits < threshold) result.Error = result.Error | PasswordError.NotComplexEnough;
			return result;
		}

		private static bool IsCommonPassword(string password)
		{
			return ResourceFile.CommonPasswords.Contains(password.ToLowerInvariant());
		}

		private static int GetIndex(char c)
		{
			if (c < 'a' || c > 'z') return 0;
			return c - 'a' + 1;
		}

		private static int GetCharacterSet(string password)
		{
			bool containsLower = false,
				containsUpper = false,
				containsNumber = false,
				containsSym1 = false,
				containsSym2 = false,
				containsSpace = false,
				containsOther = false;
			var characters = 0;

			foreach (var c in password)
			{
				var ch = c.ToString();
				if (!containsLower && LowerSet.Contains(ch))
				{
					characters += LowerSet.Length;
					containsLower = true;
				}
				if (!containsUpper && UpperSet.Contains(ch))
				{
					characters += UpperSet.Length;
					containsUpper = true;
				}
				if (!containsNumber && NumberSet.Contains(ch))
				{
					characters += NumberSet.Length;
					containsNumber = true;
				}
				if (!containsSym1 && SymbolSet1.Contains(ch))
				{
					characters += SymbolSet1.Length;
					containsSym1 = true;
				}
				if (!containsSym2 && SymbolSet2.Contains(ch))
				{
					characters += SymbolSet2.Length;
					containsSym2 = true;
				}
				if (!containsSpace && c == ' ')
				{
					characters += 1;
					containsSpace = true;
				}
				if (!containsOther && (c < ' ' || c > '~'))
				{
					characters += 32 + 128;
					containsOther = true;
				}
			}
			return characters;
		}
	}
}