/**
 * @type {import('next').NextConfig}
 **/

const {NextFederationPlugin} = require('@module-federation/nextjs-mf')
const shared = require('../../../../../sharedEmotion').DEFAULT_SHARE_SCOPE
const deps = require('./package.json').dependencies

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
        name: 'project',
        filename: 'static/chunks/remoteEntry.js',
        shared: shared(deps),
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
