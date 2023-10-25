import styled                       from '@emotion/styled'
import { RawInput }                 from '@atls-ui-parts/input'
import { useChangeValue }           from '@atls-ui-parts/input'
import { createTextareaProps }      from '@atls-ui-parts/input'
import { useSelect }                from '@atls-ui-parts/select'

import React                        from 'react'
import { HTMLInputTypeAttribute }   from 'react'
import { ForwardRefRenderFunction } from 'react'
import { useState }                 from 'react'
import { forwardRef }               from 'react'
import { useRef }                   from 'react'
import { useIntl }                  from 'react-intl'

import { Condition }                from '@ui/condition'
import { Box }                      from '@ui/layout'
import { Layout }                   from '@ui/layout'
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

const Container = styled.div(({ type }: { type?: HTMLInputTypeAttribute }) => ({
  display: type === 'hidden' ? 'none' : 'flex',
  width: '100%',
  flexDirection: 'column',
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

  const handleChangeSearch = (searchValue: string) => setSearch(searchValue)
  const changeSearchValue = useChangeValue(disabled, handleChangeSearch, onChangeNative)

  const localizedSkills = mockSkills.map((item) => formatMessage({ id: item }))

  const { buttonProps, menuProps, renderMenu } = useSelect({
    items: localizedSkills,
    isOpen: menuOpen,
  })

  // eslint-disable-next-line
  if (!ref) ref = useRef(null)

  if (!Array.isArray(value)) return null

  const closeMenu = () => setMenuOpen(false)
  const openMenu = (e) => {
    if (e.target.nodeName !== 'svg' && e.target.nodeName !== 'path') {
      setMenuOpen(true)
    }
  }

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
    <Container ref={containerRef} type={type}>
      <InputElement
        {...containerProps}
        {...buttonProps}
        onClick={openMenu}
        {...props}
        error={errorText !== ''}
      >
        <Condition match={Array.isArray(value)}>
          <Row flexWrap='wrap' flex='auto' style={{ gap: 10 }}>
            {(value as string[]).map((tag) => (
              <Box>
                <Tag key={tag} onClick={() => handleRemoveTag(tag)} close>
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
                options={filteredItems}
                onChangeOption={handleAddTag}
                onClick={closeMenu}
              />
            </Box>
          )}
        </Condition>
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

export const Multiselect = forwardRef<HTMLInputElement, MultiselectProps>(MultiselectWithoutRef)
