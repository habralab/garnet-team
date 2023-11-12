import React                       from 'react'
import { FC }                      from 'react'
import { FormattedMessage }        from 'react-intl'
import { useState }                from 'react'
import { useIntl }                 from 'react-intl'

import { Avatar }                  from '@ui/avatar'
import { Condition }               from '@ui/condition'
import { InputRadio }              from '@ui/input'
import { Box }                     from '@ui/layout'
import { Column }                  from '@ui/layout'
import { Row }                     from '@ui/layout'
import { Layout }                  from '@ui/layout'
import { Modal }                   from '@ui/modal'
import { Tag }                     from '@ui/tag'
import { Text }                    from '@ui/text'
import { useGetUser }              from '@shared/data'

import { ButtonCreateTeam }        from './button-create-team'
import { ModalInviteToTeamProps }  from './modal-invite-to-team.interfaces'
import { useCreateTeamJoinInvite } from './data'

export const ModalInviteToTeam: FC<ModalInviteToTeamProps> = ({
  invitedUserId,
  modalOpen,
  onClose,
}) => {
  const [selectedTeam, setSelectedTeam] = useState<string>()

  const { formatMessage } = useIntl()

  const { createTeamJoiInvite } = useCreateTeamJoinInvite()

  const { user, teams: allTeams } = useGetUser({ id: invitedUserId })
  const teams = allTeams?.filter((team) => team.ownerUserId === user?.id) || []

  const handleCancel = () => {
    setSelectedTeam(undefined)
    onClose?.()
  }

  const handleSubmit = async () => {
    try {
      await createTeamJoiInvite({
        variables: { userId: invitedUserId || '', teamId: selectedTeam || '' },
      })
      onClose?.()
    } catch (error) {
      /** @todo error notification */
    }
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.invite_to_team' })}
      onClose={handleCancel}
      onCancel={handleCancel}
      showCancel={teams.length > 0}
      showConfirm={teams.length > 0}
      onConfirm={handleSubmit}
      confirmText={formatMessage({ id: 'shared_ui.modal.invite' })}
      confirmProps={{ disabled: !selectedTeam }}
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
        <Column fill id='custom-scrollbar' maxHeight={274} gap={20} overflow='auto'>
          {teams.map(({ id, avatarUrl, name, tags }) => (
            <Row key={id} alignItems='center'>
              <Box height={20}>
                <InputRadio value={selectedTeam === id} onChange={() => setSelectedTeam(id)} />
              </Box>
              <Layout flexBasis={20} flexShrink={0} />
              <Avatar image={avatarUrl} size={74} title={name} shape='square' url={`/team/${id}`} />
              <Layout flexBasis={24} flexShrink={0} />
              <Column width='100%' height='auto' justifyContent='center'>
                <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
                  {name}
                </Text>
                <Layout flexBasis={6} flexShrink={0} />
                <Row flexWrap='wrap' maxHeight={22} overflow='hidden' gap={10}>
                  {tags?.map((tag) => (
                    <Tag key={tag} size='small'>
                      {tag}
                    </Tag>
                  ))}
                </Row>
              </Column>
            </Row>
          ))}
        </Column>
        <Layout flexBasis={40} flexShrink={0} />
      </Condition>
    </Modal>
  )
}
