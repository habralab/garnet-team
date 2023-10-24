import React                from 'react'
import { FC }               from 'react'

import { Avatar }           from '@ui/avatar'
import { Background }       from '@ui/background'
import { Condition }        from '@ui/condition'
import { Text }             from '@ui/text'

import { AvatarGroupProps } from './avatar-group.interfaces'

export const AvatarGroup: FC<AvatarGroupProps> = ({
  avatars = [],
  shape = 'circle',
  maxCount = 3,
  size = 50,
  borderSize = 4,
  margin = 20,
}) => (
  <>
    {Array.from({ length: avatars.length <= maxCount ? maxCount : maxCount }, (_, index) => (
      <Background
        key={index}
        height={size}
        width={size}
        color='white'
        borderRadius={shape === 'circle' ? 'half' : 'medium'}
        justifyContent='center'
        alignItems='center'
        style={{ transform: `translateX(-${margin * index}px)` }}
      >
        <Avatar image={avatars[index]} shape={shape} size={size - borderSize * 2} />
      </Background>
    ))}
    <Condition match={avatars.length > maxCount}>
      <Background
        height={size}
        width={size}
        color='white'
        borderRadius={shape === 'circle' ? 'half' : 'medium'}
        justifyContent='center'
        alignItems='center'
        style={{ transform: `translateX(-${maxCount * margin}px)` }}
      >
        <Text fontSize='normal' color='text.gray'>
          {avatars.length}+
        </Text>
      </Background>
    </Condition>
  </>
)
