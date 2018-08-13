import React from "react";
import styles from "./Entry.scss";
import heartImgSrc from "../../Images/crossstitch-heart-web.jpg";

const Entry = props => (
	<div className={styles.entry}>
		<div className={styles.heartImage}>
			<img src={heartImgSrc} alt="Cross-stitched red heart." />
		</div>
		<div className={styles.kAndJOverlay}>K&nbsp;&amp;&nbsp;J</div>
	</div>
);

export default Entry;
