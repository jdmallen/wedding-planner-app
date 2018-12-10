import { login as loginService, logout as logoutService } from "../_services";
import { error as errorAlert } from "./alert";
import { history } from "../_helpers";

// Actions
const LOGIN_REQUEST = "wedding-planner/user/LOGIN_REQUEST";
const LOGIN_SUCCESS = "wedding-planner/user/LOGIN_SUCCESS";
const LOGIN_FAILURE = "wedding-planner/user/LOGIN_FAILURE";
const LOGOUT = "wedding-planner/user/LOGOUT";

// Reducer
const storedUser = JSON.parse(localStorage.getItem("user"));
const initialState = storedUser ? { loggedIn: true, user: storedUser } : {};

export default (state = initialState, action) =>
{
	switch (action.type)
	{
	case LOGIN_REQUEST:
		return {
			loggingIn: true,
			user: action.user,
		};
	case LOGIN_SUCCESS:
		return {
			loggedIn: true,
			user: action.user,
		};
	case LOGIN_FAILURE:
	case LOGOUT:
		return {};
	default:
		return state;
	}
};

// Action Creators
const request = user => ({ type: LOGIN_REQUEST, user });
const success = user => ({ type: LOGIN_SUCCESS, user });
const failure = error => ({ type: LOGIN_FAILURE, error });

export function login(email, password)
{
	return (dispatch) =>
	{
		dispatch(request({ email }));

		loginService(email, password)
			.then(
				(user) =>
				{
					dispatch(success(user));
					history.push("/");
				},
				(error) =>
				{
					dispatch(failure(error));
					dispatch(errorAlert(error));
				}
			);
	};
}

export function logout()
{
	logoutService();
	return { type: LOGOUT };
}
