// eslint-disable-next-line no-shadow
export enum FontWeightNames {
  THIN = 'thin',
  EXTRA_LIGHT = 'extraLight',
  LIGHT = 'light',
  NORMAL = 'normal',
  MEDIUM = 'medium',
  SEMI_BOLD = 'semiBold',
  BOLD = 'bold',
  EXTRA_BOLD = 'extraBold',
  HEAVY = 'heavy',
}

interface FontWeight {
  weight: string
  value: number
}

export const FontWeights: FontWeight[] = [
  { weight: FontWeightNames.THIN, value: 100 },
  { weight: FontWeightNames.EXTRA_LIGHT, value: 200 },
  { weight: FontWeightNames.LIGHT, value: 300 },
  { weight: FontWeightNames.NORMAL, value: 400 },
  { weight: FontWeightNames.MEDIUM, value: 500 },
  { weight: FontWeightNames.SEMI_BOLD, value: 600 },
  { weight: FontWeightNames.BOLD, value: 700 },
  { weight: FontWeightNames.EXTRA_BOLD, value: 800 },
  { weight: FontWeightNames.HEAVY, value: 900 },
]
