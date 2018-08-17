import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { Router, Route, IndexRoute } from "react-router";
import createBrowserHistory from "history/createBrowserHistory";
import fontawesome from "@fortawesome/fontawesome";
import { faUser } from "@fortawesome/fontawesome-free-solid";
import store from "./store";
import App from "./containers/AppContainer";
import { getBrowserInfo } from "./helpers";
import "./index.scss";
import "../Styles/customBootstrap.scss";

getBrowserInfo();

fontawesome.library.add(faUser);

const history = createBrowserHistory();

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
