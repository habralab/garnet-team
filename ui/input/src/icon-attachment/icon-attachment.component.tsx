import styled                  from '@emotion/styled'
import { ConditionalRender }   from '@atls-ui-parts/conditional-render'

import React                   from 'react'
import { useMemo }             from 'react'

import { Box }                 from '@ui/layout'

import { IconAttachmentProps } from './icon-attachment.interface'

const IconAttachment = ({ iconSvg, valueWidth, valueHeight, onClick }: IconAttachmentProps) => {
  const Attachment = useMemo(() => styled(ConditionalRender())(), [])

  return iconSvg !== undefined ? (
    <Attachment>
      <Box
        justifyContent='center'
        alignItems='center'
        style={{ cursor: 'pointer' }}
        width={valueWidth || 'auto'}
        height={valueHeight || 'auto'}
        onClick={onClick}
      >
        {iconSvg}
      </Box>
    </Attachment>
  ) : null
}

export { IconAttachment }
