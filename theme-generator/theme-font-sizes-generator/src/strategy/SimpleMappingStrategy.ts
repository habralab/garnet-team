import { Text }                         from 'figma-js'

import { FontSizeDefaultName }          from '../Constants'
import { Group }                        from '../Constants'
import { Strategy }                     from './Strategy'
import { groupNamesGreaterThanDefault } from '../Constants'
import { groupNamesLessThanDefault }    from '../Constants'

export class SimpleMappingStrategy extends Strategy {
  fillSizes(fontSizes) {
    const tempTheme = {}

    const middle = Math.floor(fontSizes.length / 2)

    const less = fontSizes.filter((_, index) => index < middle)
    const greater = fontSizes.filter((_, index) => index > middle)

    const groupLess = [...groupNamesLessThanDefault]
    const groupGreater = [...groupNamesGreaterThanDefault]

    if (fontSizes.length === 1) {
      tempTheme[FontSizeDefaultName] = fontSizes
    }

    if (fontSizes.length > 1) {
      for (const value of less) {
        const nextGroupName = groupLess.pop()

        tempTheme[nextGroupName as string] = value
      }
    }

    const reversedKeysTheme = Object.keys(tempTheme).reverse()
    const themeValues = Object.values(tempTheme)

    const theme = reversedKeysTheme.reduce(
      (result, key, index) => ({
        ...result,
        [key]: themeValues[index],
      }),
      {}
    )

    for (const [index, value] of fontSizes.entries()) {
      if (index === middle) {
        theme[FontSizeDefaultName] = value
      }
    }

    for (const value of greater) {
      const nextGroupName = groupGreater.pop()

      theme[nextGroupName as string] = value
    }

    return theme
  }

  execute(textNodes: Text[] = []) {
    const stat = this.getStat(textNodes)

    const fontSizes = Array.from(stat.keys()).sort((a, b) => a - b)

    const smallSizes = fontSizes.filter((size) => size < 14)
    const normalSizes = fontSizes.filter((size) => size < 24 && size > 14)
    const mediumSizes = fontSizes.filter((size) => size < 50 && size > 24)
    const largeSizes = fontSizes.filter((size) => size > 50)

    return {
      [Group.SMALL]: {
        ...this.fillSizes(smallSizes),
      },
      [Group.NORMAL]: {
        ...this.fillSizes(normalSizes),
      },
      [Group.MEDIUM]: {
        ...this.fillSizes(mediumSizes),
      },
      [Group.LARGE]: {
        ...this.fillSizes(largeSizes),
      },
    }
  }
}
