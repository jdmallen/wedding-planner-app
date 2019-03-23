import React from "react";
import {
	Button,
	ButtonGroup,
	Card,
	CardBody,
	CardFooter,
	CardHeader,
	CardImg,
	CardSubtitle,
	CardText,
	CardTitle,
} from "reactstrap";
import styles from "./HotelCard.scss";

const HotelCard = ({
	logo, name, address, price, phone, websiteUrl, directionsUrl, rooms,
}) => (
	<Card className={styles.hotelCard}>
		<CardHeader>{name}</CardHeader>
		<CardImg top src={logo} alt={`${name} logo`} />
		<CardBody>
			<CardText>
				<h6>${price}/night (plus taxes & fees)</h6>
				<p className="small">{rooms}</p>
				<p>{address}<br />{phone}</p>
			</CardText>
		</CardBody>
		<CardFooter>
			<ButtonGroup className={styles.hotelButtons}>
				<Button color="success" href={`"tel:${phone}`}>Call</Button>
				<Button color="secondary" href={directionsUrl}>Directions</Button>
				<Button color="secondary" href={websiteUrl}>Website</Button>
			</ButtonGroup>
		</CardFooter>
	</Card>
);

export default HotelCard;
