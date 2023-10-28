import path                             from 'path'
import { existsSync }                   from 'fs'
import { mkdirSync }                    from 'fs'
import { readFileSync }                 from 'fs'

import { FigmaTheme }                   from '@atls/figma-theme'

import { FigmaThemeFontSizesGenerator } from '../FigmaThemeFontSizesGenerator'

describe('font sizes generator', () => {
  const file = {
    document: {
      children: [
        {
          type: 'TEXT',
          style: {
            fontSize: 12,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 13,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 15,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 16,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 13,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 18,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 20,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 28,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 31,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 32,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 36,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 40,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 44,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 48,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 60,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 64,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 72,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 74,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 80,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 84,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 94,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 103,
          },
        },
        {
          type: 'TEXT',
          style: {
            fontSize: 128,
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
    content: `export const fontSizes = {
    "small": {
        "semiDefault": 12,
        "default": 13
    },
    "normal": {
        "reduced": 15,
        "semiDefault": 16,
        "default": 18,
        "semiIncreased": 20
    },
    "medium": {
        "semiReduced": 28,
        "reduced": 31,
        "semiDefault": 32,
        "default": 36,
        "semiIncreased": 40,
        "increased": 44,
        "semiLarge": 48
    },
    "large": {
        "small": 60,
        "semiReduced": 64,
        "reduced": 72,
        "semiDefault": 74,
        "default": 80,
        "semiIncreased": 84,
        "increased": 94,
        "semiLarge": 103,
        "large": 128
    }
}`,
    name: 'fontSizes',
  }

  const expectedCode = `export const fontSizes = {
  small: {
    semiDefault: 12,
    default: 13,
  },
  normal: {
    reduced: 15,
    semiDefault: 16,
    default: 18,
    semiIncreased: 20,
  },
  medium: {
    semiReduced: 28,
    reduced: 31,
    semiDefault: 32,
    default: 36,
    semiIncreased: 40,
    increased: 44,
    semiLarge: 48,
  },
  large: {
    small: 60,
    semiReduced: 64,
    reduced: 72,
    semiDefault: 74,
    default: 80,
    semiIncreased: 84,
    increased: 94,
    semiLarge: 103,
    large: 128,
  },
};
`

  const expectedEmptyObj = {
    content: `export const fontSizes = {
    "small": {},
    "normal": {},
    "medium": {},
    "large": {}
}`,
    name: 'fontSizes',
  }

  const expectedEmptyFile = `export const fontSizes = {
  small: {},
  normal: {},
  medium: {},
  large: {},
};
`

  it('should generate an object with name and content', () => {
    const generator = new FigmaThemeFontSizesGenerator()
    // @ts-ignore
    const generated = generator.generate(file)

    expect(generated).toStrictEqual(expectedObj)
  })

  it('should generate an object with name and empty content', () => {
    const generator = new FigmaThemeFontSizesGenerator()
    // @ts-ignore
    const generated = generator.generate(emptyFile)

    expect(generated).toStrictEqual(expectedEmptyObj)
  })

  it('should generate font sizes empty file', async () => {
    const generator = new FigmaTheme(
      // @ts-ignore
      emptyFile,
      './theme/theme-font-sizes-generator/src/unit/generated'
    )

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/fontSizes.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedEmptyFile)
  })

  it('should generate font sizes file', async () => {
    // @ts-ignore
    const generator = new FigmaTheme(file, './theme/theme-font-sizes-generator/src/unit/generated')

    if (!existsSync(`${__dirname}/generated`)) mkdirSync(`${__dirname}/generated`)

    await generator.generate()

    const config = path.join(__dirname, '/generated/fontSizes.ts')

    const code = await readFileSync(config)

    expect(Buffer.from(code).toString()).toBe(expectedCode)
  })
})
