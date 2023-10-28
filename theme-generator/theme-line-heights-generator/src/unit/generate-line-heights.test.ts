import path                               from 'path'
import { existsSync }                     from 'fs'
import { mkdirSync }                      from 'fs'
import { readFileSync }                   from 'fs'

import { FigmaTheme }                     from '@atls/figma-theme'

import { FigmaThemeLineHeightsGenerator } from '../FigmaThemeLineHeightsGenerator'

describe('lineHeights generator', () => {
  const file = {
    document: {
      children: [
        {
          type: 'TEXT',
          style: {
            fontSize: 16,
            lineHeightPx: 28,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 44,
            lineHeightPx: 52,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 15,
            lineHeightPx: 18,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 16,
            lineHeightPx: 28,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 44,
            lineHeightPx: 52,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 64,
            lineHeightPx: 76,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 20,
            lineHeightPx: 28,
          },
        },
      ],
    },
  }

  const emptyFile = {
    document: {
      children: [],
    },
  }

  const expectedObj = {
    content: `export const lineHeights = {
    "compact": 1.2,
    "extraCompact": 1.4,
    "default": 1.8
}`,
    name: 'lineHeights',
  }

  const expectedCode = `export const lineHeights = {
  compact: 1.2,
  extraCompact: 1.4,
  default: 1.8,
};
`

  const expectedEmptyObj = {
    content: `export const lineHeights = {}`,
    name: 'lineHeights',
  }

  const expectedEmptyFile = `export const lineHeights = {};
`

  it('should generate an object with name and content', () => {
    const generator = new FigmaThemeLineHeightsGenerator()
    // @ts-ignore
    const generated = generator.generate(file)

    expect(generated).toStrictEqual(expectedObj)
  })

  it('should generate an object with name and empty content', () => {
    const generator = new FigmaThemeLineHeightsGenerator()
    // @ts-ignore
    const generated = generator.generate(emptyFile)

    expect(generated).toStrictEqual(expectedEmptyObj)
  })

  it('should generate lineHeights empty file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      emptyFile,
      './theme/theme-line-heights-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/lineHeights.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedEmptyFile)
  })

  it('should generate lineHeights file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      file,
      './theme/theme-line-heights-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/lineHeights.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedCode)
  })
})
