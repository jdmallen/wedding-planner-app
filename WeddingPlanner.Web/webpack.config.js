const path = require("path");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const CleanWebpackPlugin = require("clean-webpack-plugin");

const BUILD_DIR = path.resolve(__dirname, "wwwroot");
const APP_DIR = path.resolve(__dirname, "ClientApp");

const isProd =
	process.argv.indexOf("--optimize-minimize") > -1 ||
	process.argv.indexOf("-p") > -1;

module.exports = {
	devtool: isProd ? "" : "source-map",
	entry: ["babel-polyfill", `${APP_DIR}/index.jsx`],
	module: {
		rules: [
			{
				test: /\.(js|jsx)$/,
				use: [
					"babel-loader",
					"eslint-loader"
				],
				exclude: /node_modules/,
			},
			{
				test: /\.scss$/,
				use: ExtractTextPlugin.extract({
					fallback: "style-loader",
					use: [
						{
							loader: "css-loader",
							options: {
								modules: true,
								localIdentName: "[name]__[local]___[hash:base64:5]"
							}
						},
						"sass-loader"
					]
				})
			},
		]
	},
	output: {
		path: BUILD_DIR,
		filename: isProd ? "js/[name].bundle.min.js" : "js/[name].bundle.js"
	},
	plugins: [
		new ExtractTextPlugin("css/[name].css", { allChunks: true }),
		new CleanWebpackPlugin([BUILD_DIR] + "/*.*"),
	],
	resolve: {
		extensions: [".js", ".jsx"]
	},
};