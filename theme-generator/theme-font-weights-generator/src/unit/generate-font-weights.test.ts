import { FigmaTheme }                     from '@atls/figma-theme'

import path                               from 'path'
import { readFileSync }                   from 'fs'
import { mkdirSync }                      from 'fs'
import { existsSync }                     from 'fs'

import { FigmaThemeFontWeightsGenerator } from '../FigmaThemeFontWeightsGenerator'

describe('font weights generator', () => {
  const file = {
    document: {
      children: [
        {
          type: 'TEXT',
          style: {
            fontWeight: 100,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontWeight: 200,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontWeight: 500,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontWeight: 700,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontWeight: 800,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontWeight: 900,
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
    content: `export const fontWeights = {
    "thin": 100,
    "extraLight": 200,
    "medium": 500,
    "bold": 700,
    "extraBold": 800,
    "heavy": 900
}`,
    name: 'fontWeights',
  }

  const expectedCode = `export const fontWeights = {
  thin: 100,
  extraLight: 200,
  medium: 500,
  bold: 700,
  extraBold: 800,
  heavy: 900,
};
`

  const expectedEmptyObj = {
    content: `export const fontWeights = {}`,
    name: 'fontWeights',
  }

  const expectedEmptyFile = `export const fontWeights = {};
`

  it('should generate an object with name and content', () => {
    const generator = new FigmaThemeFontWeightsGenerator()
    // @ts-ignore
    const generated = generator.generate(file)

    expect(generated).toStrictEqual(expectedObj)
  })

  it('should generate an object with name and empty content', () => {
    const generator = new FigmaThemeFontWeightsGenerator()
    // @ts-ignore
    const generated = generator.generate(emptyFile)

    expect(generated).toStrictEqual(expectedEmptyObj)
  })

  it('should generate font weights empty file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      emptyFile,
      './theme/theme-font-weights-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/fontWeights.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedEmptyFile)
  })

  it('should generate font weights file', async () => {
    // @ts-ignore
    const generator = new FigmaTheme(
      // @ts-ignore
      file,
      './theme/theme-font-weights-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/fontWeights.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedCode)
  })
})
