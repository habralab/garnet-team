import styled                       from '@emotion/styled'
import { RawInput }                 from '@atls-ui-parts/input'
import { useChangeValue }           from '@atls-ui-parts/input'
import { createTextareaProps }      from '@atls-ui-parts/input'

import React                        from 'react'
import { HTMLInputTypeAttribute }   from 'react'
import { ForwardRefRenderFunction } from 'react'
import { forwardRef }               from 'react'
import { useRef }                   from 'react'

import { Condition }                from '@ui/condition'
import { Layout }                   from '@ui/layout'
import { Text }                     from '@ui/text'

import { IconAttachment }           from '../icon-attachment'
import { InputProps }               from '../input.interfaces'
import { transitionStyles }         from '../input.styles'
import { shapeStyles }              from '../input.styles'
import { baseStyles }               from '../input.styles'
import { appearanceStyles }         from '../input.styles'

const InputElement = styled.div(baseStyles, shapeStyles, appearanceStyles, transitionStyles, {
  padding: 16,
})

const { containerProps, rawInputProps } = createTextareaProps()

const Container = styled.div(({ type }: { type?: HTMLInputTypeAttribute }) => ({
  display: type === 'hidden' ? 'none' : 'flex',
  width: '100%',
  flexDirection: 'column',
}))

export const TextareaWithoutRef: ForwardRefRenderFunction<HTMLInputElement, InputProps> = (
  { value, type, disabled, id, errorText = '', onChange, onChangeNative, ...props },
  ref
) => {
  const changeValue = useChangeValue(disabled, onChange, onChangeNative)

  // eslint-disable-next-line
  if (!ref) ref = useRef(null)

  return (
    <Container type={type} onClick={() => (ref as any).current.focus()}>
      <InputElement {...containerProps} {...props} error={errorText !== ''}>
        <RawInput
          id={id}
          ref={ref}
          type={type}
          disabled={disabled}
          value={value}
          onChange={changeValue}
          {...rawInputProps}
          {...props}
          style={{ ...props.style, height: '100%' }}
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
      <Condition match={Boolean(errorText) && typeof errorText === 'string'}>
        <Layout flexBasis={5} />
        <Text color='text.error' fontSize='normal'>
          {errorText}
        </Text>
      </Condition>
      <Condition match={Boolean(errorText) && typeof errorText !== 'string'}>
        <Layout flexBasis={5} />
        {errorText}
      </Condition>
    </Container>
  )
}

export const Textarea = forwardRef<HTMLInputElement, InputProps>(TextareaWithoutRef)
