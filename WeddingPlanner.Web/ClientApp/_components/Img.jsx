import React, { Component, Suspense } from "react";
// eslint-disable-next-line camelcase
import { unstable_createResource } from "react-cache";

const ImageResource = unstable_createResource(
	source =>
		new Promise((resolve) =>
		{
			const img = new Image();
			img.src = source;
			img.onload = resolve;
		})
);

/*  We create a new img component, that will read and display
    the full resolution picture from the cache, once it gets loaded */
const Img = ({ src, alt, ...props }) =>
{
	ImageResource.read(src);
	return <img src={src} alt={alt} {...props} />;
};

export default Img;
