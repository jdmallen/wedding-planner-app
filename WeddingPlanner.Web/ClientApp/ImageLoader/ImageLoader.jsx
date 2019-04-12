import React, { Component } from "react";
import { withRouter } from "react-router-dom";

const loaded = {};

class ImageLoader extends Component
{
	// define our loading and loaded image classes
	static defaultProps =
	{
		className: "",
		loadingClassName: "img-loading",
		loadedClassName: "img-loaded",
	};

	// initial state: image loaded stage
	state =
	{
		loaded: loaded[this.props.src],
	};

	// image onLoad handler to update state to loaded
	onLoad = () =>
	{
		loaded[this.props.src] = true;
		this.setState(() => ({ loaded: true }));
	};

	render()
	{
		let { className } = this.props;
		const { loadedClassName, loadingClassName, ...props } = this.props;

		className = `${className} ${this.state.loaded
			? loadedClassName
			: loadingClassName}`;

		return (
			<img
				alt={props.alt}
				src={props.src}
				className={className}
				onLoad={this.onLoad}
			/>
		);
	}
}

export default withRouter(ImageLoader);
