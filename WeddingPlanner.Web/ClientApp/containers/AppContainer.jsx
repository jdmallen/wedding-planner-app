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
import { listFetchData } from "../ducks/list";
import { toggleNav } from "../ducks/ui";
import styles from "./AppContainer.scss";
import Entry from "../components/Entry";
import Table from "../components/Table";

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
					<NavbarBrand href="/">Kristen & Jesse</NavbarBrand>
					<NavbarToggler onClick={() => this.toggle()} />
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
