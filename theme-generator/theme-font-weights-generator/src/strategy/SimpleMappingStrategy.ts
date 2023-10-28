import { Text }        from 'figma-js'

import { FontWeights } from '../Constants'
import { Strategy }    from './Strategy'

export class SimpleMappingStrategy extends Strategy {
  fillWeights(fontWeights) {
    return fontWeights.reduce((result, fontWeight) => {
      const fontWeightItem = FontWeights.filter((item) => item.value === fontWeight)[0]

      if (fontWeight) return { ...result, [fontWeightItem?.weight]: fontWeight }

      return false
    }, {})
  }

  execute(textNodes: Text[] = []) {
    const stat = this.getStat(textNodes)

    const fontWeights = Array.from(stat.keys()).sort((a, b) => a - b)

    return {
      ...this.fillWeights(fontWeights),
    }
  }
}
