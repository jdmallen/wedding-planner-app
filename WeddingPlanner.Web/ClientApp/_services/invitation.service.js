/* eslint-disable no-console */
/* eslint-disable no-unused-vars */
import axios from "axios";
import { authHeader, config } from "../_helpers";

function handleResponse(resp)
{
	const pagedResult = resp.data;
	let invitation = "";
	if (pagedResult.items.length > 0)
	{
		[invitation] = pagedResult.items;
	}
	if (invitation !== "")
	{
		localStorage.setItem("invitation", JSON.stringify(invitation));
	}
	console.info(resp);
	return invitation;
}

export async function searchInvitationsByCode(code)
{
	try
	{
		const response =
			// eslint-disable-next-line max-len
			await axios.get(`${config.apiUrl}/invitation/find?invitationCode=${code}`);
		return handleResponse(response);
	}
	catch (error)
	{
		console.error(error);
		return null;
	}
}

export async function searchInvitations(searchParameters)
{
	try
	{
		const response =
			await axios.get(
				`${config.apiUrl}/invitation/find`,
				searchParameters
			);
		return handleResponse(response);
	}
	catch (error)
	{
		console.error(error);
		return null;
	}
}
