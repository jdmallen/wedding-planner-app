import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import fontawesome from "@fortawesome/fontawesome";
import { faUser } from "@fortawesome/fontawesome-free-solid";
import configureStore from "./store/configureStore";
import App from "./containers/AppContainer";
import { getBrowserInfo } from "./helpers";
import "./index.scss";
import "../Styles/customBootstrap.scss";

getBrowserInfo();

fontawesome.library.add(faUser);

const store = configureStore();

render(
	<Provider store={store}>
		<App />
	</Provider>,
	document.getElementById("root")
);
