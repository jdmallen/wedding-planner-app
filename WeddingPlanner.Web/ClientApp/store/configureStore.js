import { createStore, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import { createLogger } from "redux-logger";
import rootReducer from "../reducers";

const middleware = [thunk];

console.log(process.env);

if (process.env.ASPNETCORE_ENVIRONMENT !== "Production") {
	middleware.push(createLogger());
}

export default function(initialState) {
	return createStore(
		rootReducer,
		initialState,
		applyMiddleware(...middleware)
	);
}