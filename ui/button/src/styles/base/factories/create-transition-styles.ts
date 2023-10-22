import { styleFn } from 'styled-system'

const createTransitionStyles = (): styleFn => () => ({
  transition: '.2s',
})

export { createTransitionStyles }
