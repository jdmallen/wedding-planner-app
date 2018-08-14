import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import React, { Component } from "react";
import { connect } from "react-redux";
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
import { listFetchData } from "../actions/list.actions";
import toggleNav from "../actions/ui.actions";
import styles from "./AppContainer.scss";
import Entry from "../components/Entry";
import Table from "../components/Table";

class App extends Component {
	componentDidMount() {
		this.props.fetchData("https://5aae9a497389ab0014b7b953.mockapi.io" +
			"/api/v1/list");
	}

	render() {
		return (
			<div className={styles.app}>
				<Navbar color="black" dark expand="md">
					<NavbarBrand href="/">Kristen & Jesse</NavbarBrand>
					<NavbarToggler onClick={this.props.toggleNav()} />
					<Collapse isOpen={this.props.isOpen} navbar>
						<Nav className="ml-auto" navbar>
							<NavItem>
								<NavLink href="/components/">Components</NavLink>
							</NavItem>
							<NavItem>
								<NavLink href="https://github.com/reactstrap/reactstrap">GitHub</NavLink>
							</NavItem>
							<UncontrolledDropdown nav inNavbar>
								<DropdownToggle nav caret>
									Options
								</DropdownToggle>
								<DropdownMenu right>
									<DropdownItem>
										Option 1
									</DropdownItem>
									<DropdownItem>
										Option 2
									</DropdownItem>
									<DropdownItem divider />
									<DropdownItem>
										Reset
									</DropdownItem>
								</DropdownMenu>
							</UncontrolledDropdown>
						</Nav>
					</Collapse>
				</Navbar>
				<Entry />
			</div>
		);
	}
}

App.propTypes = {
	list: PropTypes.array,
	hasErrored: PropTypes.bool,
	isLoading: PropTypes.bool,
	fetchData: PropTypes.func.isRequired,
	toggleNav: PropTypes.func.isRequired,
};

App.defaultProps = {
	list: [],
	hasErrored: false,
	isLoading: false,
};

const mapStateToProps = state => ({
	list: state.listReducer.list,
	hasErrored: state.listReducer.hasErrored,
	isLoading: state.listReducer.isLoading,
});

const mapDispatchToProps = dispatch => ({
	fetchData: url => dispatch(listFetchData(url)),
	toggleNav: val => dispatch(toggleNav()),
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
