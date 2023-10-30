import { FigmaTheme }               from '@atls/figma-theme'

import path                         from 'path'
import { readFileSync }             from 'fs'
import { mkdirSync }                from 'fs'
import { existsSync }               from 'fs'

import { FigmaThemeRadiiGenerator } from '../FigmaThemeRadiiGenerator'

describe('radii generator', () => {
  const file = {
    document: {
      children: [
        {
          rectangleCornerRadii: [28, 28, 0, 0],
        },
        {
          rectangleCornerRadii: [0, 28, 0, 28],
        },
        {
          rectangleCornerRadii: [0, 0, 0, 28],
        },
        {
          cornerRadius: 54,
        },
        {
          cornerRadius: 100,
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
    content: `export const radii = {
    "f54": 54,
    "f100": 100,
    "tl28tr28": "28px 28px 0px 0px",
    "tr28br28": "0px 28px 0px 28px",
    "br28": "0px 0px 0px 28px"
}`,
    name: 'radii',
  }

  const expectedCode = `export const radii = {
  f54: 54,
  f100: 100,
  tl28tr28: "28px 28px 0px 0px",
  tr28br28: "0px 28px 0px 28px",
  br28: "0px 0px 0px 28px",
};
`

  const expectedEmptyObj = {
    content: `export const radii = {}`,
    name: 'radii',
  }

  const expectedEmptyFile = `export const radii = {};
`

  it('should generate an object with name and content', () => {
    const generator = new FigmaThemeRadiiGenerator()
    // @ts-ignore
    const generated = generator.generate(file)

    expect(generated).toStrictEqual(expectedObj)
  })

  it('should generate an object with name and empty content', () => {
    const generator = new FigmaThemeRadiiGenerator()
    // @ts-ignore
    const generated = generator.generate(emptyFile)

    expect(generated).toStrictEqual(expectedEmptyObj)
  })

  it('should generate radii empty file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      emptyFile,
      './theme/theme-radii-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/radii.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedEmptyFile)
  })

  it('should generate radii file', async () => {
    // @ts-ignore
    const generator = new FigmaTheme(
      // @ts-ignore
      file,
      './theme/theme-radii-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/radii.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedCode)
  })
})
