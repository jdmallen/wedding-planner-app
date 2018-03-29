export function listHasErrored(state = false, action) {
	switch (action.type) {
	case "LIST_HAS_ERRORED":
		return action.hasErrored;

	default:
		return state;
	}
}

export function listIsLoading(state = false, action) {
	switch (action.type) {
	case "LIST_IS_LOADING":
		return action.isLoading;

	default:
		return state;
	}
}

export function list(state = [], action) {
	switch (action.type) {
	case "LIST_FETCH_DATA_SUCCESS":
		return action.list;

	default:
		return state;
	}
}
