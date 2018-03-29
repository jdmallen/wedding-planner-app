import FontAwesomeIcon from "@fortawesome/react-fontawesome";
import React, { Component } from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { listFetchData } from "../redux/actions/list";
import styles from "./App.scss";

class App extends Component {
	componentDidMount() {
		this.props.fetchData("http(s)://5aae9a497389ab0014b7b953.mockapi.io/api/v1/list");
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
			</div>
		);
	}
}

App.propTypes = {
	list: PropTypes.array.isRequired,
	hasErrored: PropTypes.bool.isRequired,
	isLoading: PropTypes.bool.isRequired,
	fetchData: PropTypes.func.isRequired,
};

const mapStateToProps = state => ({
	list: state.list,
	hasErrored: state.listHasErrored,
	isLoading: state.listIsLoading,
});

const mapDispatchToProps = dispatch => ({
	fetchData: url => dispatch(listFetchData(url)),
});

export default connect(mapStateToProps, mapDispatchToProps)(App);
