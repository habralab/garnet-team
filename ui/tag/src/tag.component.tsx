import styled                  from '@emotion/styled'

import React                   from 'react'
import { FC }                  from 'react'

import { Condition }           from '@ui/condition'
import { ClearIcon }           from '@ui/icon'
import { Box }                 from '@ui/layout'
import { BoxProps }            from '@ui/layout'
import { Text }                from '@ui/text'

import { TagProps }            from './tag.interfaces'
import { appearanceStyles }    from './tag.styles'
import { hoverStyles }         from './tag.styles'
import { containerBaseStyles } from './tag.styles'

const TagElement = styled(Box)<BoxProps & { variant: TagProps['variant']; hover: boolean }>(
  containerBaseStyles,
  appearanceStyles,
  hoverStyles
)

export const Tag: FC<TagProps> = ({ children, variant = 'primary', close, onClick }) => (
  <TagElement
    variant={variant}
    style={{ cursor: onClick ? 'pointer' : 'default' }}
    hover={Boolean(onClick)}
    onClick={onClick}
  >
    <Text fontSize='normal' color='text.darkBlue'>
      {children}
    </Text>
    <Condition match={Boolean(close)}>
      <ClearIcon width={18} height={18} color='darkBlue' />
    </Condition>
  </TagElement>
)
