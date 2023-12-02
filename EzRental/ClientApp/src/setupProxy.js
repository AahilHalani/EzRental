const { createProxyMiddleware } = require('http-proxy-middleware');
const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:11517';

const context =  [
  "/weatherforecast",
    "/users",
    "/rooms",
    "/adfacility",
    "/advertisement",
    "/area",
    "/city",
    "/country",
    "/facility",
    "/rent",
    "/credential",
    "/session",
    "/images",
    "/wishlist"

];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, {
    proxyTimeout: 10000,
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};
