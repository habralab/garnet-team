import React                      from 'react'
import { FC }                     from 'react'
import { FormattedMessage }       from 'react-intl'
import { useIntl }                from 'react-intl'

import { Condition }              from '@ui/condition'
import { Column }                 from '@ui/layout'
import { Layout }                 from '@ui/layout'
import { Modal }                  from '@ui/modal'
import { Text }                   from '@ui/text'

import { ListParticipants }       from './list-participants'
import { ModalParticipantsProps } from './modal-participants.interfaces'
import { UserRequestBlock }       from './user-request-block'

export const ModalParticipants: FC<ModalParticipantsProps> = ({
  modalOpen,
  onClose,
  title,
  isMyTeam = false,
  ownerUser,
  participants = [],
  applicationParticipants = [],
  invitedParticipants = [],
}) => {
  const { formatMessage } = useIntl()

  const handleSubmit = () => {
    /** @todo submit changes */
    onClose?.()
  }

  return (
    <Modal
      open={modalOpen}
      title={title}
      showCancel={isMyTeam}
      onClose={onClose}
      onCancel={onClose}
      onOk={handleSubmit}
      okText={formatMessage({ id: isMyTeam ? 'shared_ui.save' : 'shared_ui.modal.close' })}
    >
      <Layout flexBasis={18} flexShrink={0} />
      <Condition match={isMyTeam && applicationParticipants.length > 0}>
        <Column>
          <Text fontSize='medium' fontWeight='bold' color='text.secondary'>
            <FormattedMessage id='shared_ui.modal.new_applications' />
            {`: ${applicationParticipants.length}`}
          </Text>
          <Layout flexBasis={20} flexShrink={0} />
          <Column fill id='custom-scrollbar' maxHeight={412} overflow='auto'>
            {applicationParticipants.map((user) => (
              <>
                <UserRequestBlock user={user} size='large' />
                <Layout flexBasis={20} flexShrink={0} />
              </>
            ))}
          </Column>
        </Column>
        <Layout flexBasis={50} flexShrink={0} />
      </Condition>
      <Condition match={isMyTeam && invitedParticipants.length > 0}>
        <Column>
          <Text fontSize='medium' fontWeight='bold' color='text.secondary'>
            <FormattedMessage id='shared_ui.modal.invitations' />
            {`: ${invitedParticipants.length}`}
          </Text>
          <Layout flexBasis={20} flexShrink={0} />
          <Column fill id='custom-scrollbar' maxHeight={412} overflow='auto'>
            {invitedParticipants.map((user) => (
              <>
                <UserRequestBlock user={user} size='large' />
                <Layout flexBasis={20} flexShrink={0} />
              </>
            ))}
          </Column>
        </Column>
        <Layout flexBasis={50} flexShrink={0} />
      </Condition>
      <Column fill>
        <Text fontSize='medium' fontWeight='bold' color='text.secondary'>
          <FormattedMessage id='shared_ui.modal.participants' />
          {`: ${participants.length}`}
        </Text>
        <Layout flexBasis={20} flexShrink={0} />
        <ListParticipants isMyTeam={isMyTeam} ownerUser={ownerUser} participants={participants} />
      </Column>
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
