module.exports = {
  basePath: '/app',
  experimental: {
    externalDir: true,
    swcFileReading: false,
    workerThreads: true,
    esmExternals: 'loose',
  },
  typescript: {
    ignoreBuildErrors: true,
  },
  output: 'standalone',
  webpack: (config, _) => {
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
