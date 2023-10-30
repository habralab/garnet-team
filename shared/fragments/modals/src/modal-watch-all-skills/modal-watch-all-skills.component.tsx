import React                        from 'react'
import { FC }                       from 'react'
import { useIntl }                  from 'react-intl'

import { Layout }                   from '@ui/layout'
import { Row }                      from '@ui/layout'
import { Modal }                    from '@ui/modal'
import { Tag }                      from '@ui/tag'

import { ModalWatchAllSkillsProps } from './modal-watch-all-skills.interfaces'

export const ModalWatchAllSkills: FC<ModalWatchAllSkillsProps> = ({
  modalOpen = false,
  onClose,
  skills = [],
}) => {
  const { formatMessage } = useIntl()

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.skills' })}
      okText={formatMessage({ id: 'shared_ui.modal.close' })}
      showCancel={false}
      onClose={onClose}
      onOk={onClose}
    >
      <Row flexWrap='wrap' gap={10}>
        {skills?.map((tag) => (
          <Tag key={tag}>{tag}</Tag>
        ))}
      </Row>
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
