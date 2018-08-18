import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { Router, Route, IndexRoute } from "react-router";
import fontawesome from "@fortawesome/fontawesome";
import { faUser } from "@fortawesome/fontawesome-free-solid";
import store from "./store";
import App from "./containers/AppContainer";
import { getBrowserInfo, history } from "./helpers";
import "./index.scss";
import "../Styles/customBootstrap.scss";

getBrowserInfo();

fontawesome.library.add(faUser);

// pass initialState to store() once you have it
const reduxStore = store();

const router = (
	<Provider store={reduxStore}>
		<Router history={history}>
			<Route path="/" component={App} />
		</Router>
	</Provider>
);

render(
	router,
	document.getElementById("root")
);
