import React from "react";
import styles from "./BrowserWarning.scss";

const BrowserWarning = ({ supported }) => (
	<div
		className={styles.mainDiv}
		style={{ display: (supported ? "none" : "block") }}
	>
		<p>
			{"You are using an "}
			<strong>unsupported</strong>
			{" browser."}
		</p>
		<p>
			{"The site may still work OK, but you may run into problems."}
		</p>
		<p>
			{"Please consider using the latest version of "}
			<br />
			<a
				href="https://www.mozilla.org/en-US/firefox/new/"
				rel="noopener noreferrer"
				target="_blank"
			>
				{"Firefox"}
			</a>
			{", "}
			<a
				href="https://www.google.com/chrome/"
				rel="noopener noreferrer"
				target="_blank"
			>
				{"Chrome"}
			</a>
			{", "}
			<a
				href="https://www.microsoft.com/en-us/windows/microsoft-edge"
				rel="noopener noreferrer"
				target="_blank"
			>
				{"Edge"}
			</a>
			{", "}
			<a
				href="https://www.opera.com/"
				rel="noopener noreferrer"
				target="_blank"
			>
				{"Opera"}
			</a>
			{", or "}
			<a
				href="https://www.chromium.org/"
				rel="noopener noreferrer"
				target="_blank"
			>
				{"Chromium"}
			</a>
			{" instead."}
		</p>
	</div>
);

export default BrowserWarning;
