import React                         from 'react'
import { FC }                        from 'react'
import { FormattedMessage }          from 'react-intl'
import { useState }                  from 'react'
import { useIntl }                   from 'react-intl'

import { Button }                    from '@ui/button'
import { Layout }                    from '@ui/layout'
import { Row }                       from '@ui/layout'
import { Modal }                     from '@ui/modal'
import { Tag }                       from '@ui/tag'
import { Text }                      from '@ui/text'

import { ButtonWatchAllSkillsProps } from './button-watch-all-skills.interfaces'

export const ButtonWatchAllSkills: FC<ButtonWatchAllSkillsProps> = ({ skills }) => {
  const [modalOpen, setModalOpen] = useState(false)
  const { formatMessage } = useIntl()

  const openModal = () => setModalOpen(true)
  const closeModal = () => setModalOpen(false)

  return (
    <>
      <Button variant='link' size='micro' onClick={openModal}>
        <Text fontSize='normal' color='currentColor'>
          <FormattedMessage id='profile.watch_all' />
        </Text>
      </Button>
      <Modal
        open={modalOpen}
        title={formatMessage({ id: 'profile.skills' })}
        okText={formatMessage({ id: 'profile.close' })}
        showCancel={false}
        onClose={closeModal}
        onOk={closeModal}
      >
        <Row flexWrap='wrap' style={{ gap: 10 }}>
          {skills?.map((tag) => (
            <Tag key={tag}>{tag}</Tag>
          ))}
        </Row>
        <Layout flexBasis={50} flexShrink={0} />
      </Modal>
    </>
  )
}
