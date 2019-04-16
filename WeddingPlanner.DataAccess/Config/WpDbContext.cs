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
			LoadPhotoDefaults(modelBuilder);
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
			modelBuilder
				.Entity<Photo>(ConfigurePhoto);

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

		private void ConfigurePhoto(EntityTypeBuilder<Photo> builder)
		{
			// required props
			builder
				.Property(x => x.FileName)
				.IsRequired()
				.HasColumnType("varchar(256)")
				.HasMaxLength(256);
			builder
				.Property(x => x.IsCaptionHtml)
				.IsRequired()
				.HasDefaultValue(false);

			// not required props
			builder
				.Property(x => x.Caption)
				.IsRequired(false)
				.HasColumnType("ntext");
			builder
				.Property(x => x.DateTaken)
				.IsRequired(false)
				.HasColumnType("date");
			builder
				.Property(x => x.Order)
				.IsRequired(false);

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
				DietaryRestrictions.Vegetarian
				| DietaryRestrictions.PeanutAllergy,
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

		private void LoadPhotoDefaults(ModelBuilder modelBuilder)
		{
			var defaults = new List<Photo>
			{
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 5, 26),
					FileName = "20160526-just_started_dating.jpg",
					Caption =
						"The first times Kristen came over to Jesse's place "
						+ "were in April of 2016 under the guise of \"kitty "
						+ "therapy\" with Baby, Jesse's cat. Within a month, "
						+ "on May 23rd, they officially starting dating, "
						+ "though they were tyring to keep it a secret from "
						+ "their coworkers at Moog."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 5, 27),
					FileName = "20160527-mist.jpg",
					Caption =
						"They went on Maid of the Mist a couple days later."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 5, 28),
					FileName = "20160528-minigolf.jpg",
					Caption =
						"Mini-golf the day after that. This particular shot "
						+ "was taken just after Kristen knocked Jesse's ball "
						+ "far away from the hole (hers is the red; his is "
						+ "long gone)."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 6, 10),
					FileName = "20160610-bacchus.jpg",
					Caption =
						"Bacchus Wine Bar became their restaurant of choice. "
						+ "This was taken a couple weeks after they started "
						+ "dating, and would later be where they celebrated "
						+ "each anniversary and their engagement."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 6, 17),
					FileName = "20160617-canalside.jpg",
					Caption =
						"They decided to try to have a day of photoshooting at "
						+ "Canalside and Delaware Park."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 5, 26),
					FileName = "20160618-first_photo_shoot.jpg",
					Caption =
						"They weren't the best at it, and the sun certainly "
						+ "wasn't cooperating with them, but they loved it "
						+ "anyway."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 7, 9),
					FileName = "20160709-day_drunk.jpg",
					Caption =
						"They discovered a weakness for frozen wine slushies "
						+ "from Merritt Wineries (no relation). They have like "
						+ "6 of those sippy cups now."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 7, 19),
					FileName = "20160719-birthday_scavenger_hunt.jpg",
					Caption =
						"For Kristen's birthday that year, Jesse arranged an "
						+ "elaborate scavenger hunt with limerick clues that "
						+ "led her all over the building where they worked, "
						+ "and later all over the apartment. She eventually "
						+ "found a basket with all her favorite things."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 8, 26),
					FileName = "20160826-long_distance.jpg",
					Caption =
						"They spent a good portion of the start of their "
						+ "relationship long distance as Kristen finished her "
						+ "degree. Jesse managed to put close to 25K miles on "
						+ "his car in a single year driving back and forth to "
						+ "Rochester every weekend to see her. It sucked, but "
						+ "they made it through!"
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 9, 17),
					FileName = "20160917-swimming.jpg",
					Caption =
						"Here we see Jesse attempting to drown Kristen in "
						+ "Oneida Lake."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 9, 24),
					FileName = "20160924-joshs_wedding.jpg",
					Caption = "Looking dapper at the Peterson wedding."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 10, 2),
					FileName = "20161002-apple_picking.jpg",
					Caption =
						"That fall they went apple picking, and of course it "
						+ "started to rain. They clearly made the best of it."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 10, 15),
					FileName = "20161015-rit_family_weekend.jpg",
					Caption =
						"Two weeks later, in October of 2016, Jesse finally "
						+ "got to meet Kristen's sisters at an event at RIT."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 10, 29),
					FileName = "20161029-eleven_and_eggo.jpg",
					Caption =
						"Eleven and her precious Eggo at a Halloween party."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 11, 5),
					FileName = "20161105-trail1.jpg",
					Caption =
						"One of only two times they went hiking. This was "
						+ "taken along the Niagara Whirlpool Trail."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 11, 5),
					FileName = "20161105-trail2.jpg",
					Caption =
						"They got some pretty excellent pictures that hike."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 12, 3),
					FileName = "20161203-first_xmas_tree.jpg",
					Caption =
						"She joined Jesse on his annual hunt for the perfect "
						+ "Christmas tree. They upped the ante the following "
						+ "year and began chopping them down themselves."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2016, 12, 31),
					FileName = "20161231-first_new_years.jpg",
					Caption =
						"They celebrated their first New Years together down "
						+ "in NJ, ringing in 2017 with ridiculous hats and "
						+ "lots of champagne."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 2, 13),
					FileName = "20170213-om_nom_nom.jpg",
					Caption =
						"They have some weird shots that they love. This is "
						+ "one of them."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 3, 18),
					FileName = "20170318-red_panda_encounter.jpg",
					Caption =
						"One of Jesse's Christmas gifts to Kristen was a "
						+ "reservation to have an encounter with her favorite "
						+ "animal, the red panda, capping off a weeklong "
						+ "spring break roadship around the Northeast."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 3, 25),
					FileName = "20170325-baby.jpg",
					Caption =
						"Nearly a year into the relationship, the two of them "
						+ "and Baby were quickly becoming a family."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 5, 30),
					FileName = "20170530-earrings.jpg",
					Caption =
						"Back at Bacchus, Jesse surprised Kristen with diamond "
						+ "earrings for their 1st anniversary."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 5, 30),
					FileName = "20170530-first_anniversary.jpg",
					Caption =
						"If it wasn't clear at this point, they really love "
						+ "wine."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 6, 23),
					FileName = "20170623-ef.jpg",
					Caption =
						"During the summer of 2017, they began traveling to "
						+ "many outdoor festivals and concerts."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 10, 8),
					FileName = "20171008-belle_baby.jpg",
					Caption =
						"Jesse had also adopted a new kitten. Here's Belle at "
						+ "11 weeks old, cuddling up with an oddly tolerant "
						+ "Baby."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2017, 10, 28),
					FileName = "20171028-halloween.jpg",
					Caption = "That Halloween they went as Boo and Kitty."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2018, 1, 13),
					FileName = "20180113-jesse_proposing.jpg",
					Caption =
						"Finally, on January 13th, 2018, Jesse proposed. In "
						+ "cahoots with Kristen's sisters, Jesse rented out "
						+ "Kristen's favorite ice cream place (yes, in "
						+ "January), sneaked over their parents Jeannette and "
						+ "David, and aimed to surprised Kristen with a "
						+ "carefully plotted ruse.",
					Order = 1
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2018, 1, 13),
					FileName = "20180113-kristens_reaction.jpg",
					Caption = "She was definitely surprised.",
					Order = 2
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2018, 1, 13),
					FileName = "20180113-forehead_kiss.jpg",
					Caption =
						"She said she liked him as a friend and would have to "
						+ "think about it. Just kidding; it was a resounding "
						+ "yes! :)",
					Order = 3
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2018, 5, 11),
					FileName = "20180511-graduation.jpg",
					Caption =
						"That May came her graduation ceremony, and began "
						+ "Jesse's bowtie phase."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2018, 5, 13),
					FileName = "20180513-idk.jpg",
					Caption = "Yup, another weird one."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2018, 6, 16),
					FileName = "20180616-harry_potter.jpg",
					Caption =
						"One thing they figured out was to never stop dating. "
						+ "Here they are at the Harry Potter play on Broadway."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2018, 12, 23),
					FileName = "20181223-kristen_and_hobbes.jpg",
					Caption =
						"A few months after Baby sadly passed away, they "
						+ "decided they were ready for a new member of the "
						+ "family. Enter Hobbes, Kristen's crazy, cuddly, "
						+ "orange kitty."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2019, 1, 1),
					FileName = "20190101-jesse_and_hobbes.jpg",
					Caption =
						"Safe to say Jesse and Hobbes hit it off pretty well. "
						+ "Here they are on New Years Day 2019, driving back "
						+ "home from another holiday with the families."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2019, 1, 5),
					FileName = "20190105-hobbes_and_belle.jpg",
					Caption =
						"Here's the picture included on the backs of the "
						+ "paper invitations. The two cats get along FAR "
						+ "better than Belle and Baby ever did."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2019, 2, 15),
					FileName = "20190215-kristen_and_hobbes.jpg",
					Caption = "The two of them are inseparable."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2019, 3, 11),
					FileName = "20190311-jesse_and_hobbes.jpg",
					Caption =
						"Hobbes has done a good job of keeping them sane, as "
						+ "the last few months have chiefly comprised of "
						+ "frenetic wedding planning and turbulent job changes."
				},
				new Photo
				{
					Id = Guid.NewGuid(),
					DateTaken = new DateTime(2019, 3, 23),
					FileName = "20190323-hobbes_and_belle.jpg",
					Caption =
						"While they sadly can't accompany the bride and groom "
						+ "to the wedding, here's the two of them saying they "
						+ "hope to see you there!"
				}
			};
			modelBuilder.Entity<Photo>().HasData(defaults);
		}
	}
}
