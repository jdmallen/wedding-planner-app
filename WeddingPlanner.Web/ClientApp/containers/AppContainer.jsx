import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import React, { Component } from "react";
import { connect } from "react-redux";
import { Switch, Route, Link, NavLink as RouterNavLink } from "react-router-dom";
import PropTypes from "prop-types";
import {
	Collapse,
	Navbar,
	NavbarToggler,
	NavbarBrand,
	Nav,
	NavItem,
	NavLink,
	UncontrolledDropdown,
	DropdownToggle,
	DropdownMenu,
	DropdownItem,
	} from "reactstrap";
import { listFetchData } from "../ducks/list";
import { toggleNav } from "../ducks/ui";
import styles from "./AppContainer.scss";
import Entry from "../components/Entry";
import Login from "../components/Login";

class App extends Component {
	componentDidMount() {
		this.props.fetchData("https://5aae9a497389ab0014b7b953.mockapi.io" +
			"/api/v1/list");
	}

	toggle() {
		this.props.toggleNav(!this.props.isOpen);
	}

	render() {
		return (
			<div className={styles.app}>
				<Navbar color="black" fixed dark expand="md">
					<NavbarBrand tag={Link} to="/">Kristen & Jesse</NavbarBrand>
					<NavbarToggler onClick={() => this.toggle()} />
					<Collapse isOpen={this.props.isOpen} navbar>
						<Nav className="ml-auto" navbar>
							<NavItem>
								<NavLink tag={RouterNavLink} to="/login">Login</NavLink>
							</NavItem>
						</Nav>
					</Collapse>
				</Navbar>
				<Switch>
					<Route exact path="/" component={Entry} />
					<Route path="/login" component={Login} />
				</Switch>
			</div>
		);
	}
}

App.propTypes = {
	list: PropTypes.array,
	hasErrored: PropTypes.bool,
	isLoading: PropTypes.bool,
	isOpen: PropTypes.bool,
	fetchData: PropTypes.func.isRequired,
	toggleNav: PropTypes.func.isRequired,
};

App.defaultProps = {
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
	fetchData: url => dispatch(listFetchData(url)),
	toggleNav: isOpen => dispatch(toggleNav(isOpen)),
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
