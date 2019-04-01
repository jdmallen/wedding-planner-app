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
import classnames from "classnames";
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
}) => (
	<Card className={styles.hotelCard}>
		<CardHeader>{name}</CardHeader>
		<CardImg top src={logo} alt={`${name} logo`} />
		<CardBody>
			<h6>{`${price}/night (plus taxes & fees)`}</h6>
			<p className="small">{rooms}</p>
			<p>{address}<br />{phone}</p>
		</CardBody>
		<CardFooter>
			<ButtonGroup
				size="sm"
				className={classnames(styles.hotelButtons, "d-sm-none d-md-block")}
			>
				<Button color="success" href={`tel:${phone}`}>Call</Button>
				<Button color="secondary" href={directionsUrl}>Directions</Button>
				<Button color="secondary" href={websiteUrl}>Site</Button>
			</ButtonGroup>
			<ButtonGroup
				size="sm"
				vertical
				className={
					classnames(styles.hotelButtons, "d-none d-sm-block d-md-none")
				}
			>
				<Button color="success" href={`tel:${phone}`}>Call</Button>
				<Button color="secondary" href={directionsUrl}>Directions</Button>
				<Button color="secondary" href={websiteUrl}>Site</Button>
			</ButtonGroup>
		</CardFooter>
	</Card>
);

export default HotelCard;
