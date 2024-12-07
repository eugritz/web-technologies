import httpProxy from 'http-proxy';

const proxy = httpProxy.createProxyServer({
  target: 'http://localhost:5177/',
  changeOrigin: true,
});

export default defineEventHandler((event) => {
  return new Promise((resolve) => {
    const options = {};

    const origEnd = event.node.res.end;
    event.node.res.end = function() {
      resolve(null);
      // @ts-ignore
      return origEnd.call(event.node.res);
    }
  
    proxy.web(event.node.req, event.node.res, options);
  });
});
