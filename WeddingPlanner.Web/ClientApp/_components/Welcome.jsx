import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
	Button,
	Col,
	Row,
} from "reactstrap";
import { Link } from "react-router-dom";
import styles from "./Welcome.scss";

const Welcome = () => (
	<div className={styles.entryOuterDiv}>
		<div className={styles.kAndJOverlay}>
			{"Kristen & Jesse"}
		</div>
		<div className={styles.kAndJShortOverlay}>
			{"K\u0026J"}
		</div>
		<div className={styles.weddingInfo}>
			<div className={styles.headingWithButton}>
				<h2 className="d-block d-sm-none">July 27, 2019</h2>
				<h1 className="d-none d-sm-block">July 27, 2019</h1>
				<Button
					outline
					color="info"
					// eslint-disable-next-line max-len
					href="https://calendar.google.com/calendar/r/eventedit?text=Kristen%20%26%20Jesse's%20Wedding&dates=20190727T213000Z/20190728T030000Z&location=Ventosa%20Vineyards%2C%203440%20NY-96A%2C%20Geneva%2C%20NY%2014456"
				>
					<FontAwesomeIcon icon="calendar-alt" />
				</Button>
			</div>
			<div className={styles.headingWithButton}>
				<h4 className="d-block d-sm-none">Ventosa Vineyards</h4>
				<h2 className="d-none d-sm-block">Ventosa Vineyards</h2>
				<Button
					outline
					color="info"
					// eslint-disable-next-line max-len
					href="https://www.google.com/maps/dir//Ventosa+Vineyards,+3440+NY-96A,+Geneva,+NY+14456/"
				>
					<FontAwesomeIcon icon="map-marker-alt" />
				</Button>
			</div>
			<h6>in Geneva, NY</h6>
			<h3>5:30 p.m.</h3>
			<h3>Adults Only Event</h3>
		</div>
		<div className={styles.welcomeNavigation}>
			<Row className={styles.customRow}>
				<Col xs="12">
					<Link to="/rsvp" className="btn btn-primary btn-lg btn-block">
						{"RSVP"}
					</Link>
				</Col>
			</Row>
			<Row className={styles.customRow}>
				<Col xs="6">
					<Link to="/hotels" className="btn btn-secondary btn-lg btn-block">
						<span className="d-none d-sm-block">Hotel Information</span>
						<span className="d-block d-sm-none">Hotels</span>
					</Link>
				</Col>
				<Col xs="6">
					<Link to="/registries" className="btn btn-secondary btn-lg btn-block">
						<span className="d-none d-sm-block">Gift Registries</span>
						<span className="d-block d-sm-none">Registries</span>
					</Link>
				</Col>
			</Row>
			<Row className={styles.customRow}>
				<Col xs="6">
					<Link to="/itinerary" className="btn btn-secondary btn-lg btn-block">
						{"Itinerary"}
					</Link>
				</Col>
				<Col xs="6">
					<Link to="/us" className="btn btn-secondary btn-lg btn-block">
						{"Us"}
					</Link>
				</Col>
			</Row>
		</div>
	</div>
);

export default Welcome;
