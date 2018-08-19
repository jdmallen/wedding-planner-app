import { userService } from "../_services";
import { success as successAlert, error as errorAlert } from "./alert";
import { history } from "../_helpers";

// Actions
const REGISTER_REQUEST = "wedding-planner/user/REGISTER_REQUEST";
const REGISTER_SUCCESS = "wedding-planner/user/REGISTER_SUCCESS";
const REGISTER_FAILURE = "wedding-planner/user/REGISTER_FAILURE";

// Reducer
export default (state = {}, action) => {
	switch (action.type) {
	case REGISTER_REQUEST:
		return { registering: true };
	case REGISTER_SUCCESS:
		return {};
	case REGISTER_FAILURE:
		return {};
	default:
		return state;
	}
};

// Action Creators
const request = user => ({ type: REGISTER_REQUEST, user });
const success = user => ({ type: REGISTER_SUCCESS, user });
const failure = error => ({ type: REGISTER_FAILURE, error });

export function register(user) {
	return (dispatch) => {
		dispatch(request(user));

		userService.register(user)
			.then(
				(userResp) => {
					dispatch(success(userResp));
					history.push("/login");
					dispatch(successAlert("Registration successful!"));
				},
				(error) => {
					dispatch(failure(error));
					dispatch(errorAlert(error));
				}
			);
	};
}
