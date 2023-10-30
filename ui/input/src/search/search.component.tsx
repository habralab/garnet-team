import styled                       from '@emotion/styled'
import { RawInput }                 from '@atls-ui-parts/input'
import { useChangeValue }           from '@atls-ui-parts/input'

import React                        from 'react'
import { HTMLInputTypeAttribute }   from 'react'
import { ForwardRefRenderFunction } from 'react'
import { useState }                 from 'react'
import { forwardRef }               from 'react'
import { useRef }                   from 'react'
import { useLayer }                 from 'react-laag'

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
  showDropdown?: boolean
  options?: string[]
  onChangeOptions?: (option: string) => void
  value?: string
}

export const SearchWithoutRef: ForwardRefRenderFunction<HTMLInputElement, SearchProps> = (
  {
    value = '',
    showDropdown = true,
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

  const openMenu = () => setMenuOpen(true)
  const closeMenu = () => setMenuOpen(false)

  const { renderLayer, layerProps, triggerProps, triggerBounds } = useLayer({
    isOpen: menuOpen,
    placement: 'bottom-center',
    onOutsideClick: closeMenu,
    container: containerRef.current || undefined,
  })

  const changeValue = useChangeValue(disabled, onChange, onChangeNative)

  // eslint-disable-next-line
  if (!ref) ref = useRef(null)

  return (
    <Container ref={containerRef} type={type} onClick={() => (ref as any).current.focus()}>
      <InputElement
        {...triggerProps}
        {...props}
        onClick={openMenu}
        error={errorText !== ''}
        disabled={disabled}
      >
        <SearchIcon
          width={20}
          height={20}
          // @ts-ignore
          // eslint-disable-next-line react/destructuring-assignment
          color={ref?.current === document?.activeElement ? 'accent' : 'gray'}
        />
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
        <Condition match={Boolean(props.iconSvg)}>
          <IconAttachment
            iconSvg={props.iconSvg}
            valueWidth={props.valueWidth}
            valueHeight={props.valueHeight}
            onClick={props.onIconClick}
          />
        </Condition>
      </InputElement>
      <Condition match={menuOpen}>
        {renderLayer(
          <Box
            {...layerProps}
            style={{
              ...layerProps.style,
              position: 'static',
              display: showDropdown ? 'flex' : 'none',
              width: triggerBounds?.width || 600,
              zIndex: 2000,
            }}
          >
            <DropdownSkills
              options={options.filter((item) => item.toLowerCase().includes(value.toLowerCase()))}
              onChangeOption={onChangeOptions}
              onClick={closeMenu}
            />
          </Box>
        )}
      </Condition>
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
