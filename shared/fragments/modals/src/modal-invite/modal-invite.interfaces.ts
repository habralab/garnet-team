export interface ModalInviteProps {
  title?: string
  modalOpen?: boolean
  loading: boolean
  onClose?: () => void
  onSubmit?: (teamId?: string) => void
}
