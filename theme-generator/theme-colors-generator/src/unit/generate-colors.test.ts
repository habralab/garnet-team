import path                          from 'path'
import { existsSync }                from 'fs'
import { mkdirSync }                 from 'fs'
import { readFileSync }              from 'fs'

import { FigmaTheme }                from '@atls/figma-theme'

import { FigmaThemeColorsGenerator } from '../FigmaThemeColorsGenerator'

describe('colors generator', () => {
  const file = {
    document: {
      children: [
        {
          children: [
            {
              color: {
                r: 0.07058823853731155,
                g: 0.07058823853731155,
                b: 0.07450980693101883,
                a: 1,
              },
            },
            {
              color: {
                r: 0.4124999940395355,
                g: 0.405349999666214,
                b: 0.3987500071525574,
                a: 1,
              },
            },
            {
              color: {
                r: 0.9450980424880981,
                g: 0.9176470637321472,
                b: 0.886274516582489,
                a: 1,
              },
            },
            {
              color: {
                r: 0.9666666388511658,
                g: 0.9478166699409485,
                b: 0.9304166436195374,
                a: 1,
              },
            },
          ],
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
    content: `export const colors = {
    "pampas": "rgb(246, 242, 237)",
    "whitelinen": "rgb(241, 234, 226)",
    "dimgray": "rgb(105, 103, 102)",
    "woodsmoke": "rgb(18, 18, 19)"
}`,
    name: 'colors',
  }

  const expectedCode = `export const colors = {
  pampas: "rgb(246, 242, 237)",
  whitelinen: "rgb(241, 234, 226)",
  dimgray: "rgb(105, 103, 102)",
  woodsmoke: "rgb(18, 18, 19)",
};
`

  const expectedEmptyObj = {
    content: `export const colors = {}`,
    name: 'colors',
  }

  const expectedEmptyFile = `export const colors = {};
`

  it('should generate an object with name and content', () => {
    const generator = new FigmaThemeColorsGenerator()
    // @ts-ignore
    const generated = generator.generate(file)

    expect(generated).toStrictEqual(expectedObj)
  })

  it('should generate an object with name and empty content', () => {
    const generator = new FigmaThemeColorsGenerator()
    // @ts-ignore
    const generated = generator.generate(emptyFile)

    expect(generated).toStrictEqual(expectedEmptyObj)
  })

  it('should generate colors empty file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      emptyFile,
      './theme/theme-colors-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/colors.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedEmptyFile)
  })

  it('should generate colors file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      file,
      './theme/theme-colors-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/colors.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedCode)
  })
})
