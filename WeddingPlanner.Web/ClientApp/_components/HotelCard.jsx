import React from "react";
import {
	Button,
	ButtonGroup,
	Card,
	CardBody,
	CardFooter,
	CardHeader,
	CardImg,
} from "reactstrap";
import styles from "./HotelCard.scss";

const HotelCard = ({
	logo,
	name,
	address,
	price,
	phone,
	websiteUrl,
	directionsUrl,
	rooms,
	googleMapsIframeUrl,
}) => (
	<Card className={styles.hotelCard}>
		<CardHeader>{name}</CardHeader>
		<CardImg top src={logo} alt={`${name} logo`} />
		<CardBody>
			<h6>{`${price}/night (plus taxes & fees)`}</h6>
			<p className="small">{rooms}</p>
			<p>{`${address}<br />${phone}`}</p>
		</CardBody>
		<CardFooter>
			<ButtonGroup size="sm" className={styles.hotelButtons}>
				<Button color="success" href={`tel:${phone}`}>Call</Button>
				<Button color="secondary" href={directionsUrl}>Directions</Button>
				<Button color="secondary" href={websiteUrl}>Site</Button>
			</ButtonGroup>
		</CardFooter>
		<iframe
			className="d-none d-xl-block"
			title="map"
			src={googleMapsIframeUrl}
			width="100%"
			height="300"
			frameBorder="0"
			style={{ border: 0 }}
			allowFullScreen
		/>
	</Card>
);

export default HotelCard;
