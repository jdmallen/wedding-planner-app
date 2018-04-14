export default (state = {
	hasErrored: false,
	isLoading: false,
	list: [],
}, action) => {
	switch (action.type) {
	case "LIST_HAS_ERRORED":
		return {
			...state,
			hasErrored: action.payload,
		};

	case "LIST_IS_LOADING":
		return {
			...state,
			isLoading: action.payload,
		};

	case "LIST_FETCH_DATA_SUCCESS":
		return {
			...state,
			list: action.payload,
		};

	default:
		return state;
	}
};
