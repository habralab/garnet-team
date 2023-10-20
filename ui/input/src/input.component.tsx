import styled             from '@emotion/styled'
import { RawInput }       from '@atls-ui-parts/input'
import { useChangeValue } from '@atls-ui-parts/input'

import React                        from 'react'
import { ForwardRefRenderFunction } from 'react'
import { forwardRef }               from 'react'
import { useRef }                   from 'react'

import { Condition } from '@ui/condition'
import { Text }      from '@ui/text'

import { IconAttachment }   from './icon-attachment'
import { InputProps }       from './input.interfaces'
import { transitionStyles } from './styles'
import { shapeStyles }      from './styles'
import { baseStyles }       from './styles'
import { appearanceStyles } from './styles'

export const InputElement = styled.div<any>(
  baseStyles,
  shapeStyles,
  appearanceStyles,
  transitionStyles
)

const Container = styled.div(({ type }: any) => ({
  display: type === 'hidden' ? 'none' : 'flex',
  width: '100%',
  flexDirection: 'column',
}))

const Layout = styled.div(({ flexBasis }: any) => ({
  display: 'flex',
  flexBasis,
}))

export const InputWithoutRef: ForwardRefRenderFunction<HTMLInputElement, InputProps> = (
  { value, type, disabled, errorText = '', onChange, onChangeNative, ...props },
  ref
) => {
  const changeValue = useChangeValue(disabled, onChange, onChangeNative)

  // eslint-disable-next-line
  if (!ref) ref = useRef(null)

  return (
    <Container type={type} onClick={() => (ref as any).current.focus()}>
      <InputElement {...props} error={errorText !== ''}>
        <RawInput
          ref={ref}
          type={type}
          disabled={disabled}
          value={value}
          onChange={changeValue}
          {...props}
        />
        <Condition match={Boolean(props.iconSvg)}>
          <IconAttachment
            iconSvg={props.iconSvg}
            valueWidth={props.valueWidth}
            valueHeight={props.valueHeight}
            onClick={props.onIconClick}
          />
        </Condition>
      </InputElement>
      <Layout flexBasis={5}/>
      <Condition match={typeof errorText === 'string'}>
        <Text color='text.error' fontSize={'normal'}>
          {errorText}
        </Text>
      </Condition>
      <Condition match={Boolean(errorText) && typeof errorText !== 'string'}>
        {errorText}
      </Condition>
    </Container>
  )
}

export const Input = forwardRef<HTMLInputElement, InputProps>(InputWithoutRef)
