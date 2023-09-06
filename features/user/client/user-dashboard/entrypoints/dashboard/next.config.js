module.exports = {
  experimental: {
    externalDir: true,
    swcFileReading: false,
    workerThreads: true,
    esmExternals: 'loose',
    outputStandalone: true,
  },
}

const { ModuleFederationPlugin } = require("webpack").container;

module.exports = {
  webpack: (config, options) => {
    config.plugins.push(
      new ModuleFederationPlugin({
        name: "UserDashboard",
        library: { type: config.output.libraryTarget, name: "UserDashboard" },
        filename: "remoteEntry.js",
        exposes: {
          "./Dashboard": "./components/Dashboard",
        },
        shared: ["react", "react-dom"],
      })
    );
    return config;
  },
};
