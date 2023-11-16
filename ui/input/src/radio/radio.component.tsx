import { HiddenInput }     from '@atls-ui-parts/hidden-input'

import React               from 'react'
import { FC }              from 'react'

import { RadioButtonIcon } from '@ui/icon'
import { Box }             from '@ui/layout'
import { useHover }        from '@shared/utils'
import { theme }           from '@ui/theme'

import { InputRadioProps } from './radio.interfaces'

export const InputRadio: FC<InputRadioProps> = ({ value, disabled, onChange }) => {
  const [hover, hoverProps] = useHover()
  const onChangeHandler = () => onChange?.(!value)

  const getIconColors = () => {
    const { checked, disabled: disabledStyle, unchecked } = theme.colors.input.radio
    const type = hover ? 'hover' : 'default'

    if (disabled) {
      return {
        color: disabledStyle.background,
        stroke: disabledStyle.border,
        fill: disabledStyle.background,
      }
    }

    if (value) {
      return {
        color: checked[type].background,
        stroke: checked[type].border,
        fill: checked[type].border,
      }
    }

    return {
      color: unchecked[type].background,
      stroke: unchecked[type].border,
      fill: unchecked[type].background,
    }
  }

  return (
    <>
      <HiddenInput type='radio' checked={value} onChange={onChangeHandler} disabled={disabled} />
      <Box
        fill
        style={{ cursor: disabled ? 'not-allowed' : 'pointer' }}
        onClick={onChangeHandler}
        {...hoverProps}
      >
        <RadioButtonIcon width={20} height={20} {...getIconColors()} />
      </Box>
    </>
  )
}
