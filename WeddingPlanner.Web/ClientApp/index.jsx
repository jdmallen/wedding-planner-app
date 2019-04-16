import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { Router, Route } from "react-router";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
	faUser,
	faArrowDown,
	faCalendarAlt,
	faCircleNotch,
	faMapMarkerAlt,
} from "@fortawesome/free-solid-svg-icons";
import store from "./store";
import App from "./MainApp/MainApp";
import { history } from "./_helpers";
import "./index.scss";
import "../Styles/customBootstrap.scss";

library.add(
	faUser,
	faArrowDown,
	faCalendarAlt,
	faCircleNotch,
	faMapMarkerAlt
);

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
