using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml.Serialization;
using CsvHelper;
using JDMallen.Toolbox.Constants;

namespace WeddingPlanner.FileConverter
{
	public class Program
	{
		static void Main(string[] args)
		{
			var filePath = @"\\nas\jesse\Documents\merritt_contacts.csv";
			var sr = new StreamReader(filePath);

			var csv = new CsvReader(sr);

			var list = new List<Contact>();
			var i = 0;
			while (csv.Read())
			{
				var contact = new Contact();
				if (i == 0)
				{
					csv.ReadHeader();
				}

				contact.FirstName = csv.GetField<string>("First Name");
				contact.LastName = csv.GetField<string>("Last Name");
				contact.Address = csv.GetField<string>("Other Address");
				contact.Street1 = csv.GetField<string>("Other Street");
				contact.Street2 = csv.GetField<string>("Other Street 2");
				contact.City = csv.GetField<string>("Other City");
				contact.State = csv.GetField<string>("Other State");
				contact.Zip =
					csv.GetField<string>("Other Postal Code");

				contact = Verifier.VerifyAddress(contact);

				list.Add(contact);
				i++;
			}

			foreach (var contact in list)
			{
				Console.WriteLine(contact.ToString());
			}

			Console.ReadLine();
		}
	}

	[Serializable]
	[XmlRootAttribute("AddressValidateResponse")]
	public class AddressValidateResponse
	{
		[XmlElement("Address")]
		public Address Address { get; set; }
	}

	[Serializable]
	public class Address
	{
		private string _address1;
		private string _address2;
		private string _city;

		[XmlElement("Address2")]
		public string Address1
		{
			get => string.IsNullOrWhiteSpace(_address1)
				? _address1
				: CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_address1);
			set => _address1 = value;
		}

		[XmlElement("Address1")]
		public string Address2
		{
			get => string.IsNullOrWhiteSpace(_address2)
				? _address2
				: CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_address2);
			set => _address2 = value;
		}

		[XmlElement("City")]
		public string City
		{
			get => string.IsNullOrWhiteSpace(_city)
				? _city
				: CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_city);
			set => _city = value;
		}

		[XmlElement("State")]
		public string State { get; set; }

		[XmlElement("Zip5")]
		public string Zip5 { get; set; }
		
		[XmlElement("Zip4")]
		public string Zip4 { get; set; }

		public bool IsComplete() => !string.IsNullOrWhiteSpace(Address1)
		                            && !string.IsNullOrWhiteSpace(City)
		                            && !string.IsNullOrWhiteSpace(State)
		                            && !string.IsNullOrWhiteSpace(Zip5)
		                            && !string.IsNullOrWhiteSpace(Zip4);

		public override string ToString()
		{
			return $"{Address1} {Address2}{Environment.NewLine}"
			       + $"{City}, {State}  "
			       + $"{Zip5}{(string.IsNullOrWhiteSpace(Zip5) ? "" : "-" + Zip4)}";
		}
	}

	public class Verifier
	{
		private const string template =
			"<AddressValidateRequest USERID=\"939HOME01138\"><Address><Address1>{0}</Address1><Address2>{1}</Address2><City>{2}</City><State>{3}</State><Zip5>{4}</Zip5><Zip4>{5}</Zip4></Address></AddressValidateRequest>";

		public static Contact VerifyAddress(Contact contact)
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri("http://production.shippingapis.com/")
			};

			var xmlParam = GetRequestXml(contact);

			var message = new HttpRequestMessage(
				HttpMethod.Get,
				$"ShippingAPI.dll?API=Verify&XML={xmlParam}");

			var result = client.SendAsync(message).Result;

			var addressXml = result.Content.ReadAsStringAsync().Result;

			var serializer = new XmlSerializer(typeof(AddressValidateResponse));

			var sr = new StringReader(addressXml);

			var addr =
				((AddressValidateResponse) serializer.Deserialize(sr))?.Address;

			if (addr == null || !addr.IsComplete())
				return contact;

			contact.Street1 = addr.Address1;
			contact.Street2 = addr.Address2;
			contact.City = addr.City;
			contact.State = addr.State;
			contact.Zip = $"{addr.Zip5} {addr.Zip4}";
			contact.Address = addr.ToString();

			return contact;
		}

		private static string GetRequestXml(Contact contact) => string.Format(
			template,
			contact.Street1,
			contact.Street2,
			contact.City,
			contact.State,
			contact.Zip.Split("-").FirstOrDefault()
			?? string.Empty,
			contact.Zip.Split("-").LastOrDefault()
			?? string.Empty);
	}

	public class Contact
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Address { get; set; }

		public string Street1 { get; set; }

		public string Street2 { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string Zip { get; set; }

		public override string ToString()
		{
			return $"{FirstName} {LastName}: {Address}";
		}
	}
}
