import axios from "axios";
import { authHeader, config } from "../_helpers";

function handleResponse(resp)
{
	const token = resp.data;
	// if (token)
	// {
	// 	localStorage.setItem("user", JSON.stringify(token))
	// }
	console.log(resp);
}

export function login(email, password)
{
	return axios.post(
		`${config.apiUrl}/account/login`,
		{
			email,
			password,
		}
	).then(response => handleResponse(response))
		.catch((error) =>
		{
			console.log(error);
		});
}

export function logout()
{

}

export function register(user)
{

}

export function update(user)
{

}
