import React                     from 'react'
import { FC }                    from 'react'

import { ClearIcon }             from '@ui/icon'
import { Box }                   from '@ui/layout'
import { useHover }              from '@shared/utils'

import { ButtonDeleteProps }     from './button-delete.interfaces'
import { useDeleteNotification } from '../../../../data'
import { useGetNotifications }   from '../../../../data'

export const ButtonDelete: FC<ButtonDeleteProps> = ({ id }) => {
  const [hover, hoverProps] = useHover()

  const { deleteNotification, loading } = useDeleteNotification()
  const { refetch } = useGetNotifications()

  const handleDeleteNotification = async () => {
    try {
      if (loading) return

      await deleteNotification({ variables: { id } })

      refetch()
    } catch (error) {
      if (process.env.NODE_ENV !== 'production') throw error
    }
  }

  return (
    <Box {...hoverProps} style={{ cursor: 'pointer' }} onClick={handleDeleteNotification}>
      <ClearIcon width={24} height={24} color={hover ? 'secondary' : 'gray'} />
    </Box>
  )
}
