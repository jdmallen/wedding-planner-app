import { applyMiddleware, combineReducers, createStore } from "redux";
import thunk from "redux-thunk";
import { createLogger } from "redux-logger";
import * as reducers from "./_ducks";

const middleware = [thunk];

if (process.env.ASPNETCORE_ENVIRONMENT !== "Production") {
	middleware.push(createLogger());
}

const rootReducer = combineReducers(reducers);

export default initialState => createStore(
	rootReducer,
	initialState,
	applyMiddleware(...middleware)
);
