import React from "react";
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import {
	Button,
	Collapse,
	Container,
	Navbar,
	NavbarToggler,
	NavbarBrand,
	Nav,
	NavItem,
	NavLink,
} from "reactstrap";
import styles from "./Welcome.scss";

const Welcome = () => (
	<div className={styles.entryOuterDiv}>
		<div className={styles.kAndJOverlay}>
			Kristen & Jesse
		</div>
		<div className={styles.kAndJShortOverlay}>
			K&amp;J
		</div>
		<div className={styles.weddingInfo}>
      <div className={styles.headingWithButton}>
          <h1>July 27, 2019</h1>
          <Button
              outline
              color="info"
              href="https://calendar.google.com/calendar/r/eventedit?text=Kristen%20%26%20Jesse's%20Wedding&dates=20190727T213000Z/20190728T030000Z&location=Ventosa%20Vineyards%2C%203440%20NY-96A%2C%20Geneva%2C%20NY%2014456"
          >
						<FontAwesomeIcon icon="calendar-alt" />
          </Button>
      </div>
			<div className={styles.headingWithButton}>
        <h2>Ventosa Vineyards</h2>
        <Button
            outline
            color="info"
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
      <Button color="primary" size="lg" block>RSVP</Button>
      <Button color="secondary" size="lg" block>Hotel Information</Button>
      <Button color="secondary" size="lg" block>Directions</Button>
      <Button color="secondary" size="lg" block>Itinerary</Button>
		</div>
	</div>
);

export default Welcome;
