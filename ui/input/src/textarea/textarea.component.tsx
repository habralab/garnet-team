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
import { InputElement }             from '../input.element'
import { InputProps }               from '../input.interfaces'
import { textareaElementStyles }    from './textarea.styles'
import { rawTextareaStyles }        from './textarea.styles'
import { textareaHeightStyles }     from './textarea.styles'

const TextareaElement = styled(InputElement)(textareaElementStyles, textareaHeightStyles)
const RawTextarea = styled(RawInput)(rawTextareaStyles)

const { rawInputProps } = createTextareaProps()

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
      <TextareaElement {...props} error={errorText !== ''}>
        <RawTextarea
          id={id}
          ref={ref}
          type={type}
          disabled={disabled}
          value={value}
          onChange={changeValue}
          {...rawInputProps}
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
      </TextareaElement>
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
