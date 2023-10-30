import styled               from '@emotion/styled'
import { Content }          from '@atls-ui-parts/button'

import React                from 'react'
import { forwardRef }       from 'react'
import { useState }         from 'react'

import { useHover }         from '@ui/utils'

import { ButtonProps }      from './button.interfaces'
import { IconAttachment }   from './icon-attachment'
import { getBaseStyles }    from './styles'
import { contentStyles }    from './styles'
import { shapeStyles }      from './styles'
import { fillStyles }       from './styles'
import { appearanceStyles } from './styles'

const ButtonElement = styled('button')<any>(
  getBaseStyles,
  contentStyles,
  shapeStyles,
  appearanceStyles,
  fillStyles
)

export const Button = forwardRef<HTMLElement, ButtonProps>((
  { children, active, fill, ...props },
  ref
) => {
  const [hover, hoverProps] = useHover()
  const [pressed, setPressed] = useState<boolean>(false)

  if (props.horizontalLocation === 'right') {
    return (
      <ButtonElement
        hover={hover}
        pressed={pressed || active}
        onMouseDown={() => setPressed(true)}
        onMouseUp={() => setPressed(false)}
        withIcon
        fill={fill}
        {...hoverProps}
        {...props}
        ref={ref}
      >
        <Content divider={4}>
          {children}
          <IconAttachment
            iconSvg={props.iconSvg}
            valueWidth={props.valueWidth}
            valueHeight={props.valueHeight}
          />
        </Content>
      </ButtonElement>
    )
  }
  if (props.horizontalLocation === 'left') {
    return (
      <ButtonElement
        hover={hover}
        pressed={pressed || active}
        onMouseDown={() => setPressed(true)}
        onMouseUp={() => setPressed(false)}
        withIcon
        {...hoverProps}
        {...props}
        ref={ref}
      >
        <Content divider={4}>
          <IconAttachment
            iconSvg={props.iconSvg}
            valueWidth={props.valueWidth}
            valueHeight={props.valueHeight}
          />
          {children}
        </Content>
      </ButtonElement>
    )
  }
  return (
    <ButtonElement
      hover={hover}
      pressed={pressed || active}
      onMouseDown={() => setPressed(true)}
      onMouseUp={() => setPressed(false)}
      {...hoverProps}
      {...props}
      ref={ref}
    >
      <Content divider={4}>{children}</Content>
    </ButtonElement>
  )
})
