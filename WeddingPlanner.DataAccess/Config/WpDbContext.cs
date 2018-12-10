using System;
using System.Collections.Generic;
using JDMallen.Toolbox.EFCore.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeddingPlanner.DataAccess.Constants;
using WeddingPlanner.DataAccess.Entities;

namespace WeddingPlanner.DataAccess.Config
{
	public class WpDbContext : EFContextBase
	{
		public WpDbContext(
			DbContextOptions options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			LoadRelationships(modelBuilder);
			LoadMealChoices(modelBuilder);
			if ("development".Equals(
				Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
				StringComparison.CurrentCultureIgnoreCase))
			{
				LoadTestData(modelBuilder);
			}

			modelBuilder
				.Entity<Address>(ConfigureAddress);
			modelBuilder
				.Entity<Invitation>(ConfigureInvitation);
			modelBuilder
				.Entity<Invitee>(ConfigureInvitee);
			modelBuilder
				.Entity<MealChoice>(ConfigureMealChoice);
			modelBuilder
				.Entity<Relationship>(ConfigureRelationship);

			base.OnModelCreating(modelBuilder);
		}

		private void ConfigureAddress(
			EntityTypeBuilder<Address> builder)
		{
			// required props
			builder.Property(x => x.StreetLine1)
				.IsRequired()
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);
			builder.Property(x => x.City)
				.IsRequired()
				.HasColumnType("nvarchar(128)")
				.HasMaxLength(128);
			builder.Property(x => x.DbState)
				.IsRequired()
				.HasColumnName("State")
				.HasColumnType("varchar(2)")
				.HasMaxLength(2);
			builder.Property(x => x.Zip)
				.IsRequired()
				.HasColumnType("varchar(10)")
				.HasMaxLength(10);

