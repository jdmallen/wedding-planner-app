import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import React, { Component } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { listFetchData } from "../actions/listActions";
import styles from "./AppContainer.scss";
import Table from "../components/Table";

class App extends Component {
	componentDidMount() {
		this.props.fetchData("https://5aae9a497389ab0014b7b953.mockapi.io" +
			"/api/v1/list");
	}

	render() {
		return (
			<div className={styles.app}>
				<header className={styles.appHeader}>
					<img src="img/logo.svg" className={styles.appLogo} alt="logo" />
					<h1 className={styles.appTitle}>Welcome to React</h1>
				</header>
				<p className={styles.appIntro}>
					Symbols yeah: <FontAwesomeIcon icon="user" />
				</p>
				<Table list={this.props.list} />
			</div>
		);
	}
}

App.propTypes = {
	list: PropTypes.array,
	hasErrored: PropTypes.bool,
	isLoading: PropTypes.bool,
	fetchData: PropTypes.func.isRequired,
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
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
