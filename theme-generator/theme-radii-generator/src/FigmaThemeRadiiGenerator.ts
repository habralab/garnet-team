import { FigmaThemeGenerator }       from '@atls/figma-theme-generator-common'
import { FigmaThemeGeneratorResult } from '@atls/figma-theme-generator-common'
import { toRadiiString }             from '@atls/figma-utils'
import { walk }                      from '@atls/figma-utils'

import { FileResponse }              from 'figma-js'

export class FigmaThemeRadiiGenerator extends FigmaThemeGenerator {
  readonly name = 'radii'

  getRadii(nodes) {
    const radii: Set<number | { token: string; radius: string }> = new Set()

    walk(nodes, (node) => {
      if (node.cornerRadius) {
        const radius = Math.round(node.cornerRadius)

        if (radius !== 0) {
          radii.add(radius)
        }
      }

      if (node.rectangleCornerRadii) {
        const cornerRadii = node.rectangleCornerRadii

        const duplicate = cornerRadii.reduce((acc, item) => {
          acc[item] = (acc[item] || 0) + 1

          return acc
        }, {})

        const radius = toRadiiString(node.rectangleCornerRadii)

        if (!Object.values(duplicate).includes(4)) {
          radii.add(radius)
        }
      }
    })

    let fullRadii: number[] = []
    const customRadii: Array<{ token: string; radius: string }> = []

    Array.from(radii).forEach((el) =>
      (Number.isInteger(el) ? fullRadii : customRadii).push(el as never))

    fullRadii = fullRadii.sort((a, b) => Number(a) - Number(b))

    const sortedRadii: Array<number | { token: string; radius: string }> = [
      ...fullRadii,
      ...customRadii,
    ]

    return sortedRadii.reduce(
      (result, radius) => ({
        ...(result as object),
        [typeof radius === 'number' ? `f${radius}` : radius.token]:
          typeof radius === 'number' ? radius : radius.radius,
      }),
      {}
    )
  }

  generate(file: FileResponse): FigmaThemeGeneratorResult {
    const values = this.getRadii(file.document.children)

    return {
      name: this.name,
      content: this.exportValuesTemplate('radii', values),
    }
  }
}
