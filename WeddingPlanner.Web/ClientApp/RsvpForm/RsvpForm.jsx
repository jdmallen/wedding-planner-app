import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import {
	Button,
	Col,
	Container,
	Form,
	FormGroup,
	Input,
	Label,
	Row,
} from "reactstrap";
import ReactIframeResizer from "react-iframe-resizer-super";
import { searchInvitationsByCode } from "../_ducks/invitation.lookup";
import { openModal, closeModal } from "../_ducks/ui";
import styles from "./RsvpForm.scss";

class RsvpForm extends Component
{
	constructor(props)
	{
		super(props);
		this.handleChange = this.handleChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	componentDidMount()
	{
		setTimeout(() =>
		{
			document.getElementsByClassName("fa-circle-notch")[0]
				.style.display = "none";
		}, 2000);
	}

	componentWillUnmount()
	{
	}

	handleChange(e)
	{
		const { name, value } = e.target;
		this.setState({ [name]: value });
	}

	handleSubmit(e)
	{
		e.preventDefault();

		const { invitationCode } = this.state;
		if (invitationCode)
		{
			this.props.codeLookup(invitationCode);
		}
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
						{"Having trouble with the form above? Please "}
						<a href="mailto:us@kristenandjesse.com">let us know</a>{"!"}
					</em>
				</p>
			</Container>
		);
	}
}

RsvpForm.propTypes = {
	isModalOpen: PropTypes.bool,
};

RsvpForm.defaultProps = {
	isModalOpen: false,
};

const mapStateToProps = state => ({
	isModalOpen: state.ui.isModalOpen,
});

const mapDispatchToProps = dispatch => ({
	openModal: () => dispatch(openModal()),
	closeModal: () => dispatch(closeModal()),
	codeLookup: code => dispatch(searchInvitationsByCode(code)),
});

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(RsvpForm));
