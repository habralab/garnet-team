import styled                  from '@emotion/styled'

import React                   from 'react'
import { FC }                  from 'react'

import { Condition }           from '@ui/condition'
import { ClearIcon }           from '@ui/icon'
import { Box }                 from '@ui/layout'
import { Text }                from '@ui/text'

import { TagElementProps }     from './tag.interfaces'
import { TagProps }            from './tag.interfaces'
import { appearanceStyles }    from './tag.styles'
import { shapeStyles }         from './tag.styles'
import { hoverStyles }         from './tag.styles'
import { containerBaseStyles } from './tag.styles'

const TagElement = styled(Box)<TagElementProps>(
  containerBaseStyles,
  appearanceStyles,
  shapeStyles,
  hoverStyles
)

export const Tag: FC<TagProps> = ({
  children,
  variant = 'primary',
  size = 'normal',
  close,
  onClick,
}) => (
  <TagElement
    variant={variant}
    size={size}
    style={{ cursor: onClick ? 'pointer' : 'default' }}
    hover={Boolean(onClick)}
    onClick={onClick}
  >
    <Text
      fontSize='normal'
      color='text.darkBlue'
      overflow='hidden'
      whiteSpace='nowrap'
      textOverflow='ellipsis'
    >
      {children}
    </Text>
    <Condition match={Boolean(close)}>
      <ClearIcon width={18} height={18} color='darkBlue' />
    </Condition>
  </TagElement>
)
