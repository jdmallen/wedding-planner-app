import React from "react";
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import {
	Button,
	Card,
	CardBody,
	CardFooter,
	CardHeader,
	CardImg,
	CardSubtitle,
	CardText,
	CardTitle,
	Col,
	Row,
} from "reactstrap";
import { Link } from "react-router-dom";
import classnames from "classnames";
import SVG from "react-inlinesvg";
import HotelCard from "./HotelCard";
import hamptonLogo from "../../Images/hampton_logo.png";
import holidayLogo from "../../Images/holiday_inn_express_logo.png";
import fairfieldLogo from "../../Images/fairfield_inn_and_suites_logo.png";
import styles from "./Hotels.scss";

const Hotels = () => (
	<div>
		<h1 className={styles.center}>Hotels</h1>
		<p>
			We have secured blocks of rooms at the below hotels.
			Mention &quot;Merritt-Mallen Wedding&quot; to get the rate!
		</p>
		<p className="small">
			<em>
				If they report that they&apos;re out of rooms,
				please <a href="mailto:us@kristenandjesse.com">let us know</a>!
			</em>
		</p>
		<div className={styles.hotelsList}>
			<Row className={styles.customRow}>
				<Col sm="6" lg="4">
					<HotelCard
						logo={holidayLogo}
						name="Holiday Inn Express"
						price="159.00"
						address="530 Hamilton St, Geneva, NY 14456"
						websiteUrl="https://www.ihg.com/holidayinnexpress/hotels/us/en/geneva/rocgf/hoteldetail"
						directionsUrl="https://www.google.com/maps/dir//Holiday+Inn+Express+%26+Suites+Geneva+Finger+Lakes,+530+Hamilton+St,+Geneva,+NY+14456/"
						phone="315-787-0530"
						rooms="15 double queen rooms"
					/>
				</Col>
				<Col sm="6" lg="4">
					<HotelCard
						logo={hamptonLogo}
						name="Hampton Inn"
						price="188.00"
						address="43 Lake St, Geneva, NY 14456"
						websiteUrl="https://hamptoninn3.hilton.com/en/hotels/new-york/hampton-inn-geneva-GVANYHX/index.html"
						directionsUrl="https://www.google.com/maps/dir//Hampton+Inn+Geneva,+Lake+Street,+Geneva,+NY/"
						phone="315-781-2035"
						rooms="10 double queen rooms"
					/>
				</Col>
				<Col sm="6" lg="4">
					<HotelCard
						logo={fairfieldLogo}
						name="Fairfield Inn & Suites"
						price="208.00"
						address="383 Hamilton St, Geneva, NY 14456"
						websiteUrl="https://www.marriott.com/hotels/travel/ithfl-fairfield-inn-and-suites-geneva-finger-lakes/"
						directionsUrl="https://www.google.com/maps/dir//Fairfield+Inn+%26+Suites+by+Marriott+Geneva+Finger+Lakes,+Hamilton+Street,+Geneva,+NY/"
						phone="315 789-2900"
						rooms="10 king and 10 double queen rooms, 20 total"
					/>
				</Col>
			</Row>
		</div>
	</div>
);

export default Hotels;
