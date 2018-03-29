import React from "react";
import { render } from "react-dom";
import { createStore, applyMiddleware } from "redux";
import { Provider } from "react-redux";
import fontawesome from "@fortawesome/fontawesome";
import { faUser, faCircle } from "@fortawesome/fontawesome-free-solid";
import { faFacebook } from "@fortawesome/fontawesome-free-brands";
import configureStore from "./redux/configureStore";
import App from "./containers/App";

fontawesome.library.add(faUser);

const store = configureStore();

render(
	<Provider store={store}>
		<App />
	</Provider>,
	document.getElementById("root")
);
