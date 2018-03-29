import { combineReducers } from "redux";
import { list, listHasErrored, listIsLoading } from "./list";

export default combineReducers({
	list,
	listHasErrored,
	listIsLoading
});
