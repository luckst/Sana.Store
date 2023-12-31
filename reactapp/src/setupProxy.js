const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api/products",
    "/api/orders",
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7279',
        secure: false
    });

    app.use(appProxy);
};