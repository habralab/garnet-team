const NextFederationPlugin = require('@module-federation/nextjs-mf')

module.exports = {
  experimental: {
    externalDir: true,
    swcFileReading: false,
    workerThreads: true,
    esmExternals: 'loose',
  },
  output: 'standalone',
  webpack: (config, _) => {
    config.plugins.push(
      new NextFederationPlugin({
        name: 'Team',
        filename: 'static/chunks/remoteEntry.js',
        exposes: {
          './index-page': './pages/index.ts',
        },
      })
    )
    config.module.rules.push({
      test: /\.(woff|woff2|eot|ttf|otf)$/i,
      type: 'asset/resource',
      generator: {
        filename: 'static/media/fonts/[name][ext]',
      },
    })
    return config
  },
}
