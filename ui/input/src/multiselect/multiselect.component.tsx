import styled                       from '@emotion/styled'
import { RawInput }                 from '@atls-ui-parts/input'
import { useChangeValue }           from '@atls-ui-parts/input'
import { createTextareaProps }      from '@atls-ui-parts/input'

import React                        from 'react'
import { HTMLInputTypeAttribute }   from 'react'
import { ForwardRefRenderFunction } from 'react'
import { useState }                 from 'react'
import { forwardRef }               from 'react'
import { useRef }                   from 'react'
import { useIntl }                  from 'react-intl'
import { useLayer }                 from 'react-laag'

import { Condition }                from '@ui/condition'
import { Box }                      from '@ui/layout'
import { Layout }                   from '@ui/layout'
import { Column }                   from '@ui/layout'
import { Row }                      from '@ui/layout'
import { Tag }                      from '@ui/tag'
import { Text }                     from '@ui/text'

import { DropdownSkills }           from '../dropdown-skills'
import { IconAttachment }           from '../icon-attachment'
import { InputProps }               from '../input.interfaces'
import { transitionStyles }         from '../input.styles'
import { shapeStyles }              from '../input.styles'
import { baseStyles }               from '../input.styles'
import { appearanceStyles }         from '../input.styles'
import { mockSkills }               from './data'

const InputElement = styled.div(baseStyles, shapeStyles, appearanceStyles, transitionStyles, {
  padding: 16,
})

const { containerProps } = createTextareaProps()

const Container = styled(Column)(({ type }: { type?: HTMLInputTypeAttribute }) => ({
  display: type === 'hidden' ? 'none' : 'flex',
  width: '100%',
}))

type MultiselectProps = InputProps & { onChange: (values: string[]) => void }

export const MultiselectWithoutRef: ForwardRefRenderFunction<HTMLInputElement, MultiselectProps> = (
  { value, type, disabled, id, errorText = '', onChange, onChangeNative, ...props },
  ref
) => {
  const [search, setSearch] = useState('')
  const [menuOpen, setMenuOpen] = useState(false)
  const containerRef = useRef<HTMLDivElement>(null)

  const { formatMessage } = useIntl()

  const openMenu = () => setMenuOpen(true)
  const closeMenu = () => setMenuOpen(false)

  const handleChangeSearch = (searchValue: string) => setSearch(searchValue)
  const changeSearchValue = useChangeValue(disabled, handleChangeSearch, onChangeNative)

  const localizedSkills = mockSkills.map((item) => formatMessage({ id: item }))

  const { renderLayer, layerProps, triggerProps } = useLayer({
    isOpen: menuOpen,
    placement: 'bottom-center',
    onOutsideClick: closeMenu,
  })

  // eslint-disable-next-line
  if (!ref) ref = useRef(null)

  if (!Array.isArray(value)) return null

  const handleRemoveTag = (removedValue: string) => {
    onChange?.(value.filter((item) => item !== removedValue))
  }

  const handleAddTag = (newValue: string) => {
    onChange?.(value.concat([newValue]))
  }

  const filteredItems = localizedSkills.filter(
    (item) => !value.includes(item) && item.toLowerCase().includes(search.toLowerCase())
  )

  return (
    <Container ref={containerRef} type={type} onClick={() => (ref as any).current.focus()}>
      <InputElement
        {...containerProps}
        {...triggerProps}
        {...props}
        onClick={openMenu}
        error={errorText !== ''}
        disabled={disabled}
      >
        <Row flexWrap='wrap' flex='auto' style={{ gap: 10 }}>
          {(value as string[]).map((tag) => (
            <Box key={tag}>
              <Tag onClick={() => handleRemoveTag(tag)} close>
                {tag}
              </Tag>
            </Box>
          ))}
          <RawInput
            id={id}
            ref={ref}
            type={type}
            disabled={disabled}
            onChange={changeSearchValue}
            value={search}
            {...props}
            style={{ ...props.style, height: '100%' }}
          />
        </Row>
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
              // @ts-ignore
              width: containerRef?.current?.offsetWidth || 600,
              zIndex: 2000,
            }}
          >
            <DropdownSkills
              options={filteredItems}
              onChangeOption={handleAddTag}
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

export const Multiselect = forwardRef<HTMLInputElement, MultiselectProps>(MultiselectWithoutRef)
