const albumfilenames = [
	{
		filename: "20160526-just_started_dating.jpg",
		caption: "The first times Kristen came over to Jesse's place were in April of 2016 under the guise of \"kitty therapy\" with Baby, Jesse's cat. Within a month, on May 23rd, they officially starting dating, though they were tyring to keep it a secret from their coworkers at Moog.",
	},
	{
		filename: "20160527-mist.jpg",
		caption: "They went on Maid of the Mist a couple days later.",
	},
	{
		filename: "20160528-minigolf.jpg",
		caption: "Mini-golf the day after that. This particular shot was taken just after Kristen knocked Jesse's ball far away from the hole (hers is the red; his is long gone).",
	},
	{
		filename: "20160610-bacchus.jpg",
		caption: "Bacchus Wine Bar became their restaurant of choice. This was taken a couple weeks after they started dating, and would later be where they celebrated each anniversary and their engagement.",
	},
	{
		filename: "20160617-canalside.jpg",
		caption: "They decided to try to have a day of photoshooting at Canalside and Delaware Park.",
	},
	{
		filename: "20160618-first_photo_shoot.jpg",
		caption: "They weren't the best at it, and the sun certainly wasn't cooperating with them, but they loved it anyway.",
	},
	{
		filename: "20160709-day_drunk.jpg",
		caption: "They discovered a weakness for frozen wine slushies from Merritt Wineries (no relation). They have like 6 of those sippy cups now.",
	},
	{
		filename: "20160719-birthday_scavenger_hunt.jpg",
		caption: "For Kristen's birthday that year, Jesse arranged an elaborate scavenger hunt with limerick clues that led her all over the building where they worked, and later all over the apartment. She eventually found a basket with all her favorite things.",
	},
	{
		filename: "20160826-long_distance.jpg",
		caption: "They spent a good portion of the start of their relationship long distance as Kristen finished her degree. Jesse managed to put close to 25K miles on his car in a single year driving back and forth to Rochester every weekend to see her. It sucked, but they made it through!",
	},
	{
		filename: "20160917-swimming.jpg",
		caption: "Here we see Jesse attempting to drown Kristen in Oneida Lake.",
	},
	{
		filename: "20160924-joshs_wedding.jpg",
		caption: "Looking dapper at the Peterson wedding.",
	},
	{
		filename: "20161002-apple_picking.jpg",
		caption: "That fall they went apple picking, and of course it started to rain. They clearly made the best of it.",
	},
	{
		filename: "20161015-rit_family_weekend.jpg",
		caption: "Two weeks later, in October of 2016, Jesse finally got to meet Kristen's sisters at an event at RIT.",
	},
	{
		filename: "20161029-eleven_and_eggo.jpg",
		caption: "Eleven and her precious Eggo at a Halloween party.",
	},
	{
		filename: "20161105-trail1.jpg",
		caption: "One of only two times they went hiking. This was taken along the Niagara Whirlpool Trail.",
	},
	{
		filename: "20161105-trail2.jpg",
		caption: "They got some pretty excellent pictures that hike.",
	},
	{
		filename: "20161203-first_xmas_tree.jpg",
		caption: "She joined Jesse on his annual hunt for the perfect Christmas tree. They upped the ante the following year and began chopping them down themselves.",
	},
	{
		filename: "20161231-first_new_years.jpg",
		caption: "They celebrated their first New Years together down in NJ, ringing in 2017 with ridiculous hats and lots of champagne.",
	},
	{
		filename: "20170213-om_nom_nom.jpg",
		caption: "They have some weird shots that they love. This is one of them.",
	},
	{
		filename: "20170318-red_panda_encounter.jpg",
		caption: "One of Jesse's Christmas gifts to Kristen was a reservation to have an encounter with her favorite animal, the red panda, capping off a weeklong spring break roadship around the Northeast.",
	},
	{
		filename: "20170325-baby.jpg",
		caption: "Nearly a year into the relationship, the two of them and Baby were quickly becoming a family.",
	},
	{
		filename: "20170530-earrings.jpg",
		caption: "Back at Bacchus, Jesse surprised Kristen with diamond earrings for their 1st anniversary.",
	},
	{
		filename: "20170530-first_anniversary.jpg",
		caption: "If it wasn't clear at this point, they really love wine.",
	},
	{
		filename: "20170623-ef.jpg",
		caption: "During the summer of 2017, they began traveling to many outdoor festivals and concerts.",
	},
	{
		filename: "20171008-belle_baby.jpg",
		caption: "Jesse had also adopted a new kitten. Here's Belle at 11 weeks old, cuddling up with an oddly tolerant Baby.",
	},
	{
		filename: "20171028-halloween.jpg",
		caption: "That Halloween they went as Boo and Kitty.",
	},
	{
		filename: "20180113-jesse_proposing.jpg",
		caption: "Finally, on January 13th, 2018, Jesse proposed. In cahoots with Kristen's sisters, Jesse rented out Kristen's favorite ice cream place (yes, in January), sneaked over their parents Jeannette and David, and aimed to surprised Kristen with a carefully plotted ruse.",
	},
	{
		filename: "20180113-kristens_reaction.jpg",
		caption: "She was definitely surprised.",
	},
	{
		filename: "20180113-forehead_kiss.jpg",
		caption: "She said she liked him as a friend and would have to think about it. Just kidding; it was a resounding yes! :)",
	},
	{
		filename: "20180511-graduation.jpg",
		caption: "That May came her graduation ceremony, and began Jesse's bowtie phase.",
	},
	{
		filename: "20180513-idk.jpg",
		caption: "Yup, another weird one.",
	},
	{
		filename: "20180616-harry_potter.jpg",
		caption: "One thing they figured out was to never stop dating. Here they are at the Harry Potter play on Broadway.",
	},
	{
		filename: "20181223-kristen_and_hobbes.jpg",
		caption: "A few months after Baby sadly passed away, they decided they were ready for a new member of the family. Enter Hobbes, Kristen's crazy, cuddly, orange kitty.",
	},
	{
		filename: "20190101-jesse_and_hobbes.jpg",
		caption: "Safe to say Jesse and Hobbes hit it off pretty well. Here they are on New Years Day 2019, driving back home from another holiday with the families.",
	},
	{
		filename: "20190105-hobbes_and_belle.jpg",
		caption: "Here's the picture included on the backs of the paper invitations. The two cats get along FAR better than Belle and Baby ever did.",
	},
	{
		filename: "20190215-kristen_and_hobbes.jpg",
		caption: "The two of them are inseparable.",
	},
	{
		filename: "20190311-jesse_and_hobbes.jpg",
		caption: "Hobbes has done a good job of keeping them sane, as the last few months have chiefly comprised of frenetic wedding planning and turbulent job changes.",
	},
	{
		filename: "20190323-hobbes_and_belle.jpg",
		caption: "While they sadly can't accompany the bride and groom to the wedding, here's the two of them saying they hope to see you there!",
	},
];

export default albumfilenames;
