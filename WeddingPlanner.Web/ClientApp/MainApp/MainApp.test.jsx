import React from "react";
import ReactDOM from "react-dom";
import { it } from "jasmine";
import App from "./MainApp";

it("renders without crashing", () => {
	const div = document.createElement("div");
	ReactDOM.render(<App />, div);
	ReactDOM.unmountComponentAtNode(div);
});
