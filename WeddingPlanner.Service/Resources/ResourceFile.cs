using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WeddingPlanner.Service.Resources
{
    public class ResourceFile
	{
		static ResourceFile()
		{
			LoadResources();
		}

		public static IEnumerable<string> CommonPasswords { get; set; }
		public static IEnumerable<string> FrequencyTable { get; set; }

		private static void LoadResources()

		{
			typeof(ResourceFile)
				.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)
				.ToList()
				.ForEach(prop =>
				{
					prop.SetValue(typeof(ResourceFile),ReadResourceFile(prop.Name + ".txt"));
				});
		}

		private static IEnumerable<string> ReadResourceFile(string fileName)
		{
			using (var rs = Assembly.GetExecutingAssembly()
									.GetManifestResourceStream(typeof(ResourceFile), fileName))
			{
				using (var sr = new StreamReader(rs, Encoding.UTF8))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						yield return line;
					}
				}
			}
		}
	}
}
