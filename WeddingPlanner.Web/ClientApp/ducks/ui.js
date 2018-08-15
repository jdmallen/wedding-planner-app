// Actions
const TOGGLE_NAV = "wedding-planner/ui/TOGGLE_NAV";

// Reducer
export default (state = {
	isOpen: false,
}, action) => {
	switch (action.type) {
	case TOGGLE_NAV:
		return {
			...state,
			isOpen: action.payload,
		};
	default:
		return state;
	}
};

// Action Creators
export function toggleNav(boolValue) {
	return {
		type: TOGGLE_NAV,
		payload: boolValue,
	};
}
