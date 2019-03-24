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
			// eslint-disable-next-line max-len
			googleMapsIframeUrl="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2531.4040533668394!2d-77.00881238267057!3d42.85881641570059!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89d0c4d574da6b15%3A0xd41e6568134843d0!2sHoliday+Inn+Express+%26+Suites+Geneva+Finger+Lakes!5e0!3m2!1sen!2sus!4v1553374479952"
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
			// eslint-disable-next-line max-len
			googleMapsIframeUrl="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2924.1121716864163!2d-76.9818082838176!3d42.870476510705714!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89d0c513bac65e45%3A0xc00d9b379484aaba!2sHampton+Inn+Geneva!5e0!3m2!1sen!2sus!4v1553374280603"
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
			// eslint-disable-next-line max-len
			googleMapsIframeUrl="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d6020.436742226887!2d-76.99837986885295!3d42.86179745003181!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x89d0c527df1b1c35%3A0xa1b476acfd186369!2sFairfield+Inn+%26+Suites+by+Marriott+Geneva+Finger+Lakes!5e0!3m2!1sen!2sus!4v1553374426376"
		/>);
	return (
		<div>
			<h1>Hotels</h1>
			<p>
				We have secured blocks of rooms at the below hotels.<br />
				Mention &quot;Merritt-Mallen Wedding&quot; to get the rate!
			</p>
			<p className="small">
				<em>
					If any hotel reports that they&apos;re out of rooms,
					please <a href="mailto:us@kristenandjesse.com">let us know</a>!
				</em>
			</p>
			<CardDeck>
				{holidayInn}
				{hamptonInn}
				{fairfieldInn}
			</CardDeck>
		</div>
	);
};

export default Hotels;
