import axios from "axios";
import { authHeader, config } from "../_helpers";

function handleResponse(resp)
{
	console.log(resp);
}

export function login(email, password)
{
	axios.post(
		`${config.apiUrl}/account/login`,
		{
			email,
			password,
		}
	)
		.then(response => handleResponse(response))
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
