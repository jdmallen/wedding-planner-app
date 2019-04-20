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
import styles from "./RegistryCard.scss";

const RegistryCard = ({
	logo,
	name,
	registryUrl,
	websiteUrl,
}) => (
	<Card className={styles.storeCard}>
		<CardHeader>{name}</CardHeader>
		<CardBody className={styles.cardBody}>
			<a className={styles.logoImg} href={websiteUrl}>
				<CardImg top src={logo} alt={`${name} logo`} />
			</a>
		</CardBody>
		<CardFooter>
			<ButtonGroup
				size="sm"
				className={
					classnames(styles.storeButtons, "d-sm-none d-md-inline-flex")
				}
			>
				<Button color="success" href={registryUrl}>Registry</Button>
				<Button color="secondary" href={websiteUrl}>Main Store Site</Button>
			</ButtonGroup>
			<ButtonGroup
				size="sm"
				vertical
				className={
					classnames(styles.storeButtons, "d-none d-sm-inline-flex d-md-none")
				}
			>
				<Button color="success" href={registryUrl}>Registry</Button>
				<Button color="secondary" href={websiteUrl}>Main Store Site</Button>
			</ButtonGroup>
		</CardFooter>
	</Card>
);

export default RegistryCard;
