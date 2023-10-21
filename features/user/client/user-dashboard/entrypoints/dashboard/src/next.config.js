const { ModuleFederationPlugin } = require("webpack").container;

module.exports = {
  experimental: {
    externalDir: true,
    swcFileReading: false,
    workerThreads: true,
    esmExternals: 'loose',
  },
  output: 'standalone',
  webpack: (config, _) => {
    // TODO: reintroduce module federation
    // config.plugins.push(
    //   new ModuleFederationPlugin({
    //     name: "UserDashboard",
    //     library: { type: config.output.libraryTarget, name: "UserDashboard" },
    //     filename: "remoteEntry.js",
    //     exposes: {
    //       "./Dashboard": "./components/Dashboard",
    //     },
    //     shared: ["react", "react-dom"],
    //   })
    // );
    config.module.rules.push({
      test: /\.(woff|woff2|eot|ttf|otf)$/i,
      type: 'asset/resource',
      generator: {
        filename: 'static/media/fonts/[name][ext]',
      },
    });
    return config;
  }
}
