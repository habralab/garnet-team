import React                         from 'react'
import { FC }                        from 'react'
import { FormattedMessage }          from 'react-intl'
import { useState }                  from 'react'

import { ModalWatchAllSkills }       from '@shared/modals-fragment'
import { Button }                    from '@ui/button'
import { Text }                      from '@ui/text'

import { ButtonWatchAllSkillsProps } from './button-watch-all-skills.interfaces'

export const ButtonWatchAllSkills: FC<ButtonWatchAllSkillsProps> = ({ skills }) => {
  const [modalOpen, setModalOpen] = useState(false)

  const toggleModalOpen = () => setModalOpen(!modalOpen)

  return (
    <>
      <Button variant='link' size='micro' onClick={toggleModalOpen}>
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='profile.watch_all' />
        </Text>
      </Button>
      <ModalWatchAllSkills modalOpen={modalOpen} onClose={toggleModalOpen} skills={skills} />
    </>
  )
}
