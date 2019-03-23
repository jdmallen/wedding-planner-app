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
import Welcome from "../_components/Welcome";
import Hotels from "../_components/Hotels";
import LoginModal from "../LoginModal/LoginModal";
import RsvpForm from "../RsvpForm/RsvpForm";
import leaves1src from "../../Images/leaves1-32.png";

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
			<div>
				<Navbar color="linen" light expand="md">
					<NavbarBrand tag={Link} className={styles.kAndJText} to="/">
						K&amp;J
					</NavbarBrand>
					<NavbarToggler onClick={() => this.toggleNavbar()} />
					<Collapse isOpen={this.props.isNavbarOpen} navbar>
						<Nav className="ml-auto" navbar>
							<NavItem>
								<Link to="/rsvp" className="nav-link">RSVP</Link>
							</NavItem>
							<NavItem>
								<Link to="/hotels" className="nav-link">Hotels</Link>
							</NavItem>
							<NavItem>
								<Link to="/registries" className="nav-link">
									Registries
								</Link>
							</NavItem>
							<NavItem>
								<Link to="/itinerary" className="nav-link">Itinerary</Link>
							</NavItem>
							<NavItem>
								<Link to="/us" className="nav-link">Us</Link>
							</NavItem>
							<NavLink
								href="#"
								onClick={() => this.toggleLoginModal()}
							>Admin
							</NavLink>
						</Nav>
					</Collapse>
				</Navbar>
				<div className={styles.app}>
					<Container
						className={styles.invitationBody}
						// eslint-disable-next-line max-len
						style={{ background: `rgb(250, 240, 230) center / 90vmax no-repeat url(${leaves1src})` }}
					>
						<LoginModal />
						<Switch>
							<Route exact path="/" component={Welcome} />
							<Route path="/rsvp" component={RsvpForm} />
							<Route path="/hotels" component={Hotels} />
						</Switch>
					</Container>
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
