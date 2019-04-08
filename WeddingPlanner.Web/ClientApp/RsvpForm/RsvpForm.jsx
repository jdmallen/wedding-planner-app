import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Container } from "reactstrap";
import ReactIframeResizer from "react-iframe-resizer-super";
import styles from "./RsvpForm.scss";

class RsvpForm extends Component
{
	componentDidMount()
	{
		setTimeout(() =>
		{
			document.getElementsByClassName("fa-circle-notch")[0]
				.style.display = "none";
		}, 2000);
	}

	render()
	{
		const iframeResizerOptions =
		{
			autoResize: true,
			checkOrigin: false,
			heightCalculationMethod: "max",
			enablePublicMethods: true,
			initCallback: () =>
			{
				document.getElementsByClassName("fa-circle-notch")[0]
					.style.display = "none";
			},
		};

		return (
			<Container className={styles.iframeContainer}>
				<h2>RSVP</h2>
				<div className={styles.loadingSpinner}>
					<FontAwesomeIcon
						icon="circle-notch"
						spin
					/>
				</div>
				<ReactIframeResizer
					iframeResizerOptions={iframeResizerOptions}
					src="https://kristenandjesse.app.rsvpify.com/?embed=1&js=1"
				/>
				<p className={styles.troubleText}>
					<em>
						{"Having trouble with the form above? "}
						{"Seeing just a picture of a girl's head? "}
						{"Try refreshing the site. If that fails, please "}
						<a href="mailto:us@kristenandjesse.com">let us know</a>{"!"}
					</em>
				</p>
			</Container>
		);
	}
}

export default withRouter(RsvpForm);
