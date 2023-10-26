import React           from 'react'
import { FC }          from 'react'
import { useRouter }   from 'next/router'

import { Avatar }      from '@ui/avatar'
import { AvatarGroup } from '@ui/avatar'
import { Background }  from '@ui/background'
import { Condition }   from '@ui/condition'
import { UserIcon }    from '@ui/icon'
import { Box }         from '@ui/layout'
import { Column }      from '@ui/layout'
import { Row }         from '@ui/layout'
import { Layout }      from '@ui/layout'
import { Link }        from '@ui/link'
import { Text }        from '@ui/text'
import { useHover }    from '@ui/utils'

import { CardProps }   from './card.interfaces'
import { sizeConfig }  from './card.config'

export const CardComponent: FC<CardProps> = ({
  name,
  cardSize = 'large',
  avatarUrl,
  countPeopleWord,
  countItemsWord,
  countItems = 0,
  itemsAvatars = [],
  itemsAvatarsShape = 'circle',
  itemsIcon,
}) => {
  const [hover, hoverProps] = useHover()

  return (
    <Box position='relative' title={name} style={{ cursor: 'pointer' }} {...hoverProps}>
      <Avatar title={name} image={avatarUrl} shape='square' size={sizeConfig[cardSize].size} />
      <Condition match={!hover && countItems > 0 && sizeConfig[cardSize].avatarGroupShow}>
        <Box position='absolute' bottom={20} left={20} borderRadius='medium' height={50}>
          <AvatarGroup
            shape={itemsAvatarsShape}
            avatars={itemsAvatars}
            maxCount={itemsAvatars.length < 3 ? itemsAvatars.length : 3}
          />
        </Box>
      </Condition>
      <Condition match={hover}>
        <Background
          position='absolute'
          top={0}
          bottom={0}
          left={0}
          right={0}
          color='accentPressedTransparent'
          borderRadius='medium'
        >
          <Column fill justifyContent='center' alignItems='center'>
            <Text
              fontSize={sizeConfig[cardSize].fontSize}
              width={sizeConfig[cardSize].size - sizeConfig[cardSize].statisticGap}
              fontWeight='bold'
              textAlign='center'
              color='text.white'
              overflow='hidden'
              whiteSpace={cardSize === 'large' ? 'wrap' : 'nowrap'}
              textOverflow='ellipsis'
              style={{ justifyContent: 'center' }}
            >
              {name}
            </Text>
            <Layout flexBasis={20} flexShrink={0} />
            <Row
              justifyContent='center'
              flexWrap='wrap'
              paddingX={`${sizeConfig[cardSize].statisticGap}px`}
              style={{ gap: sizeConfig[cardSize].statisticGap }}
            >
              <Box alignItems='center'>
                <Background
                  width={20}
                  height={20}
                  color='white'
                  alignItems='center'
                  justifyContent='center'
                  borderRadius='half'
                >
                  {itemsIcon}
                </Background>
                <Layout width={8} />
                <Text fontSize='normal' color='text.white'>
                  {countItemsWord}
                </Text>
              </Box>
              <Box alignItems='center'>
                <Background
                  width={20}
                  height={20}
                  color='white'
                  alignItems='center'
                  justifyContent='center'
                  borderRadius='half'
                >
                  <UserIcon width={14} height={14} color='accentPressed' />
                </Background>
                <Layout width={8} />
                <Text fontSize='normal' color='text.white'>
                  {countPeopleWord}
                </Text>
              </Box>
            </Row>
          </Column>
        </Background>
      </Condition>
    </Box>
  )
}

export const Card: FC<CardProps> = ({ url, ...props }) => {
  const router = useRouter()

  if (url) {
    const handleClick = (event) => {
      event.preventDefault()

      router.push(url)
    }

    return (
      <Link href={url} onClick={handleClick}>
        <CardComponent {...props} />
      </Link>
    )
  }

  return <CardComponent {...props} />
}
