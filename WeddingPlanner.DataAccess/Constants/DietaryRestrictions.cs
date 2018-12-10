using System;

namespace WeddingPlanner.DataAccess.Constants
{
	[Flags]
	public enum DietaryRestrictions
	{
		None = 0,
		GlutenFree = 1,
		Vegan = 2,
		Vegetarian = 4,
		PeanutAllergy = 8,
		ShellfishAllergy = 16,
		Diabetic = 32,
		Kosher = 64,
		Halal = 128,
		Other = 256
	}
}
