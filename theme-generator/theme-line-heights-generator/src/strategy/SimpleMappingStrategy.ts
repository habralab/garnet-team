import { Text }                  from 'figma-js'

import { Strategy }              from './Strategy'
import { lineHeightSmallSizes }  from '../Constants'
import { lineHeightMediumSizes } from '../Constants'
import { lineHeightLargeSizes }  from '../Constants'

export class SimpleMappingStrategy extends Strategy {
  fillSmallLineHeights(lineHeights) {
    return lineHeights.reduce(
      (result, lineHeight, index) => ({
        ...result,
        [lineHeightSmallSizes[index]]: Number(lineHeight),
      }),
      {}
    )
  }

  fillMediumLineHeights(lineHeights) {
    return lineHeights.reduce(
      (result, lineHeight, index) => ({
        ...result,
        [lineHeightMediumSizes[index]]: Number(lineHeight),
      }),
      {}
    )
  }

  fillLargeLineHeights(lineHeights) {
    return lineHeights.reduce(
      (result, lineHeight, index) => ({
        ...result,
        [lineHeightLargeSizes[index]]: Number(lineHeight),
      }),
      {}
    )
  }

  execute(textNodes: Text[] = []) {
    const stat = this.getStat(textNodes)

    const lineHeights = Array.from(stat.keys()).sort((a, b) => {
      const x = a.split('.')
      const y = b.split('.')

      return Number(x[0]) - Number(y[0]) || Number(x[1]) - Number(y[1])
    })

    const smallLineHeights = lineHeights.filter((lineHeight) => Number(lineHeight) < 1)
    const mediumLineHeights = lineHeights.filter(
      (lineHeight) => Number(lineHeight) >= 1 && Number(lineHeight) < 2
    )
    const largeLineHeights = lineHeights.filter((lineHeight) => Number(lineHeight) >= 2)

    return {
      ...this.fillSmallLineHeights(smallLineHeights),
      ...this.fillMediumLineHeights(mediumLineHeights),
      ...this.fillLargeLineHeights(largeLineHeights),
    }
  }
}
