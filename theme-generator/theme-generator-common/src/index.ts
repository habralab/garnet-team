import { FileResponse } from 'figma-js'

export interface FigmaThemeGeneratorValues {
  name: string
  content: string
}

export type FigmaThemeGeneratorResult =
  | FigmaThemeGeneratorValues
  | Promise<FigmaThemeGeneratorValues>

export abstract class FigmaThemeGenerator {
  exportValuesTemplate(name: string, values: any): string {
    return `export const ${name} = ${JSON.stringify(values, null, 4)}`
  }

  abstract generate(file: FileResponse): FigmaThemeGeneratorResult
}