			// not required props
			builder.Property(x => x.StreetLine2)
				.IsRequired(false)
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);
			builder.Property(x => x.StreetLine3)
				.IsRequired(false)
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);

			// ignored props
			builder.Ignore(x => x.State);

			// relationships
		}

		private void ConfigureInvitation(
			EntityTypeBuilder<Invitation> builder)
		{
			// required props
			builder
				.Property(x => x.AddressId)
				.IsRequired();
			builder
				.Property(x => x.EnvelopeAddressee)
				.IsRequired()
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);
			builder
				.Property(x => x.DbInvitationType)
				.IsRequired()
				.HasColumnName("InvitationType")
				.HasColumnType("varchar(7)")
				.HasMaxLength(7);
			builder
				.Property(x => x.IsInvitationEnvelopePrinted)
				.IsRequired();
			builder
				.Property(x => x.IsInvitationSent)
				.IsRequired();
			builder
				.Property(x => x.InvitationCode)
				.IsRequired()
				.HasColumnName("InvitationCode")
				.HasColumnType("varchar(5)")
				.HasMaxLength(5);

			// not required props

			// ignored props
			builder
				.Ignore(x => x.InvitationType);
			builder
				.Ignore(x => x.InviteeCount);
			builder
				.Ignore(x => x.IsMissingPlusOne);
			builder
				.Ignore(x => x.InviteeHasDietaryRestriction);

			// relationships
			builder
				.HasOne(x => x.Address)
				.WithMany(x => x.Invitations)
				.HasForeignKey(x => x.AddressId);
			builder
				.HasMany(x => x.Invitees)
				.WithOne(x => x.Invitation)
				.HasForeignKey(x => x.InvitationId);
		}

		private void ConfigureInvitee(
			EntityTypeBuilder<Invitee> builder)
		{
			// required props
			builder
				.Property(x => x.FirstName)
				.IsRequired()
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);
			builder
				.Property(x => x.LastName)
				.IsRequired()
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);
			builder
				.Property(x => x.IsPlusOne)
				.IsRequired();
			builder
				.Property(x => x.DietaryRestrictions)
				.IsRequired()
				.HasColumnType("int");
			builder
				.Property(x => x.DbFamilySide)
				.IsRequired()
				.HasColumnName("FamilySide")
				.HasColumnType("varchar(5)")
				.HasMaxLength(5)
				.HasDefaultValue(FamilySide.Both.ToString("G"));
			builder
				.Property(x => x.RelationshipId)
				.IsRequired();
			builder
				.Property(x => x.DateCreated)
				.IsRequired()
				.HasDefaultValueSql("GETUTCDATE()");
			builder
				.Property(x => x.DateModified)
				.IsRequired()
				.HasDefaultValueSql("GETUTCDATE()");

			// not required props
			builder
				.Property(x => x.JessePriority)
				.IsRequired(false);
			builder
				.Property(x => x.KristenPriority)
				.IsRequired(false);
			builder
				.Property(x => x.EmailAddress)
				.IsRequired(false)
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);
			builder
				.Property(x => x.Notes)
				.IsRequired(false)
				.HasColumnType("ntext");
			builder
				.Property(x => x.DietaryRestrictionOtherDescription)
				.IsRequired(false)
				.HasColumnType("nvarchar(256)")
				.HasMaxLength(256);
			builder
				.Property(x => x.InvitationId)
				.IsRequired(false);
			builder
				.Property(x => x.MealChoiceId)
				.IsRequired(false);

			// ignored props
			builder
				.Ignore(x => x.DietaryRestrictionOtherRequired);
			builder
				.Ignore(x => x.FamilySide);
			builder
				.Ignore(x => x.WeightedPriority);

			// relationships
			builder
				.HasOne(x => x.Relationship)
				.WithMany(r => r.Invitees)
				.HasForeignKey(x => x.RelationshipId);
			builder
				.HasOne(x => x.MealChoice)
				.WithMany(r => r.Invitees)
				.HasForeignKey(x => x.MealChoiceId);
		}

		private void ConfigureMealChoice(
			EntityTypeBuilder<MealChoice> builder)
		{
			// required props
			builder
				.Property(x => x.DisplayName)
				.IsRequired()
				.HasColumnType("nvarchar(128)")
				.HasMaxLength(128);

			// not required props
			builder
				.Property(x => x.Description)
				.IsRequired(false)
				.HasColumnType("nvarchar(2048)")
				.HasMaxLength(2048);
			builder
				.Property(x => x.AlertsPipeDelimited)
				.IsRequired(false)
				.HasColumnType("nvarchar(1024)")
				.HasMaxLength(1024);

			// ignored props
			builder
				.Ignore(x => x.Alerts);

			// relationships
		}

		private void ConfigureRelationship(
			EntityTypeBuilder<Relationship> builder)
		{
			// required props
			builder
				.Property(x => x.RelationshipName)
				.IsRequired()
				.HasColumnType("nvarchar(32)")
				.HasMaxLength(32)
				.HasDefaultValue("Other");
			builder
				.Property(x => x.PriorityWeightOutOf100)
				.IsRequired();

			// not required props

			// ignored props

			// relationships
		}

		private void LoadRelationships(ModelBuilder modelBuilder)
		{
			var defaults = new List<Relationship>
			{
				new Relationship((int) RelationshipType.Other, "Other", 75),
				new Relationship(
					(int) RelationshipType.AuntOrUncle,
					"Aunt or Uncle",
					100),
				new Relationship(
					(int) RelationshipType.CloseFriend,
					"Close Friend",
					100),
				new Relationship((int) RelationshipType.Cousin, "Cousin", 90),
				new Relationship(
					(int) RelationshipType.FamilyFriend,
					"Family Friend",
					85),
				new Relationship((int) RelationshipType.Friend, "Friend", 95),
				new Relationship(
					(int) RelationshipType.Grandparent,
					"Grandparent",
					100),
				new Relationship(
					(int) RelationshipType.InLawExtended,
					"In-Law (Extended Family)",
					90),
				new Relationship(
					(int) RelationshipType.InLawImmediate,
					"In-Law (Immediate Family)",
					100),
				new Relationship(
					(int) RelationshipType.NieceOrNephew,
					"Niece or Nephew",
					90),
				new Relationship((int) RelationshipType.Parent, "Parent", 100),
				new Relationship(
					(int) RelationshipType.Sibling,
					"Sibling",
					100),
				new Relationship(
					(int) RelationshipType.WorkAcquaintance,
					"Work Acquaintance",
					80)
			};
			modelBuilder.Entity<Relationship>().HasData(defaults);
		}

		public void LoadMealChoices(ModelBuilder modelBuilder)
		{
			var defaults = new List<MealChoice>
			{
				new MealChoice(
					(int) MealChoiceEnum.Beef,
					"Filet Mignon",
					"Center cut beef tenderloin marinated in a special "
					+ "seasoning, then grilled to perfection and accompanied "
					+ "with a red vino rosso demi glaze."),
				new MealChoice(
					(int) MealChoiceEnum.Chicken,
					"Chicken Mandorla",
					"Airline chicken breast roasted and topped with apple "
					+ "slices and toasted almonds, then finished with a port "
					+ "wine reduction cream sauce.",
					"Contains nuts"),
				new MealChoice(
					(int) MealChoiceEnum.Fish,
					"Broiled North Atlantic Cod",
					"Fresh, broiled cod topped with a citrus burre blanc."),
				new MealChoice(
					(int) MealChoiceEnum.Vegetarian,
					"Pasta Primavera",
					"Pasta tossed with mixed vegetables and topped with the "
					+ "house marinara.",
					"Available gluten-free")
			};
			modelBuilder.Entity<MealChoice>().HasData(defaults);
		}

		private void LoadTestData(ModelBuilder modelBuilder)
		{
			var nateLaurenAddress = new Address(
				"123 Anywhere St",
				"Los Angeles",
				USState.CA,
				"90210-1234",
				"Apt 456");
			modelBuilder.Entity<Address>().HasData(nateLaurenAddress);

			var nateLaurenInvitation = new Invitation(
				"Mr. Nathaniel Pepsi & Ms. Lauren Betterlawn",
				InvitationType.Couple,
				nateLaurenAddress.Id,
				"aBcDe");
			modelBuilder.Entity<Invitation>().HasData(nateLaurenInvitation);

			var nateInvitee = new Invitee(
				"Nathaniel",
				"Pepsi",
				false,
				DietaryRestrictions.Other,
				FamilySide.Groom,
				RelationshipType.CloseFriend,
				nateLaurenInvitation.Id,
				(int?) MealChoiceEnum.Beef,
				"nrp44@funkygrandpa.com",
				"Best man",
				5,
				5,
				"Can only eat plums when floating in perfume and served in a man's hat.");
			var laurenInvitee = new Invitee(
				"Lauren",
				"Betterlawn",
				false,
				DietaryRestrictions.Vegetarian | DietaryRestrictions.PeanutAllergy,
				FamilySide.Groom,
				RelationshipType.CloseFriend,
				nateLaurenInvitation.Id,
				(int?) MealChoiceEnum.Vegetarian,
				"lwetterhahn@gmail.com",
				"Groomswoman",
				5,
				5);
			modelBuilder.Entity<Invitee>().HasData(nateInvitee, laurenInvitee);
		}
	}
}
