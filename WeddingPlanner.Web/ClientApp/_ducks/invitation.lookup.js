import { searchInvitationsByCode as codeLookup } from "../_services";
import { error as errorAlert } from "./alert";
import { history } from "../_helpers";

// Actions
const INVITATION_LOOKUP_REQUEST	=
	"wedding-planner/invitation/INVITATION_LOOKUP_REQUEST";
const INVITATION_LOOKUP_SUCCESS	=
	"wedding-planner/invitation/INVITATION_LOOKUP_SUCCESS";
const INVITATION_LOOKUP_FAILURE	=
	"wedding-planner/invitation/INVITATION_LOOKUP_FAILURE";

// Reducer
const storedInvitation =
	JSON.parse(localStorage.getItem("invitation"));
const initialState =
	storedInvitation
		? {
			invitationFound: true,
			invitationSearching: false,
			invitation: storedInvitation,
		}
		: {
			invitationFound: false,
			invitationSearching: false,
		};

export default (state = initialState, action) =>
{
	switch (action.type)
	{
	case INVITATION_LOOKUP_REQUEST:
		return {
			invitationFound: false,
			invitationSearching: true,
			invitationCode: action.code,
		};
	case INVITATION_LOOKUP_SUCCESS:
		return {
			invitationFound: true,
			invitationSearching: false,
			invitation: action.invitation,
		};
	case INVITATION_LOOKUP_FAILURE:
		return {
			invitationFound: false,
			invitationSearching: false,
			error: action.error,
		};
	default:
		return state;
	}
};

// Action Creators
const request =	code => ({ type: INVITATION_LOOKUP_REQUEST, code });
const success =	invitation => ({ type: INVITATION_LOOKUP_SUCCESS, invitation });
const failure = error => ({ type: INVITATION_LOOKUP_FAILURE, error });

export function searchInvitationsByCode(code)
{
	return (dispatch) =>
	{
		dispatch(request({ code }));

		codeLookup(code)
			.then(
				(invitation) =>
				{
					dispatch(success(invitation));
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
