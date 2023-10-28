import { Text }     from 'figma-js'

import { Fonts }    from '../Constants'
import { Strategy } from './Strategy'

export class SimpleMappingStrategy extends Strategy {
  fillFonts(fonts) {
    return fonts.reduce(
      (result, font, index) => ({
        ...result,
        [Fonts[index] || font.replace(/ /g, '')]: font,
      }),
      {}
    )
  }

  execute(textNodes: Text[] = []) {
    const stat = this.getStat(textNodes)

    const sortedFonts = Array.from(stat.entries())
      .sort((a, b) => a[1] - b[1])
      .reverse()

    const fonts = sortedFonts.map((item) => item[0])

    return {
      ...this.fillFonts(fonts),
    }
  }
}
