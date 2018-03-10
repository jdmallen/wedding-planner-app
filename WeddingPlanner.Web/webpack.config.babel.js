import webpack from "webpack";
import path from "path";

const BUILD_DIR = path.resolve(__dirname, "wwwroot");
const APP_DIR = path.resolve(__dirname, "ClientApp");

const isMinimized =
  process.argv.indexOf("--optimize-minimize") > -1 ||
  process.argv.indexOf("-p") > -1;

const config = {
  entry: `${APP_DIR}/index.jsx`,
  output: {
    path: BUILD_DIR,
    filename: isMinimized ? "js/app.min.js" : "js/app.js"
  },
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        loader: "babel-loader",
        query: {
          presets: ["env", "react"]
        }
      },
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loader: "eslint-loader"
      }
    ]
  },
  resolve: {
    extensions: [".js", ".jsx"]
  },
  externals: [],
  plugins: []
};

export default config;
