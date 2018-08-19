import React, { Component } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import {
	Button,
	Col,
	Container,
	Form,
	FormGroup,
	FormText,
	Input,
	Label,
	Row,
	} from "reactstrap";
import cn from "classnames";
import { login } from "../_ducks/user.login";
import styles from "./LoginPage.scss";
import opalImgSrc from "../../Images/opal_circle_200.png";

class Login extends Component {
	componentDidMount() {
	}

	handleChange(e) {
		const { name, value } = e.target;
		this.setState({ [name]: value });
	}

	handleSubmit(e) {
		e.preventDefault();

		// this.setState({ submitted: true });
		const { username, password } = this.state;
		const { dispatch } = this.props;
		if (username && password) {
			dispatch(login(username, password));
		}
	}

	render() {
		return (
			<Row className={styles.formSignin}>
				<Col sm="6">
					<div className={styles.opalImgDiv} style={{ background: `center / contain no-repeat url(${opalImgSrc})` }}>
						<div className={styles.kAndJOverlay}>K&nbsp;&amp;&nbsp;J</div>
					</div>
				</Col>
				<Col sm="6">
					<Form onSubmit={this.handleSubmit}>
						<h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
						<FormGroup className={styles.closeMarginFormControl}>
							<Label hidden for="inputEmail">Email</Label>
							<Input id="inputEmail" name="email" placeholder="Email address" required type="email" onChange={e => this.handleChange(e)} />
						</FormGroup>
						<FormGroup className={styles.closeMarginFormControl}>
							<Label hidden for="inputPassword">Password</Label>
							<Input id="inputPassword" name="password" placeholder="Password" required type="password" onChange={e => this.handleChange(e)} />
						</FormGroup>
						<Button color="primary" type="submit">Sign in</Button>
					</Form>
				</Col>
			</Row>
		);
	}
}

Login.propTypes = {
	list: PropTypes.array,
	hasErrored: PropTypes.bool,
	isLoading: PropTypes.bool,
	isOpen: PropTypes.bool,
};

Login.defaultProps = {
	list: [],
	hasErrored: false,
	isLoading: false,
	isOpen: false,
};

const mapStateToProps = state => ({
	list: state.list.list,
	hasErrored: state.list.hasErrored,
	isLoading: state.list.isLoading,
	isOpen: state.ui.isOpen,
});

const mapDispatchToProps = dispatch => ({
});

export default connect(mapStateToProps, mapDispatchToProps)(Login);
