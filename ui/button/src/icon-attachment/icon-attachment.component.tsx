import styled                  from '@emotion/styled'
import { ConditionalRender }   from '@atls-ui-parts/conditional-render'

import React                   from 'react'
import { useMemo }             from 'react'

import { Box }                 from '@ui/layout'

import { IconAttachmentProps } from './icon-attachment.interface'

const IconAttachment = ({ iconSvg, valueWidth, valueHeight }: IconAttachmentProps) => {
  const Attachment = useMemo(() => styled(ConditionalRender())(), [])

  return iconSvg !== undefined ? (
    <Attachment>
      <Box
        justifyContent='center'
        alignItems='center'
        width={valueWidth || 'auto'}
        height={valueHeight || 'auto'}
      >
        {iconSvg}
      </Box>
    </Attachment>
  ) : null
}

export { IconAttachment }
