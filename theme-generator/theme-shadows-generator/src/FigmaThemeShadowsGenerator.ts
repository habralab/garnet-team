import { FigmaThemeGenerator }       from '@atls/figma-theme-generator-common'
import { FigmaThemeGeneratorResult } from '@atls/figma-theme-generator-common'
import { toColorName }               from '@atls/figma-utils'
import { toColorString }             from '@atls/figma-utils'
import { walk }                      from '@atls/figma-utils'

import { FileResponse }              from 'figma-js'

interface Shadow {
  offsetX: number
  offsetY: number
  radius: number
  spreadRadius?: number
  color: string
}

export class FigmaThemeShadowsGenerator extends FigmaThemeGenerator {
  readonly name = 'shadows'

  getShadows(nodes) {
    const shadows: Map<string, Shadow> = new Map()

    walk(nodes, (node) => {
      if (node.type === 'DROP_SHADOW' || node.type === 'INNER_SHADOW') {
        const id = `${node.offset.x}px ${node.offset.y}px ${node.radius}px ${
          node.spread ? `${node.spread}px` : ''
        }${node.spread ? ' ' : ''}${toColorString(node.color)}`

        shadows.set(id, {
          offsetX: node.offset.x,
          offsetY: node.offset.y,
          radius: node.radius,
          spreadRadius: node.spread,
          color: toColorString(node.color),
        })
      }
    })

    return Array.from(shadows.values()).reduce(
      (result, { offsetX, offsetY, radius, spreadRadius, color }) => ({
        ...result,
        [toColorName(color, Object.keys(result))]: `${offsetX}px ${offsetY}px ${radius}px ${
          spreadRadius ? `${spreadRadius}px` : ''
        }${spreadRadius ? ' ' : ''}${color}`,
      }),
      {}
    )
  }

  generate(file: FileResponse): FigmaThemeGeneratorResult {
    const values = this.getShadows(file.document.children)

    return {
      name: this.name,
      content: this.exportValuesTemplate('shadows', values),
    }
  }
}
