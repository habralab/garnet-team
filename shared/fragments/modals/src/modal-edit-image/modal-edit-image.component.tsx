import React                   from 'react'
import Cropper                 from 'react-cropper'
import { FC }                  from 'react'
import { ReactCropperElement } from 'react-cropper'
import { useRef }              from 'react'
import { useIntl }             from 'react-intl'

import { Layout }              from '@ui/layout'
import { Modal }               from '@ui/modal'
import { theme }               from '@ui/theme'

import { Container }           from './container'
import { ModalEditImageProps } from './modal-edit-image.interfaces'

export const ModalEditImage: FC<ModalEditImageProps> = ({
  modalOpen = false,
  onClose,
  onConfirm,
  shape = 'circle',
  image,
}) => {
  const { formatMessage } = useIntl()

  const cropperRef = useRef<ReactCropperElement>(null)

  const handleConfirm = () => {
    cropperRef.current?.cropper
      .getCroppedCanvas()
      .toBlob((blob) => blob && onConfirm?.(URL.createObjectURL(blob)))
  }

  return (
    <Modal
      open={modalOpen}
      title={formatMessage({ id: 'shared_ui.modal.edit_photo' })}
      onClose={onClose}
      onCancel={onClose}
      onConfirm={handleConfirm}
    >
      <Container shape={shape}>
        <Cropper
          src={image}
          style={{ height: 392, width: '100%', borderRadius: theme.radii.medium }}
          aspectRatio={1 / 1}
          viewMode={1}
          dragMode='move'
          guides={false}
          center={false}
          highlight={false}
          background={false}
          toggleDragModeOnDblclick={false}
          rotatable={false}
          ref={cropperRef}
          minCropBoxHeight={282}
          minCropBoxWidth={282}
        />
      </Container>
      <Layout flexBasis={50} flexShrink={0} />
    </Modal>
  )
}
