import { Text } from 'figma-js'

export abstract class Strategy {
  abstract execute(textNodes: Text[]): any

  getStat(textNodes: Text[]): Map<number, number> {
    const stat = new Map<number, number>()

    textNodes.forEach((node) => {
      const fontSize = Math.round(node.style?.fontSize)

      stat.set(fontSize, (stat.get(fontSize) || 0) + 1)
    })

    return stat
  }
}
