import { combineReducers } from "redux";
import listReducer from "./list.reducer";
import uiReducer from "./ui.reducer";

export default combineReducers({
	listReducer,
	uiReducer,
});
