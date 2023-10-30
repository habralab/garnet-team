import { Team }            from '@shared/data'
import { User }            from '@shared/data'
import { UserWithRequest } from '@shared/data'

export interface ProfileParticipantsProps {
  team?: Team
  participants?: User[]
  ownerUser?: User
  isMyTeam?: boolean
  invitedParticipants?: UserWithRequest[]
  applicationParticipants?: UserWithRequest[]
}
