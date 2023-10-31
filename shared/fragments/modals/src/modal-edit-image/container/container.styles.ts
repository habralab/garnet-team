import { styleFn }    from 'styled-system'
import { prop }       from 'styled-tools'
import { switchProp } from 'styled-tools'

export const containerBaseStyle: styleFn = () =>
  ({ theme }) => ({
    width: '100%',

    '.cropper-modal': {
      borderRadius: theme.radii.medium,
      backgroundColor: theme.backgrounds.black,
      opacity: 0.8,
    },

    [`.point-e, .point-n, .point-w, .point-s,
    .line-e, .line-n, .line-w, .line-s`]: {
      display: 'none',
    },

    '.cropper-view-box': {
      outline: 'none',
    },

    '.cropper-point': {
      backgroundColor: theme.backgrounds.accentHover,
      border: theme.borders.white,
      height: 6,
      width: 6,
    },
  })

const containerCircleShapeStyle: styleFn = () =>
  ({ theme }) => ({
    '.cropper-view-box': {
      borderRadius: theme.radii.half,
    },
  })

const containerSquareShapeStyle: styleFn = () => ({
  '.cropper-view-box': {
    borderRadius: 'none',
  },
})

export const containerShapeStyle: styleFn = switchProp(prop('shape', 'circle'), {
  circle: containerCircleShapeStyle,
  square: containerSquareShapeStyle,
})
