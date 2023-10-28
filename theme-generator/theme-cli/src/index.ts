import program from 'commander'
import logger  from 'npmlog'

import { run } from './run'

logger.heading = 'figma-theme' as string
;(program as any)
  .option('-o, --output [output]', 'Output dir')
  .option('-v, --verbose', 'Verbose output')
  .arguments('<fileId>')
  .parse(process.argv)

if ((program as any).verbose) {
  logger.level = 'verbose'
}

const [fileId] = (program as any).args

if (!fileId) {
  logger.error('Figma file id required.')
} else {
  run(fileId, (program as any).output)
    .then(() => logger.info('Theme successful generated'))
    .catch((error) => logger.error(error.message))
}
