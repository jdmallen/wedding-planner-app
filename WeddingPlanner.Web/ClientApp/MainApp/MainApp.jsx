import React, { Component } from "react";
import { connect } from "react-redux";
import {
	Switch,
	Route,
	Link,
	withRouter,
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
} from "reactstrap";
import {
	closeNav,
	toggleNav,
	openModal,
	closeModal,
} from "../_ducks/ui";
import styles from "./MainApp.scss";
import Welcome from "../_components/Welcome";
import Hotels from "../_components/Hotels";
import RsvpForm from "../RsvpForm/RsvpForm";
import Itinerary from "../_components/Itinerary";
import Registries from "../_components/Registries";
import Us from "../_components/Us";
import leaves1src from "../../Images/leaves1-32.png";

class App extends Component
{
	toggleLoginModal()
	{
		const { isModalOpen, closeModal: close, openModal: open } = this.props;
		if (isModalOpen)
		{
			close();
		}
		else
		{
			open();
		}
	}

	closeNav()
	{
		const { closeNav: close } = this.props;
		close();
	}

	toggleNavbar()
	{
		const { isNavbarOpen, toggleNav: toggle } = this.props;
		toggle(!isNavbarOpen);
	}

	render()
	{
		const { isNavbarOpen } = this.props;
		return (
			<div>
				<Navbar className={styles.navBarFont} color="linen" light expand="md">
					<NavbarBrand
						tag={Link}
						className={styles.kAndJText}
						to="/"
						onClick={() => this.closeNav()}
					>
						{"K&J"}
					</NavbarBrand>
					<NavbarToggler onClick={() => this.toggleNavbar()} />
					<Collapse isOpen={isNavbarOpen} navbar>
						<Nav className="ml-auto" navbar>
							<NavItem>
								<Link
									to="/rsvp"
									className="nav-link"
									onClick={() => this.closeNav()}
								>
									{"RSVP"}
								</Link>
							</NavItem>
							<NavItem>
								<Link
									to="/hotels"
									className="nav-link"
									onClick={() => this.closeNav()}
								>
									{"Hotels"}
								</Link>
							</NavItem>
							<NavItem>
								<Link
									to="/registries"
									className="nav-link"
									onClick={() => this.closeNav()}
								>
									{"Registries"}
								</Link>
							</NavItem>
							<NavItem>
								<Link
									to="/itinerary"
									className="nav-link"
									onClick={() => this.closeNav()}
								>
									{"Itinerary"}
								</Link>
							</NavItem>
							<NavItem>
								<Link
									to="/us"
									className="nav-link"
									onClick={() => this.closeNav()}
								>
									{"Us"}
								</Link>
							</NavItem>
						</Nav>
					</Collapse>
				</Navbar>
				<div className={styles.app}>
					<Container
						className={styles.invitationBody}
						// eslint-disable-next-line max-len
						style={{ background: `url(${leaves1src}) top center / 90vmax no-repeat rgb(255, 247, 239)` }}
					>
						<Switch>
							<Route exact path="/" component={Welcome} />
							<Route path="/rsvp" component={RsvpForm} />
							<Route path="/hotels" component={Hotels} />
							<Route path="/registries" component={Registries} />
							<Route path="/itinerary" component={Itinerary} />
							<Route path="/us" component={Us} />
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
	closeNav: PropTypes.func.isRequired,
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
	closeNav: () => dispatch(closeNav()),
	toggleNav: isNavbarOpen => dispatch(toggleNav(isNavbarOpen)),
	openModal: () => dispatch(openModal()),
	closeModal: () => dispatch(closeModal()),
});

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(App));
