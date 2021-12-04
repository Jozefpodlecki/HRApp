"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
var webpack_merge_1 = require("webpack-merge");
var path_1 = require("path");
var mini_css_extract_plugin_1 = __importDefault(require("mini-css-extract-plugin"));
var webpack_common_1 = __importDefault(require("./webpack.common"));
var environment = "development";
process.env.NODE_ENV = environment;
var config = webpack_merge_1.merge(webpack_common_1.default, {
    mode: environment,
    devtool: "inline-source-map",
    devServer: {
        hot: true,
        //contentBase: resolve(__dirname, "build/assets"),
        historyApiFallback: true,
        //disableHostCheck: true,
        //stats: "minimal",
        //clientLogLevel: "warning",
        headers: {
            "Set-Cookie": "HttpOnly;Secure;SameSite=Strict",
        },
    },
    module: {
        rules: [
            {
                test: /styles\.scss$/,
                use: [
                    "style-loader",
                    {
                        loader: "css-loader",
                    },
                    {
                        loader: "sass-loader",
                        options: {
                            sassOptions: {
                                indentWidth: 4,
                                includePaths: [
                                    path_1.resolve(__dirname, "src/styles"),
                                ],
                            },
                        },
                    },
                ],
            },
            {
                test: /\.scss$/,
                exclude: /styles\.scss$/,
                use: [
                    "style-loader",
                    {
                        loader: "css-loader",
                        options: {
                            modules: true,
                            sourceMap: true,
                        },
                    },
                    {
                        loader: "postcss-loader",
                    },
                    {
                        loader: "sass-loader",
                        options: {
                            sourceMap: true,
                            sassOptions: {
                                indentWidth: 4,
                                includePaths: [
                                    path_1.resolve(__dirname, "src/styles"),
                                ],
                            },
                        },
                    },
                ],
            },
        ],
    },
    plugins: [
        new mini_css_extract_plugin_1.default({
            filename: "[name].css",
            chunkFilename: "[id].css",
        }),
    ],
});
exports.default = config;
//# sourceMappingURL=webpack.development.js.map