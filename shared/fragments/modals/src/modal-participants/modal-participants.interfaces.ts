import { User }            from '@shared/data'
import { UserWithRequest } from '@shared/data'

export interface ModalParticipantsProps {
  modalOpen?: boolean
  onClose?: () => void
  title?: string
  participants?: User[]
  ownerUser?: User
  isMyTeam?: boolean
  invitedParticipants?: UserWithRequest[]
  applicationParticipants?: UserWithRequest[]
}
