import { DateTime } from "luxon";

const time = DateTime.TIME_SIMPLE;
const timezone = "America/New_York";
const ceremony = DateTime.local(2019, 7, 27, 17, 30).setZone(timezone);
const itineraryItems =
[
	{
		index: 1,
		time: ceremony.minus({ hours: 2 }).toLocaleString(time),
		event: "Shuttle begins transportion between Geneva hotels and Ventosa. "
			+ "(Shuttle seats 27 people.)",
	},
	{
		index: 2,
		time: ceremony.toLocaleString(time),
		event: "Ceremony begins. Shuttle temporarily stops.*",
	},
	{
		index: 3,
		time: ceremony.plus({ minutes: 30 }).toLocaleString(time),
		event: "Ceremony concludes.* Reception begins with cocktail hour.* "
			+ "Open wine & beer bar begins.",
	},
	{
		index: 4,
		time: ceremony.plus({ hours: 1, minutes: 30 }).toLocaleString(time),
		event: "Wedding party introductions, first dance, and honor attendant "
			+ "speeches.*",
	},
	{
		index: 5,
		time: ceremony.plus({ hours: 2 }).toLocaleString(time),
		event: "Dinner is served.*",
	},
	{
		index: 6,
		time: ceremony.plus({ hours: 2, minutes: 30 }).toLocaleString(time),
		event: "Shuttle resumes.",
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
		event: "Reception concludes. Cash bar ends.",
	},
	{
		index: 10,
		time: ceremony.plus({ hours: 6, minutes: 30 }).toLocaleString(time),
		event: "Shuttle ends.*",
	},
];

export default itineraryItems;
