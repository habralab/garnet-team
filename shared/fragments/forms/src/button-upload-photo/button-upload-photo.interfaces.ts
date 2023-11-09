import { PropsWithChildren }   from 'react'

import { ModalEditImageProps } from '@shared/modals-fragment'

export interface ButtonUploadPhotoProps extends PropsWithChildren {
  onChange?: (url: string) => void
  shape?: ModalEditImageProps['shape']
}
