import { DateTime } from "luxon";

const time = DateTime.TIME_SIMPLE;
const date = DateTime.DATE_HUGE; // { month: "long", day: "numeric" };
const timezone = "America/New_York";
const ceremony = DateTime.local(2019, 7, 27, 17, 30).setZone(timezone);
const itineraryItems =
[
	{
		index: 0,
		time: "",
		event: DateTime.local(2019, 7, 27)
			.setZone(timezone)
			.toLocaleString(date),
	},
	{
		index: 1,
		time: ceremony.minus({ hours: 2, minutes: 30 }).toLocaleString(time),
		event: "Check-in begins at the three hotels.",
	},
	{
		index: 2,
		time: ceremony.minus({ hours: 2 }).toLocaleString(time),
		event: "Shuttle begins transportion between Geneva hotels and Ventosa. "
			+ "(Shuttle seats 27 people.)",
	},
	{
		index: 3,
		time: ceremony.toLocaleString(time),
		event: "Ceremony begins. Shuttle temporarily stops.*",
	},
	{
		index: 4,
		time: ceremony.plus({ minutes: 30 }).toLocaleString(time),
		event: "Ceremony concludes.* Reception begins with cocktail hour.* "
			+ "Open wine & beer bar begins.",
	},
	{
		index: 5,
		time: ceremony.plus({ hours: 1, minutes: 30 }).toLocaleString(time),
		event: "Wedding party introductions, first dance, and honor attendant "
			+ "speeches.* Shuttle resumes.*",
	},
	{
		index: 6,
		time: ceremony.plus({ hours: 2 }).toLocaleString(time),
		event: "Dinner is served.*",
	},
	{
		index: 7,
		time: ceremony.plus({ hours: 2, minutes: 45 }).toLocaleString(time),
		event: "Dancing begins.*",
	},
	{
		index: 8,
		time: ceremony.plus({ hours: 4, minutes: 30 }).toLocaleString(time),
		event: "Open bar ends. Cash bar begins.",
	},
	{
		index: 9,
		time: ceremony.plus({ hours: 5, minutes: 30 }).toLocaleString(time),
		event: "Reception concludes. Cash bar ends. Shuttle ends.*",
	},
	{
		index: 10,
		time: "",
		event: DateTime.local(2019, 7, 28)
			.setZone(timezone)
			.toLocaleString(date),
	},
	{
		index: 11,
		time: DateTime.local(2019, 7, 28, 11)
			.setZone(timezone)
			.toLocaleString(time),
		event: "Check-out at Hampton Inn and Holiday Inn Express.",
	},
	{
		index: 12,
		time: DateTime.local(2019, 7, 28, 12)
			.setZone(timezone)
			.toLocaleString(time),
		event: "Check-out at Fairview Inn.",
	},
];

export default itineraryItems;
