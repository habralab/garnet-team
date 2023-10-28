import namer     from 'color-namer'
import { Color } from 'figma-js'

export const isColor = (node: any): node is Color => node.r && node.g && node.b && node.a

export const toAverage = (node: Color) => ((node.r + node.g + node.b) / 3) * node.a

export const normalizeChannel = (num: number) => Math.round(num * 255)

export const clearStringOfSpecialChars = (str: string) => str.replace(/[^a-zа-яё0-9]/gi, ' ')

export const toColorString = (node: Color) => {
  const r = normalizeChannel(node.r)
  const g = normalizeChannel(node.g)
  const b = normalizeChannel(node.b)

  if (node.a === 1) {
    return `rgb(${r}, ${g}, ${b})`
  }

  return `rgba(${r}, ${g}, ${b}, ${node.a.toFixed(2)})`
}

export const toColorOpacityString = (node: Color, opacity: number) => {
  const r = normalizeChannel(node.r)
  const g = normalizeChannel(node.g)
  const b = normalizeChannel(node.b)

  return `rgba(${r}, ${g}, ${b}, ${opacity ? opacity.toFixed(2) : 1})`
}

const namespaces = ['basic', 'html', 'pantone', 'ntc', 'x11', 'roygbiv']

const formatColorName = (name: string) => name.replace(/ /g, '').toLowerCase()

export const toColorName = (color: string, skip: string[] = []): string => {
  try {
    const names = namer(color)

    const [appropriate] = namespaces
      .map((namespace) => {
        const [item] = names[namespace].filter(
          (current) => !skip.includes(formatColorName(current.name))
        )

        return item
      })
      .filter(Boolean)
      .sort((a, b) => a.distance - b.distance)

    if (appropriate) {
      return formatColorName(appropriate.name)
    }

    return color
  } catch (error) {
    return color
  }
}
