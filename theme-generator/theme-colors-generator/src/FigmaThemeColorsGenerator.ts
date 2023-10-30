import { FigmaThemeGenerator }       from '@atls/figma-theme-generator-common'
import { FigmaThemeGeneratorResult } from '@atls/figma-theme-generator-common'
import { clearStringOfSpecialChars } from '@atls/figma-utils'
import { isColor }                   from '@atls/figma-utils'
import { toColorOpacityString }      from '@atls/figma-utils'
import { toAverage }                 from '@atls/figma-utils'
import { toColorName }               from '@atls/figma-utils'
import { toColorString }             from '@atls/figma-utils'
import { walk }                      from '@atls/figma-utils'

import camelCase                     from 'camelcase'
import { FileResponse }              from 'figma-js'

import { ButtonState }               from './Interfaces'

export class FigmaThemeColorsGenerator extends FigmaThemeGenerator {
  readonly name = 'colors'

  readonly buttonFrameId = 'Desktop / Buttons'

  formatString(str: string): string {
    return camelCase(clearStringOfSpecialChars(str), { pascalCase: false })
  }

  getColor(obj) {
    if (obj.type === 'TEXT') return toColorString(obj.fills[0]?.color)
    if (obj.type === 'INSTANCE') return toColorString(obj.children[0].fills[0].color)

    return ''
  }

  getColors(nodes) {
    const colors = {}
    const buttonNames: string[] = []
    const buttonStates: ButtonState[] = []

    walk(nodes, (node) => {
      const { name } = node

      if (name?.match(this.buttonFrameId)) {
        const names = node.children.map((item) =>
          item.children?.map((buttonName) => buttonName.name))

        const buttons = node.children
          .map((child) =>
            child.children?.map((item) => {
              const obj = {
                name: item.name,
                default: item.children[0],
                hover: item.children[1],
                pressed: item.children[2] !== undefined ? item.children[2] : item.children[0],
                disabled: item.children[3] !== undefined ? item.children[3] : item.children[0],
              }

              const fontColorDefault = this.getColor(obj.default.children[0])
              const backgroundColorDefault = toColorString(obj.default.backgroundColor)
              const borderColorDefault =
                obj.default.strokes[0]?.color !== undefined
                  ? toColorOpacityString(
                      obj.default.strokes[0].color,
                      obj.default.strokes[0]?.opacity
                    )
                  : 'rgba(0, 0, 0, 0.00)'

              const fontColorHover = this.getColor(obj.hover.children[0])
              const backgroundColorHover = toColorString(obj.hover.backgroundColor)
              const borderColorHover =
                obj.hover.strokes[0]?.color !== undefined
                  ? toColorOpacityString(obj.hover.strokes[0].color, obj.hover.strokes[0]?.opacity)
                  : 'rgba(0, 0, 0, 0.00)'

              const fontColorPressed = this.getColor(obj.pressed.children[0])
              const backgroundColorPressed = toColorString(obj.pressed.backgroundColor)
              const borderColorPressed =
                obj.pressed.strokes[0]?.color !== undefined
                  ? toColorOpacityString(
                      obj.pressed.strokes[0].color,
                      obj.pressed.strokes[0]?.opacity
                    )
                  : 'rgba(0, 0, 0, 0.00)'

              const fontColorDisabled = this.getColor(obj.disabled.children[0])
              const backgroundColorDisabled = toColorString(obj.disabled.backgroundColor)
              const borderColorDisabled =
                obj.disabled.strokes[0]?.color !== undefined
                  ? toColorOpacityString(
                      obj.disabled.strokes[0].color,
                      obj.disabled.strokes[0]?.opacity
                    )
                  : 'rgba(0, 0, 0, 0.00)'

              return {
                default: {
                  background: backgroundColorDefault,
                  font: fontColorDefault,
                  border: borderColorDefault,
                },
                hover: {
                  background: backgroundColorHover,
                  font: fontColorHover,
                  border: borderColorHover,
                },
                pressed: {
                  background: backgroundColorPressed,
                  font: fontColorPressed,
                  border: borderColorPressed,
                },
                disabled: {
                  background: backgroundColorDisabled,
                  font: fontColorDisabled,
                  border: borderColorDisabled,
                },
              }
            }))
          .flat()
          .filter((item) => item !== undefined)

        buttonStates.push(...buttons)

        names.map((buttonItems: string[]) => {
          if (buttonItems !== undefined) {
            const trimItem = buttonItems.map((buttonName) => this.formatString(buttonName))

            buttonNames.push(...trimItem)
          }

          return []
        })
      }

      if (node.color && isColor(node.color)) {
        // node fills
        const color = toColorString(node.color)

        if (!colors[color]) {
          colors[color] = node.color
        }
      }
    })

    const colorsResult = Object.keys(colors)
      .sort((a, b) => toAverage(colors[b]) - toAverage(colors[a]))
      .reduce(
        (result, color) => ({
          ...result,
          [toColorName(color, Object.keys(result))]: color,
        }),
        {}
      )

    const buttonColorsResult = buttonNames.reduce(
      (result, name, index) => ({
        ...result,
        [name]: buttonStates[index],
      }),
      {}
    )

    return Object.keys(buttonColorsResult).length
      ? {
          ...colorsResult,
          button: {
            ...buttonColorsResult,
          },
        }
      : {
          ...colorsResult,
        }
  }

  generate(file: FileResponse): FigmaThemeGeneratorResult {
    const values = this.getColors(file.document.children)

    return {
      name: this.name,
      content: this.exportValuesTemplate('colors', values),
    }
  }
}
