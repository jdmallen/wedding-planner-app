import React from "react";
import styles from "./Entry.scss";
import heartImgSrc from "../../Images/crossstitch_heart.png";

const Entry = () => (
	<div
		className={styles.heartImageDiv}
		style={{ background: `center / contain no-repeat url(${heartImgSrc})` }}
	>
		<div className={styles.kAndJOverlay}>K&nbsp;&amp;&nbsp;J</div>
	</div>
);

export default Entry;
