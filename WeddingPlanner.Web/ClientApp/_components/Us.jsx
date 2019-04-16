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
import InViewMonitor from "react-inview-monitor";
import classnames from "classnames";
import ImageWrapper from "./ImageWrapper";
import BouncingArrow from "./BouncingArrow";
import images from "./_albumImages";
import styles from "./Us.scss";

function importAll(req)
{
	const imgs = {};
	req.keys().map((item) =>
	{
		imgs[item.replace("./", "")] = req(item);
		return false;
	});
	return imgs;
}

const imageFiles =
	importAll(
		require.context("../../Images/album/", false, /\.(png|jpe?g|svg)$/)
	);

const Us = () => (
	<div className={styles.outer}>
		<h1 className={styles.pageHeading}>Us</h1>
		{/* <div className={classnames("text-center", styles.grtH2)}>
			{"IT'S HAPPENING!!"}
		</div> */}
		<div className={styles.paragraph}>
			{"We are absolutely thrilled to invite all of our favorite people in "}
			{"the world to our special day, and we hope every one of you is able to "}
			{"share in the festivities!"}
		</div>
		<div className={styles.paragraph}>
			{"In delightful anticipation of our big day, here's a brief glimpse of "}
			{"how Kristen and Jesse came to be, replete with embarrassing "}
			{"photographs that Jesse may have forgotten to clear with Kristen "}
			{"before sharing them freely across the internet."}
		</div>
		<div className={styles.paragraph}>Enjoy!</div>
		<BouncingArrow />
		{images.map((image, i) =>
		// eslint-disable-next-line arrow-body-style
		{
			const { caption, filename } = image;
			return (
				<InViewMonitor
					key={filename}
					childPropsInView={{ render: true }}
					intoViewMargin="20%"
				>
					<ImageWrapper
						caption={caption}
						image={imageFiles[filename]}
						nr={i}
					/>
				</InViewMonitor>
			);
		})}
	</div>
);

export default Us;
