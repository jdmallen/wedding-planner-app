import path from "path";
import ExtractTextPlugin from "extract-text-webpack-plugin";

const BUILD_DIR = path.resolve(__dirname, "wwwroot");
const APP_DIR = path.resolve(__dirname, "ClientApp");

const isMinimized =
  process.argv.indexOf("--optimize-minimize") > -1 ||
  process.argv.indexOf("-p") > -1;

const config = {
	entry: `${APP_DIR}/index.jsx`,
	output: {
		path: BUILD_DIR,
		filename: isMinimized ? "js/[name].bundle.min.js" : "js/[name].bundle.js"
	},
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
	resolve: {
		extensions: [".js", ".jsx"]
	},
	externals: [],
	plugins: [
		new ExtractTextPlugin("css/[name].css", { allChunks: true })
	]
};

export default config;
