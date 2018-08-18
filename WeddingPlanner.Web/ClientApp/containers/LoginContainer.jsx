import React, { Component } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import {
	Col,
	Container,
	Row,
	} from "reactstrap";
import cn from "classnames";
import styles from "./LoginContainer.scss";
import opalImgSrc from "../../Images/opal_circle_200.png";

class Login extends Component {
	componentDidMount() {
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
					<form>
						<h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
						<label htmlFor="inputEmail" className="sr-only">Email address</label>
						<input id="inputEmail" className="form-control" placeholder="Email address" required="" type="email" />
						<label htmlFor="inputPassword" className="sr-only">Password</label>
						<input id="inputPassword" className="form-control" placeholder="Password" required="" type="password" />
						<div className="checkbox mb-3">
							<label htmlFor="rememberMe">
								<input id="rememberMe" value="remember-me" type="checkbox" /> Remember me
							</label>
						</div>
						<button className="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
						<p className="mt-5 mb-3 text-muted">© 2017-2018</p>
					</form>
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
