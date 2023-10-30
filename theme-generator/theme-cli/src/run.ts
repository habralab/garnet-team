import { FigmaFileLoader } from '@atls/figma-file-loader'
import { FigmaTheme }      from '@atls/figma-theme'

import { FileResponse }    from 'figma-js'

export const run = async (fileId, output) => {
  const loader = new FigmaFileLoader()

  const file: FileResponse = await loader.load(fileId)

  const theme = new FigmaTheme(file, output)

  theme.generate()
}
