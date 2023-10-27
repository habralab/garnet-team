import { User }            from '@shared/data'
import { UserWithRequest } from '@shared/data'

export interface ProfileParticipantsProps {
  participants?: User[]
  ownerUser?: User
  isMyTeam?: boolean
  invitedParticipants?: UserWithRequest[]
  applicationParticipants?: UserWithRequest[]
}
