import React, { lazy, Suspense } from "react";
import {
	Button,
	ButtonGroup,
	Card,
	CardBody,
	CardFooter,
	CardHeader,
	CardImg,
} from "reactstrap";
import InViewMonitor from "react-inview-monitor";
import classnames from "classnames";
import firstPhoto from "../../Images/album/20160508-first_photo.jpg";
import ImageWrapper from "./ImageWrapper";
import styles from "./Us.scss";

function importAll(req)
{
	const images = {};
	req.keys().map((item) =>
	{
		images[item.replace("./", "")] = req(item);
		return false;
	});
	return images;
}

const images =
	importAll(
		require.context("../../Images/album/", false, /\.(png|jpe?g|svg)$/)
	);

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
		{Object.keys(images).map((image, i) => (
			<InViewMonitor key={image} childPropsInView={{ render: true }}>
				<ImageWrapper image={images[image]} nr={i} />
			</InViewMonitor>
		))}
	</div>
);

export default Us;
