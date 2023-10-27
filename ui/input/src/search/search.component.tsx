import styled                       from '@emotion/styled'
import { RawInput }                 from '@atls-ui-parts/input'
import { useChangeValue }           from '@atls-ui-parts/input'
import { useSelect }                from '@atls-ui-parts/select'

import React                        from 'react'
import { HTMLInputTypeAttribute }   from 'react'
import { ForwardRefRenderFunction } from 'react'
import { useState }                 from 'react'
import { forwardRef }               from 'react'
import { useRef }                   from 'react'

import { Condition }                from '@ui/condition'
import { SearchIcon }               from '@ui/icon'
import { Box }                      from '@ui/layout'
import { Layout }                   from '@ui/layout'
import { Column }                   from '@ui/layout'
import { Text }                     from '@ui/text'

import { DropdownSkills }           from '../dropdown-skills'
import { IconAttachment }           from '../icon-attachment'
import { InputProps }               from '../input.interfaces'
import { transitionStyles }         from '../input.styles'
import { shapeStyles }              from '../input.styles'
import { baseStyles }               from '../input.styles'
import { appearanceStyles }         from '../input.styles'

const InputElement = styled.div(baseStyles, shapeStyles, appearanceStyles, transitionStyles)

const Container = styled(Column)(({ type }: { type?: HTMLInputTypeAttribute }) => ({
  display: type === 'hidden' ? 'none' : 'flex',
  width: '100%',
}))

type SearchProps = InputProps & {
  options?: string[]
  onChangeOptions?: (option: string) => void
  value?: string
}

export const SearchWithoutRef: ForwardRefRenderFunction<HTMLInputElement, SearchProps> = (
  {
    value = '',
    options = [],
    type,
    disabled,
    id,
    errorText = '',
    onChange,
    onChangeOptions,
    onChangeNative,
    ...props
  },
  ref
) => {
  const [menuOpen, setMenuOpen] = useState(false)
  const containerRef = useRef<HTMLDivElement>(null)

  const { buttonProps, menuProps, renderMenu } = useSelect({
    items: options,
    isOpen: menuOpen,
  })

  const changeValue = useChangeValue(disabled, onChange, onChangeNative)

  // eslint-disable-next-line
  if (!ref) ref = useRef(null)

  const closeMenu = () => setMenuOpen(false)
  const openMenu = (e) => {
    if (e.target.nodeName !== 'svg' && e.target.nodeName !== 'path') {
      setMenuOpen(true)
    }
  }

  return (
    <Container ref={containerRef} type={type}>
      <InputElement
        {...buttonProps}
        {...props}
        onClick={openMenu}
        error={errorText !== ''}
        disabled={disabled}
      >
        <SearchIcon width={20} height={20} color='gray' />
        <Layout flexBasis={10} flexShrink={0} />
        <RawInput
          id={id}
          ref={ref}
          type={type}
          disabled={disabled}
          value={value}
          onChange={changeValue}
          {...props}
        />
        {renderMenu(
          <Box
            {...menuProps}
            style={{
              ...menuProps.style,
              // @ts-ignore
              width: containerRef?.current?.offsetWidth || 600,
              zIndex: 2000,
            }}
          >
            <DropdownSkills
              // @ts-ignore
              options={options.filter((item) => item.toLowerCase().includes(value.toLowerCase()))}
              onChangeOption={onChangeOptions}
              onClick={closeMenu}
            />
          </Box>
        )}
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

export const Search = forwardRef<HTMLInputElement, SearchProps>(SearchWithoutRef)
