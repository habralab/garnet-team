const DEFAULT_SHARE_SCOPE = (dependencies) => ({
  '@emotion/react': {
    eager: true,
    singleton: true,
    requiredVersion: dependencies['@emotion/react'],
  },
  '@emotion/css': {
    eager: true,
    singleton: true,
    requiredVersion: dependencies['@emotion/css'],
  },
  '@emotion/styled': {
    eager: true,
    singleton: true,
    requiredVersion: dependencies['@emotion/styled'],
  },
  'react-intl': {
    eager: true,
    singleton: true,
    requiredVersion: dependencies['react-intl'],
  },
  '@ui/theme': {
    eager: true,
    singleton: true,
    requiredVersion: '*',
  },
  'styled-system': {
    eager: true,
    singleton: true,
    requiredVersion: dependencies['styled-system'],
  },
  'styled-tools': {
    eager: true,
    singleton: true,
    requiredVersion: dependencies['styled-tools'],
  },
})

module.exports = { DEFAULT_SHARE_SCOPE }
