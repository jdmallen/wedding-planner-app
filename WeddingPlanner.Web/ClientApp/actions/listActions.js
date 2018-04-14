import axios from "axios";

export function listHasErrored(boolValue) {
	return {
		type: "LIST_HAS_ERRORED",
		payload: boolValue,
	};
}

export function listIsLoading(boolValue) {
	return {
		type: "LIST_IS_LOADING",
		payload: boolValue,
	};
}

export function listFetchDataSuccess(list) {
	return {
		type: "LIST_FETCH_DATA_SUCCESS",
		payload: list,
	};
}

export function errorAfterFiveSeconds() {
	// We return a function instead of an action object
	return (dispatch) => {
		setTimeout(() => {
			// This function is able to dispatch other action creators
			dispatch(listHasErrored(true));
		}, 5000);
	};
}

export function listFetchData(url) {
	return (dispatch) => {
		dispatch(listIsLoading(true));

		axios.get(url)
			.then((response) => {
				console.log(response);
				dispatch(listIsLoading(false));
				return response;
			})
			.then(response => dispatch(listFetchDataSuccess(response.data)))
			.catch(() => dispatch(listHasErrored(true)))
			.finally(() => dispatch(listIsLoading(false)));
	};
}
