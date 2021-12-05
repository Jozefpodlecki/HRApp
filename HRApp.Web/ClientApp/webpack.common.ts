delete process.env.TS_NODE_PROJECT;

import * as webpackDevServer from "webpack-dev-server";
import { Configuration } from "webpack";
import { TsconfigPathsPlugin } from "tsconfig-paths-webpack-plugin";
import { resolve } from "path";
import DotenvPlugin from "webpack-dotenv-plugin";
import HtmlWebpackPlugin from "html-webpack-plugin"; // eslint-disable-line
import MiniCssExtractPlugin from "mini-css-extract-plugin";

const config: Configuration = {
    entry: {
        app: "./src/index.tsx",
    },
    output: {
        path: resolve(__dirname, "build"),
        publicPath: "/",
        filename: "[name].bundle.js",
    },
    resolve: {
        extensions: [
            ".ts",
            ".tsx",
            ".json",
            ".scss",
            ".js",
            "png",
            "svg",
            "jpg",
            "jfif",
            "gif",
            "mp3",
        ],
        plugins: [new TsconfigPathsPlugin({ baseUrl: "src" })],
    },
    module: {
        rules: [
            {
                test: /\.ts(x?)$/,
                loader: "ts-loader",
            },
            {
                test: /\.(png|jpe?g|gif|jfif|mp3)$/i,
                type: "asset/resource",
            },
        ],
    },
    optimization: {
        splitChunks: {
            chunks: "all",
        },
    },
    plugins: [
        new HtmlWebpackPlugin({
            favicon: resolve(__dirname, "src/favicon.ico"),
            template: "index.html.ejs",
        }),
    ],
};

export default config;
