import React from "react";
import { CardDeck } from "reactstrap";
import HotelCard from "./HotelCard";
import hamptonLogo from "../../Images/hampton_logo.png";
import holidayLogo from "../../Images/holiday_inn_express_logo.png";
import fairfieldLogo from "../../Images/fairfield_inn_and_suites_logo.png";
import styles from "./Hotels.scss";

const Hotels = function Hotels()
{
	const holidayInn =
		(<HotelCard
			logo={holidayLogo}
			name="Holiday Inn Express"
			price="159.00"
			address="530 Hamilton St, Geneva, NY 14456"
			// eslint-disable-next-line max-len
			websiteUrl="https://www.ihg.com/holidayinnexpress/hotels/us/en/geneva/rocgf/hoteldetail"
			// eslint-disable-next-line max-len
			directionsUrl="https://www.google.com/maps/dir//Holiday+Inn+Express+%26+Suites+Geneva+Finger+Lakes,+530+Hamilton+St,+Geneva,+NY+14456/"
			phone="315-787-0530"
			rooms="15 double queen rooms"
		/>);
	const hamptonInn =
		(<HotelCard
			logo={hamptonLogo}
			name="Hampton Inn"
			price="188.00"
			address="43 Lake St, Geneva, NY 14456"
			// eslint-disable-next-line max-len
			websiteUrl="https://hamptoninn3.hilton.com/en/hotels/new-york/hampton-inn-geneva-GVANYHX/index.html"
			// eslint-disable-next-line max-len
			directionsUrl="https://www.google.com/maps/dir//Hampton+Inn+Geneva,+Lake+Street,+Geneva,+NY/"
			phone="315-781-2035"
			rooms="10 double queen rooms"
		/>);
	const fairfieldInn =
		(<HotelCard
			logo={fairfieldLogo}
			name="Fairfield Inn & Suites"
			price="208.00"
			address="383 Hamilton St, Geneva, NY 14456"
			// eslint-disable-next-line max-len
			websiteUrl="https://www.marriott.com/hotels/travel/ithfl-fairfield-inn-and-suites-geneva-finger-lakes/"
			// eslint-disable-next-line max-len
			directionsUrl="https://www.google.com/maps/dir//Fairfield+Inn+%26+Suites+by+Marriott+Geneva+Finger+Lakes,+Hamilton+Street,+Geneva,+NY/"
			phone="315 789-2900"
			rooms="10 king and 10 double queen rooms (20 total)"
		/>);
	return (
		<div>
			<h1>Hotels</h1>
			<p>
				We have secured blocks of rooms at the below hotels.<br />
				Mention &quot;Merritt-Mallen Wedding&quot; to get the rate!
			</p>
			<CardDeck>
				{holidayInn}
				{hamptonInn}
				{fairfieldInn}
			</CardDeck>
			<p className={styles.troubleText}>
				<em>
					If any hotel reports that they&apos;re out of rooms,
					please <a href="mailto:us@kristenandjesse.com">let us know</a>!
				</em>
			</p>
		</div>
	);
};

export default Hotels;
