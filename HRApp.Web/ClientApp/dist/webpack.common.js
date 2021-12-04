"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
delete process.env.TS_NODE_PROJECT;
var tsconfig_paths_webpack_plugin_1 = require("tsconfig-paths-webpack-plugin");
var path_1 = require("path");
var html_webpack_plugin_1 = __importDefault(require("html-webpack-plugin")); // eslint-disable-line
var config = {
    entry: {
        app: "./src/index.tsx",
    },
    output: {
        path: path_1.resolve(__dirname, "build"),
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
            "gif",
            "mp3",
        ],
        plugins: [new tsconfig_paths_webpack_plugin_1.TsconfigPathsPlugin({ baseUrl: "src" })],
    },
    module: {
        rules: [
            {
                test: /\.ts(x?)$/,
                loader: "ts-loader",
            },
            {
                test: /\.(png|jpe?g|gif|mp3)$/i,
                use: [
                    {
                        loader: "file-loader",
                        options: {},
                    },
                ],
            },
        ],
    },
    plugins: [
        new html_webpack_plugin_1.default({
            favicon: path_1.resolve(__dirname, "src/favicon.ico"),
            template: "index.html.ejs",
        }),
    ],
};
exports.default = config;
//# sourceMappingURL=webpack.common.js.map