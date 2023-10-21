const NextModuleFederationPlugin = require('@module-federation/nextjs-mf');

const remotes = (isServer) => {
  const location = isServer ? 'ssr' : 'chunks';
  return {
    remote: `UserDashboard@http://localhost:3001/_next/static/${location}/remoteEntry.js`,
  };
};

module.exports = {
  experimental: {
    externalDir: true,
    swcFileReading: false,
    workerThreads: true,
    esmExternals: 'loose',
  },
  output: 'standalone',
  webpack: (config, { isServer }) => {
    config.plugins.push(
      new NextModuleFederationPlugin({
        name: 'App',
        filename: 'static/chunks/remoteEntry.js',
        remotes: remotes(isServer)
      })
    );
    config.module.rules.push({
      test: /\.(woff|woff2|eot|ttf|otf)$/i,
      type: 'asset/resource',
      generator: {
        filename: 'static/media/fonts/[name][ext]',
      },
    });
    return config;
  }
};
