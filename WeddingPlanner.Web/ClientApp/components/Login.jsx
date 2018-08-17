import React from "react";
import styles from "./Login.scss";

const Login = props => (
	<form className="form-signin">
		<img className="mb-4" src="../../assets/brand/bootstrap-solid.svg" alt="" width="72" height="72" />
		<h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
		<label htmlFor="inputEmail" className="sr-only">Email address</label>
		<input id="inputEmail" className="form-control" placeholder="Email address" required="" type="email" />
		<label htmlFor="inputPassword" className="sr-only">Password</label>
		<input id="inputPassword" className="form-control" placeholder="Password" required="" type="password" />
		<div className="checkbox mb-3">
			<label htmlFor="rememberMe">
				<input id="rememberMe" value="remember-me" type="checkbox" /> Remember me
			</label>
		</div>
		<button className="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
		<p className="mt-5 mb-3 text-muted">© 2017-2018</p>
	</form>
);

export default Login;
