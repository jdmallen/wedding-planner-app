import { uiConstants } from "../constants";

export default (state = {
	isOpen: false,
}, action) => {
	switch (action.type) {
	case uiConstants.TOGGLE_NAV:
		return {
			...state,
			isOpen: !state.isOpen,
		};
	default:
		return state;
	}
};
