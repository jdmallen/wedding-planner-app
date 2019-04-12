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
import LazyLoad from "react-lazy-load";
import firstPhoto from "../../Images/album/20160508-first_photo.jpg";
import ImageLoader from "../ImageLoader/ImageLoader";
import styles from "./Us.scss";

const Us = () => (
	<div className={styles.outer}>
		<h1 className={styles.pageHeading}>Us</h1>
		<div className={classnames("text-center", styles.grtH2)}>
			{"IT'S HAPPENING!!"}
		</div>
		<div className={styles.paragraph}>
			{"We are absolutely thrilled to invite all of our favorite people in "}
			{"the world to our special day, and we hope every one of you is able to "}
			{"share in the festivities!"}
		</div>
		<div className={styles.paragraph}>
			{"In delightful anticipation of our big day, here's a brief glimpse of "}
			{"how Kristen and Jesse came to be, complete with embarrassing "}
			{"photographs that Jesse may have forgotten to clear with Kristen "}
			{"before sharing them freely across the internet."}
		</div>
		<div className={styles.paragraph}>Enjoy!</div>
		<div style={{ height: "600px" }} />
		<LazyLoad
			debounce={false}
			offsetVertical={50}
		>
			<div className={styles.photo}>
				<ImageLoader alt="first pic I took" src={firstPhoto} />
			</div>
		</LazyLoad>
	</div>
);

export default Us;
