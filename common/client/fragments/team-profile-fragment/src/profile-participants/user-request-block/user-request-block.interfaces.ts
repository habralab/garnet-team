import { UserWithRequest } from '@shared/data'

export interface UserRequestBlockProps {
  user: UserWithRequest
  size?: 'large' | 'normal'
}
