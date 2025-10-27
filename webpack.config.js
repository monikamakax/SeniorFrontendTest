const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const RemoveEmptyScriptsPlugin = require("webpack-remove-empty-scripts");

module.exports = (env, argv) => {
  const dev = argv.mode !== "production";

  return {
    entry: {
      "scripts/main": "./src/scripts/main.ts",
      "styles/main": "./src/styles/main.less",
    },
    output: {
      filename: "[name].js", // dist/scripts/main.js
      path: path.resolve(__dirname, "wwwroot/dist"),
      clean: true,
    },
    devtool: dev ? "source-map" : false,
    resolve: { extensions: [".ts", ".js"] },
    module: {
      rules: [
        { test: /\.ts$/, use: "ts-loader", exclude: /node_modules/ },
        {
          test: /\.less$/i,
          use: [MiniCssExtractPlugin.loader, "css-loader", "less-loader"],
        },
        { enforce: "pre", test: /\.js$/, loader: "source-map-loader" },
      ],
    },
    plugins: [
      new RemoveEmptyScriptsPlugin(), // ⬅️ removes styles/main.js
      new MiniCssExtractPlugin({
        filename: "[name].css", // dist/styles/main.css
      }),
    ],
    watchOptions: { ignored: /node_modules/ },
  };
};
