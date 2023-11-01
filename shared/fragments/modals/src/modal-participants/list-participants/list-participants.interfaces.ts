import { ModalParticipantsProps } from '../modal-participants.interfaces'

export type ListParticipantsProps = Pick<
  ModalParticipantsProps,
  'participants' | 'isMyTeam' | 'ownerUser'
>
