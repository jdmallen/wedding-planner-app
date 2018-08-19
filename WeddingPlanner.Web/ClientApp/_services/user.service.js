import axios from "axios";
import { authHeader, config } from "../_helpers";

export function login(email, password) {
	axios.post(`${config.apiUrl}/account/login`, {
			email,
			password,
		})
		.then((response) => {
			console.log(response);
		})
		.catch((error) => {
			console.log(error);
		});
}

export function logout() {

}

export function register(user) {

}

export function update(user) {

}

function handleResponse(resp) {

}
