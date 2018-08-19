﻿import React, { Component } from "react";
import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import {
	Button,
	Form,
	FormGroup,
	Input,
	Label,
	Modal,
	ModalHeader,
	ModalBody,
	ModalFooter,
} from "reactstrap";
import { login } from "../_ducks/user.login";
import { openModal, closeModal } from "../_ducks/ui";
import styles from "./LoginModal.scss";

class LoginModal extends Component
{
	constructor(props)
	{
		super(props);
		this.state = { submitted: false };
	}

	componentDidMount() {}

	handleChange(e)
	{
		const { name, value } = e.target;
		this.setState({ [name]: value });
	}

	handleSubmit(e)
	{
		e.preventDefault();

		this.setState({ submitted: true });
		const { username, password } = this.state;
		const { dispatch } = this.props;
		if (username && password)
		{
			dispatch(login(username, password));
		}
	}

	toggleModal()
	{
		if (this.props.isModalOpen)
		{
			this.props.closeModal();
		}
		else
		{
			this.props.openModal();
		}
	}

	render()
	{
		return (
			<Modal
				backdrop="static"
				centered
				size="lg"
				isOpen={this.props.isModalOpen}
				toggle={() => this.toggleModal()}
			>
				<Form onSubmit={this.handleSubmit}>
					<ModalHeader
						toggle={() => this.toggleModal()}
						className={styles.properModalTitle}
					>
						Please sign in
					</ModalHeader>
					<ModalBody>
						<FormGroup className={styles.closeMarginFormControl}>
							<Label hidden htmlFor="inputEmail">Email</Label>
							<Input
								id="inputEmail"
								name="email"
								placeholder="Email address"
								required
								type="email"
								onChange={e => this.handleChange(e)}
							/>
						</FormGroup>
						<FormGroup className={styles.closeMarginFormControl}>
							<Label hidden htmlFor="inputPassword">Password</Label>
							<Input
								id="inputPassword"
								name="password"
								onChange={e => this.handleChange(e)}
								placeholder="Password"
								required
								type="password"
							/>
						</FormGroup>
					</ModalBody>
					<ModalFooter>
						<Button color="primary" type="submit">
							{
								this.state.submitted
									? <FontAwesomeIcon icon="spinner" spin />
									: "Submit"
							}
						</Button>
					</ModalFooter>
				</Form>
			</Modal>
		);
	}
}

LoginModal.propTypes = {
	isModalOpen: PropTypes.bool,
	openModal: PropTypes.func.isRequired,
	closeModal: PropTypes.func.isRequired,
};

LoginModal.defaultProps = {
	isModalOpen: false,
};

const mapStateToProps = state => ({
	isModalOpen: state.ui.isModalOpen,
});

const mapDispatchToProps = dispatch => ({
	openModal: () => dispatch(openModal()),
	closeModal: () => dispatch(closeModal()),
});

export default connect(mapStateToProps, mapDispatchToProps)(LoginModal);
