import React from "react";
import classnames from "classnames";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import styles from "./BouncingArrow.scss";

const BouncingArrow = () => (
	<div className={classnames(styles.arrow, styles.bounce)}>
		<FontAwesomeIcon icon="arrow-down" />
	</div>
);

export default BouncingArrow;
