const path = require('path');
module.exports = {
    entry: ['./app/layout.jsx','./app/ReactIndex.jsx'] ,
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: 'bundle.js'
    },
    module: {
        rules: [
            //第一個loader編譯JSX
            {
              test: /.jsx$/,
              exclude: /node_modules/,
              use: {
                loader: "babel-loader",
                options: {
                  presets: ['@babel/preset-react','@babel/preset-env']
                }
              }
            }
]
    }
};