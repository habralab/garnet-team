import path                           from 'path'
import { readFileSync }               from 'fs'
import { mkdirSync }                  from 'fs'
import { existsSync }                 from 'fs'

import { FigmaTheme }                 from '@atls/figma-theme'

import { FigmaThemeShadowsGenerator } from '../FigmaThemeShadowsGenerator'

describe('shadows generator', () => {
  const file = {
    document: {
      children: [
        {
          type: 'INNER_SHADOW',
          offset: {
            x: 0,
            y: 0,
          },
          radius: 12,
          spread: 16,
          color: {
            r: 0,
            g: 0,
            b: 0,
            a: 0.07999999821186066,
          },
        },
        {
          type: 'DROP_SHADOW',
          offset: {
            x: 0,
            y: 0,
          },
          radius: 32,
          spread: undefined,
          color: {
            r: 0.11764705926179886,
            g: 0.125490203499794,
            b: 0.15294118225574493,
            a: 0.07999999821186066,
          },
        },
        {
          type: 'DROP_SHADOW',
          offset: {
            x: 0,
            y: 20,
          },
          radius: 12,
          spread: undefined,
          color: {
            r: 0,
            g: 0,
            b: 0,
            a: 0.11999999731779099,
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
    content: `export const shadows = {
    "black": "0px 0px 12px 16px rgba(0, 0, 0, 0.08)",
    "shark": "0px 0px 32px rgba(30, 32, 39, 0.08)",
    "codgray": "0px 20px 12px rgba(0, 0, 0, 0.12)"
}`,
    name: 'shadows',
  }

  const expectedCode = `export const shadows = {
  black: "0px 0px 12px 16px rgba(0, 0, 0, 0.08)",
  shark: "0px 0px 32px rgba(30, 32, 39, 0.08)",
  codgray: "0px 20px 12px rgba(0, 0, 0, 0.12)",
};
`

  const expectedEmptyObj = {
    content: `export const shadows = {}`,
    name: 'shadows',
  }

  const expectedEmptyFile = `export const shadows = {};
`

  it('should generate an object with name and content', () => {
    const generator = new FigmaThemeShadowsGenerator()
    // @ts-ignore
    const generated = generator.generate(file)

    expect(generated).toStrictEqual(expectedObj)
  })

  it('should generate an object with name and empty content', () => {
    const generator = new FigmaThemeShadowsGenerator()
    // @ts-ignore
    const generated = generator.generate(emptyFile)

    expect(generated).toStrictEqual(expectedEmptyObj)
  })

  it('should generate shadows empty file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      emptyFile,
      './theme/theme-shadows-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/shadows.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedEmptyFile)
  })

  it('should generate shadows file', async () => {
    // @ts-ignore
    const generator = new FigmaTheme(
      // @ts-ignore
      file,
      './theme/theme-shadows-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/shadows.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedCode)
  })
})
