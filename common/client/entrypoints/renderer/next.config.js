/**
 * @type {import('next').NextConfig}
 **/

const {NextFederationPlugin} = require('@module-federation/nextjs-mf')
const shared = require('../../../../sharedEmotion').DEFAULT_SHARE_SCOPE
const deps = require('./package.json').dependencies

const remotes = (isServer) => {
  const location = isServer ? 'ssr' : 'chunks'
  return {
    identity: `identity@http://localhost:3000/_next/static/${location}/remoteEntry.js`,
    project: `project@http://localhost:3001/_next/static/${location}/remoteEntry.js`,
    team: `team@http://localhost:3002/_next/static/${location}/remoteEntry.js`,
    user: `user@http://localhost:3003/_next/static/${location}/remoteEntry.js`,
  }
}

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
      new NextFederationPlugin({
        name: 'app',
        filename: 'static/chunks/remoteEntry.js',
        remotes: remotes(isServer),
        shared: shared(deps),
        extraOptions: {
          debug: true,
        }
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
