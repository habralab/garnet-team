const DEFAULT_SHARE_SCOPE = {
  'next/dynamic': {
    eager: false,
    requiredVersion: false,
    singleton: true,
    import: undefined,
  },
  'next/head': {
    eager: false,
    requiredVersion: false,
    singleton: true,
    import: undefined,
  },
  'next/link': {
    eager: true,
    requiredVersion: false,
    singleton: true,
    import: undefined,
  },
  'next/router': {
    requiredVersion: false,
    singleton: true,
    import: false,
    eager: false,
  },
  'next/script': {
    requiredVersion: false,
    singleton: true,
    import: undefined,
    eager: false,
  },
  react: {
    singleton: true,
    requiredVersion: false,
    eager: false,
    import: false,
  },
  'react-dom': {
    singleton: true,
    requiredVersion: false,
    eager: false,
    import: false,
  },
  'react/jsx-dev-runtime': {
    singleton: true,
    requiredVersion: false,
    import: undefined,
    eager: false,
  },
  'react/jsx-runtime': {
    singleton: true,
    requiredVersion: false,
    eager: false,
    import: false,
  },
  'styled-jsx': {
    requiredVersion: false,
    singleton: true,
    import: undefined,
    eager: false,
  },
  'styled-jsx/style': {
    requiredVersion: false,
    singleton: true,
    import: undefined,
    eager: false,
  },
}

module.exports = { DEFAULT_SHARE_SCOPE }
