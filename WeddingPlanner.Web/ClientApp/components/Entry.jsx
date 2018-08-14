import React from "react";
import styles from "./Entry.scss";
import heartImgSrc from "../../Images/crossstitch-heart.png";

const Entry = props => (
	<div className={styles.entry}>
		<div>
			<img src={heartImgSrc} className={styles.heartImage} alt="Cross-stitched red heart." />
		</div>
		<div className={styles.kAndJOverlay}>K&nbsp;&amp;&nbsp;J</div>
	</div>
);

export default Entry;
