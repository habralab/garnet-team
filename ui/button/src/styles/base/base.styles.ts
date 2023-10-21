import { createBaseStyles }       from '@atls-ui-parts/button'

import { styleFn }                from 'styled-system'

import { createTransitionStyles } from './factories'

const getBaseStyles = (): styleFn => {
  const baseStyles = createBaseStyles()
  const transitionStyles = createTransitionStyles()

  return () => ({
    ...baseStyles(),
    ...transitionStyles(),
  })
}

export { getBaseStyles }
