import { listConstants } from "../constants";

export default (state = {
	hasErrored: false,
	isLoading: false,
	list: [],
}, action) => {
	switch (action.type) {
	case listConstants.LIST_HAS_ERRORED:
		return {
			...state,
			hasErrored: action.payload,
		};

	case listConstants.LIST_IS_LOADING:
		return {
			...state,
			isLoading: action.payload,
		};

	case listConstants.LIST_FETCH_DATA_SUCCESS:
		return {
			...state,
			list: action.payload,
		};

	default:
		return state;
	}
};
