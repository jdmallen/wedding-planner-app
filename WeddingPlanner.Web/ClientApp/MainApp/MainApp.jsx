import React, { Component } from "react";
import { connect } from "react-redux";
import {
	Switch,
	Route,
	Link,
} from "react-router-dom";
import PropTypes from "prop-types";
import {
	Collapse,
	Container,
	Navbar,
	NavbarToggler,
	NavbarBrand,
	Nav,
	NavItem,
	NavLink,
} from "reactstrap";
import classnames from "classnames";
import { toggleNav, openModal, closeModal } from "../_ducks/ui";
import styles from "./MainApp.scss";
import Welcome from "../_components/Welcome";
import LoginModal from "../LoginModal/LoginModal";
import leaves1src from "../../Images/leaves1-64.png";
import leaves2src from "../../Images/leaves2.png";

class App extends Component
{
	componentDidMount()	{}

	toggleLoginModal()
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

	toggleNavbar()
	{
		this.props.toggleNav(!this.props.isNavbarOpen);
	}

	render() {
		return (
			<div>
				<div className={styles.app}>
					<Container
						className={styles.invitationBody}
						style={{ background: `rgb(250, 240, 230) center / 90vmax no-repeat url(${leaves1src})` }}
					>
						<LoginModal />
						<Switch>
							<Route exact path="/" component={Welcome} />
						</Switch>
					</Container>
				</div>
				<div className={styles.footer}>
          <span className={styles.kAndJText}>K&amp;J</span>
				</div>
			</div>
		);
	}
}

App.propTypes = {
	isModalOpen: PropTypes.bool,
	isNavbarOpen: PropTypes.bool,
	toggleNav: PropTypes.func.isRequired,
};

App.defaultProps = {
	isModalOpen: false,
	isNavbarOpen: false,
};

const mapStateToProps = state => ({
	isModalOpen: state.ui.isModalOpen,
	isNavbarOpen: state.ui.isNavbarOpen,
});

const mapDispatchToProps = dispatch => ({
	toggleNav: isNavbarOpen => dispatch(toggleNav(isNavbarOpen)),
	openModal: () => dispatch(openModal()),
	closeModal: () => dispatch(closeModal()),
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
