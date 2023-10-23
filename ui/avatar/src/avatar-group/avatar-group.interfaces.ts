import { AvatarProps } from '../avatar.interfaces'

export interface AvatarGroupProps {
  avatars: string[]
  shape?: AvatarProps['shape']
  maxCount?: number
  size?: number
  borderSize?: number
  margin?: number
}
