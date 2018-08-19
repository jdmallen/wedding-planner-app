// Actions
const TOGGLE_NAV = "wedding-planner/ui/TOGGLE_NAV";
const OPEN_MODAL = "wedding-planner/ui/OPEN_MODAL";
const CLOSE_MODAL = "wedding-planner/ui/CLOSE_MODAL";

// Reducer
export default (state = {
	isNavbarOpen: false,
	isModalOpen: false,
}, action) =>
{
	switch (action.type)
	{
	case TOGGLE_NAV:
		return {
			...state,
			isNavbarOpen: action.payload,
		};
	case OPEN_MODAL:
		return {
			...state,
			isModalOpen: true,
		};
	case CLOSE_MODAL:
		return {
			...state,
			isModalOpen: false,
		};
	default:
		return state;
	}
};

// Action Creators
export function toggleNav(boolValue)
{
	return {
		type: TOGGLE_NAV,
		payload: boolValue,
	};
}

export function openModal()
{
	return {
		type: OPEN_MODAL,
	};
}

export function closeModal()
{
	return {
		type: CLOSE_MODAL,
	};
}
