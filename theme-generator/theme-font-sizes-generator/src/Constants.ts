// eslint-disable-next-line no-shadow
export enum Group {
  SMALL = 'small',
  NORMAL = 'normal',
  MEDIUM = 'medium',
  LARGE = 'large',
}

// eslint-disable-next-line no-shadow
export enum FontSize {
  SemiTiny = 'semiTiny',
  Tiny = 'tiny',
  SemiSmall = 'semiSmall',
  Small = 'small',
  SemiReduced = 'semiReduced',
  Reduced = 'reduced',
  SemiDefault = 'semiDefault',

  Default = 'default',

  SemiIncreased = 'semiIncreased',
  Increased = 'increased',
  SemiLarge = 'semiLarge',
  Large = 'large',
  SemiHuge = 'semiHuge',
  Huge = 'huge',
  SemiGiant = 'semiGiant',
  Giant = 'giant',
}

export const FontSizeDefaultName = FontSize.Default

export const groupNamesLessThanDefault: Array<string> = [
  FontSize.SemiTiny,
  FontSize.Tiny,
  FontSize.SemiSmall,
  FontSize.Small,
  FontSize.SemiReduced,
  FontSize.Reduced,
  FontSize.SemiDefault,
]

export const groupNamesGreaterThanDefault: Array<string> = [
  FontSize.Giant,
  FontSize.SemiGiant,
  FontSize.Huge,
  FontSize.SemiHuge,
  FontSize.Large,
  FontSize.SemiLarge,
  FontSize.Increased,
  FontSize.SemiIncreased,
]
