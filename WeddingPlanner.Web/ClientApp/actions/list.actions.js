import axios from "axios";
import listConstants from "../constants/list.constants";

export function listHasErrored(boolValue) {
	return {
		type: listConstants.LIST_HAS_ERRORED,
		payload: boolValue,
	};
}

export function listIsLoading(boolValue) {
	return {
		type: listConstants.LIST_IS_LOADING,
		payload: boolValue,
	};
}

export function listFetchDataSuccess(list) {
	return {
		type: listConstants.LIST_FETCH_DATA_SUCCESS,
		payload: list,
	};
}

export function listFetchData(url) {
	return (dispatch) => {
		dispatch(listIsLoading(true));

		axios.get(url)
			.then((response) => {
				dispatch(listIsLoading(false));
				return response;
			})
			.then(response => dispatch(listFetchDataSuccess(response.data)))
			.catch(() => dispatch(listHasErrored(true)))
			.finally(() => dispatch(listIsLoading(false)));
	};
}