import path                         from 'path'
import { readFileSync }             from 'fs'
import { mkdirSync }                from 'fs'
import { existsSync }               from 'fs'

import { FigmaTheme }               from '@atls/figma-theme'

import { FigmaThemeFontsGenerator } from '../FigmaThemeFontsGenerator'

describe('fonts generator', () => {
  const file = {
    document: {
      children: [
        {
          type: 'TEXT',
          style: {
            fontFamily: 'Inter',
          },
        },
        {
          type: 'TEXT',
          style: {
            fontFamily: 'Inter',
          },
        },
        {
          type: 'TEXT',
          style: {
            fontFamily: 'Inter',
          },
        },
        {
          type: 'TEXT',
          style: {
            fontFamily: 'Roboto',
          },
        },
        {
          type: 'TEXT',
          style: {
            fontFamily: 'Roboto',
          },
        },
        {
          type: 'TEXT',
          style: {
            fontFamily: 'Helvetica',
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
    content: `export const fonts = {
    "primary": "Inter",
    "secondary": "Roboto",
    "tertiary": "Helvetica"
}`,
    name: 'fonts',
  }

  const expectedCode = `export const fonts = {
  primary: "Inter",
  secondary: "Roboto",
  tertiary: "Helvetica",
};
`

  const expectedEmptyObj = {
    content: `export const fonts = {}`,
    name: 'fonts',
  }

  const expectedEmptyFile = `export const fonts = {};
`

  it('should generate an object with name and content', () => {
    const generator = new FigmaThemeFontsGenerator()
    // @ts-ignore
    const generated = generator.generate(file)

    expect(generated).toStrictEqual(expectedObj)
  })

  it('should generate an object with name and empty content', () => {
    const generator = new FigmaThemeFontsGenerator()
    // @ts-ignore
    const generated = generator.generate(emptyFile)

    expect(generated).toStrictEqual(expectedEmptyObj)
  })

  it('should generate fonts empty file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      emptyFile,
      './theme/theme-fonts-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/fonts.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedEmptyFile)
  })

  it('should generate fonts file', async () => {
    // @ts-ignore
    const generator = new FigmaTheme(
      // @ts-ignore
      file,
      './theme/theme-fonts-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/fonts.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedCode)
  })
})
