// Actions
const CLEAR = "wedding-planner/alert/CLEAR";
const ERROR = "wedding-planner/alert/ERROR";
const SUCCESS = "wedding-planner/alert/SUCCESS";

// Reducer
export default (state = {}, action) => {
	switch (action.type) {
	case CLEAR:
		return {};
	case ERROR:
		return {
			cssClass: "alert-danger",
			message: action.message
		};
	case SUCCESS:
		return {
			cssClass: "alert-success",
			message: action.message
		};
	default:
		return state;
	}
};

// Action Creators
export const clear = () => ({ type: CLEAR });
export const error = message => ({ type: ERROR, message });
export const success = message => ({ type: SUCCESS, message });
