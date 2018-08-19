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
import { toggleNav, openModal, closeModal } from "../_ducks/ui";
import styles from "./MainApp.scss";
import Entry from "../_components/Entry";
import LoginModal from "../LoginModal/LoginModal";

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

	render()
	{
		return (
			<div className={styles.app}>
				<Navbar color="black" dark expand="md">
					<NavbarBrand tag={Link} to="/">Kristen & Jesse</NavbarBrand>
					<NavbarToggler onClick={() => this.toggleNavbar()} />
					<Collapse isOpen={this.props.isNavbarOpen} navbar>
						<Nav className="ml-auto" navbar>
							<NavItem>
								<NavLink
									href="#"
									onClick={() => this.toggleLoginModal()}
								>Login
								</NavLink>
							</NavItem>
						</Nav>
					</Collapse>
				</Navbar>
				<Container>
					<LoginModal />
					<Switch>
						<Route exact path="/" component={Entry} />
					</Switch>
				</Container>
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
