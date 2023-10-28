import fs                  from 'fs-extra'
import fetch               from 'node-fetch'
import path                from 'path'
import { Node }            from 'figma-js'

import { FigmaFileLoader } from '@atls/figma-file-loader'

export class FigmaAssets {
  fileId: string

  node: Node

  output: string

  client: FigmaFileLoader = new FigmaFileLoader()

  constructor(fileId: string, node: Node, output) {
    this.fileId = fileId
    this.node = node

    this.output = path.join(process.cwd(), output || 'assets')
  }

  async loadImage(name, url) {
    const fileName = `${name.replace(/ /g, '-').replace(/_/g, '-')}.svg`
    const filePath = path.join(this.output, fileName)

    await fs.ensureDir(path.dirname(filePath))

    const response: any = await fetch(url)

    if (response.status !== 200) {
      return
    }

    const file = fs.createWriteStream(filePath)

    response.body.pipe(file)
  }

  async generate() {
    const { children } = this.node as any
    const items: any[] = []

    children
      .filter((item) => item.type === 'COMPONENT')
      .forEach((item) => {
        items.push({ id: item.id, name: item.name })
      })

    const images: any = await this.client.fileImages(
      this.fileId,
      items.map((item) => item.id)
    )

    await Promise.all(
      items.map(async (item) => {
        if (images[item.id]) {
          await this.loadImage(item.name, images[item.id])
        }
      })
    )
  }
}
