import React, { Component } from "react";
import styles from "./App.scss";

class App extends Component {
	constructor() {
		super();
		this.state = { count: 0 };
	}

	increment() {
		this.setState({
			count: this.state.count + 1,
		});
	}

	render() {
		return (
			<div className={styles.app}>
				<header className={styles.appHeader}>
					<img src="img/logo.svg" className={styles.appLogo} alt="logo" />
					<h1 className={styles.appTitle}>Welcome to React</h1>
				</header>
				<p className={styles.appIntro}>
					To get started, edit <code>src/App.js</code> and save to reload.
				</p>
			</div>
		);
	}
}

export default App;
