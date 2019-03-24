import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
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
import { searchInvitationsByCode } from "../_ducks/invitation.lookup";
import { openModal, closeModal } from "../_ducks/ui";
import styles from "./RsvpForm.scss";

class RsvpForm extends Component
{
	constructor(props)
	{
		super(props);
		this.state = { submitted: false };
		this.handleChange = this.handleChange.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	componentDidMount()
	{
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

		this.setState({ submitted: true });
		const { invitationCode } = this.state;
		if (invitationCode)
		{
			this.props.codeLookup(invitationCode);
		}
	}

	render()
	{
		return (
			<Container
				backdrop="static"
				centered
				size="lg"
				isOpen={this.props.isModalOpen}
				toggle={this.toggleModal}
			>
				<Row>
					<Col xs="12" sm="12" md="6">
						<h1>RSVP</h1>
						<p>
							Thank you so much for RSVPing!
						</p>
						<p>
							Please enter your invitation code
							so we can look up your invitation.
						</p>
					</Col>
					<Col xs="12" sm="10" md="6" lg="4">
						<Form onSubmit={this.handleSubmit}>
							<FormGroup className={styles.closeMarginFormControl}>
								<Label htmlFor="inputCode">Code</Label>
								<Input
									id="inputCode"
									name="invitationCode"
									placeholder="abc12"
									required
									type="text"
									maxLength="5"
									onChange={e => this.handleChange(e)}
								/>
							</FormGroup>
							<Button color="primary" type="submit">
								{
									this.state.submitted
										? <FontAwesomeIcon icon="spinner" spin />
										: "Submit"
								}
							</Button>
						</Form>
					</Col>
				</Row>
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
