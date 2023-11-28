import React                from 'react'
import { FC }               from 'react'
import { FormattedMessage } from 'react-intl'
import { useState }         from 'react'
import { useIntl }          from 'react-intl'

import { Condition }        from '@ui/condition'
import { Column }           from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Modal }            from '@ui/modal'
import { Text }             from '@ui/text'
import { useGetUser }       from '@shared/data'
import { useSession }       from '@stores/session'

import { ButtonCreateTeam } from './button-create-team'
import { ListTeams }        from './list-teams'
import { ModalInviteProps } from './modal-invite.interfaces'

export const ModalInvite: FC<ModalInviteProps> = ({
  title,
  modalOpen,
  loading,
  onClose,
  onSubmit,
}) => {
  const [selectedTeam, setSelectedTeam] = useState<string>()

  const { formatMessage } = useIntl()

  const { userId } = useSession()

  const { user, teams: allTeams } = useGetUser({ id: userId })
  const teams = allTeams?.filter((team) => team.ownerUserId === user?.id) || []

  const handleCancel = () => {
    setSelectedTeam(undefined)
    onClose?.()
  }

  const handleSubmit = () => onSubmit?.(selectedTeam)

  return (
    <Modal
      open={modalOpen}
      title={title}
      onClose={handleCancel}
      onCancel={handleCancel}
      showCancel={teams.length > 0}
      showConfirm={teams.length > 0}
      onConfirm={handleSubmit}
      confirmText={formatMessage({ id: 'shared_ui.modal.invite' })}
      confirmProps={{ disabled: !selectedTeam || loading }}
    >
      <Condition match={teams.length === 0}>
        <Column height={120} justifyContent='center' alignItems='center'>
          <Text fontSize='normal' color='text.gray'>
            <FormattedMessage id='shared_ui.modal.you_do_not_have_teams' />
          </Text>
          <Layout flexBasis={20} flexShrink={0} />
          <ButtonCreateTeam user={user} />
        </Column>
      </Condition>
      <Condition match={teams.length > 0}>
        <ListTeams
          teams={teams}
          selectedTeam={selectedTeam}
          onChangeSelectedTeam={setSelectedTeam}
        />
        <Layout flexBasis={40} flexShrink={0} />
      </Condition>
    </Modal>
  )
}
