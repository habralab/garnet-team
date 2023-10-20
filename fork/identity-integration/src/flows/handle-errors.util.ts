/* eslint-disable consistent-return */
/* eslint-disable prefer-template */
/* eslint-disable default-case */

import { AxiosError }     from 'axios'
import { NextRouter }     from 'next/router'
import { Dispatch }       from 'react'
import { SetStateAction } from 'react'

export const handleFlowError = <S>(
    router: NextRouter,
    flowType: 'login' | 'registration' | 'settings' | 'recovery' | 'verification',
    onResetFlow: Dispatch<SetStateAction<S | undefined>>,
    onError?: (error: any) => void
  ) =>
  async (error: AxiosError<any>) => {
    switch (error.response?.data.error?.id) {
      case 'session_aal2_required':
        window.location.href = error.response?.data.redirect_browser_to

        return
      case 'session_already_available':
        if (error.response?.data?.redirect_browser_to) {
          window.location.href = error.response.data.redirect_browser_to
        } else {
          await router.push('/profile/settings')
        }

        return
      case 'session_refresh_required':
        window.location.href = error.response?.data.redirect_browser_to

        return
      case 'self_service_flow_return_to_forbidden':
        if (onError) {
          onError(error.response.data.error)
        }

        onResetFlow(undefined)

        await router.push(flowType === 'settings' ? '/profile/settings' : '/auth/' + flowType)

        return
      case 'self_service_flow_expired':
        if (onError) {
          onError(error.response.data.error)
        }

        onResetFlow(undefined)

        await router.push(flowType === 'settings' ? '/profile/settings' : '/auth/' + flowType)

        return
      case 'security_csrf_violation':
        if (onError) {
          onError(error.response.data.error)
        }

        onResetFlow(undefined)

        await router.push(flowType === 'settings' ? '/profile/settings' : '/auth/' + flowType)

        return
      case 'security_identity_mismatch':
        onResetFlow(undefined)

        await router.push(flowType === 'settings' ? '/profile/settings' : '/auth/' + flowType)

        return
      case 'browser_location_change_required':
        window.location.href = error.response.data.redirect_browser_to

        return
    }

    switch (error.response?.status) {
      case 410:
        onResetFlow(undefined)

        await router.push(flowType === 'settings' ? '/profile/settings' : '/auth/' + flowType)

        return
    }

    return Promise.reject(error)
  }
