/* eslint-disable default-case */

import { UpdateVerificationFlowBody }                 from '@ory/kratos-client'
import { VerificationFlow as KratosVerificationFlow } from '@ory/kratos-client'
import { PropsWithChildren }                          from 'react'

import React                                          from 'react'
import { AxiosError }                                 from 'axios'
import { FC }                                         from 'react'
import { useRouter }                                  from 'next/router'
import { useState }                                   from 'react'
import { useEffect }                                  from 'react'
import { useMemo }                                    from 'react'
import { useCallback }                                from 'react'

import { FlowProvider }                               from '../providers'
import { ValuesProvider }                             from '../providers'
import { ValuesStore }                                from '../providers'
import { SubmitProvider }                             from '../providers'
import { kratos }                                     from '../sdk'

export interface VerificationFlowProps {
  onError?: (error: { id: string }) => void
  returnToUrl?: string
}

export const VerificationFlow: FC<PropsWithChildren<VerificationFlowProps>> = ({ children, onError, returnToUrl }) => {
  const [flow, setFlow] = useState<KratosVerificationFlow>()
  const [submitting, setSubmitting] = useState<boolean>(false)
  const [loading, setLoading] = useState<boolean>(true)
  const values = useMemo(() => new ValuesStore(), [])
  const router = useRouter()

  const { return_to: returnTo, flow: flowId, refresh, aal } = router.query

  useEffect(() => {
    if (!router.isReady || flow) {
      return
    }

    if (flowId) {
      kratos
        .getVerificationFlow({ id: String(flowId) }, { withCredentials: true })
        .then(({ data }) => {
          setFlow(data)
        })
        .catch((error: AxiosError<KratosVerificationFlow>) => {
          switch (error.response?.status) {
            case 410:
            case 403:
              return router.push('/auth/verification')
          }

          throw error
        })
        .finally(() => setLoading(false))

      return
    }

    kratos
      .createBrowserVerificationFlow(
        { returnTo: String(returnTo) ?? returnToUrl },
        {
          withCredentials: true,
        }
      )
      .then(({ data }) => {
        setFlow(data)
      })
      .catch((error: AxiosError<KratosVerificationFlow>) => {
        switch (error.response?.status) {
          case 400:
            return router.push('/')
        }

        throw error
      })
      .finally(() => setLoading(false))
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [flowId, router, router.isReady, aal, refresh, returnTo, flow, onError])

  useEffect(() => {
    if (flow) {
      values.setFromFlow(flow)
    }
  }, [values, flow])

  const onSubmit = useCallback(
    (override?: Partial<UpdateVerificationFlowBody>) => {
      setSubmitting(true)

      const body = {
        ...(values.getValues() as UpdateVerificationFlowBody),
        ...(override || {}),
      }

      kratos
        .updateVerificationFlow(
          { flow: String(flow?.id), updateVerificationFlowBody: body },
          {
            withCredentials: true,
          }
        )
        .then(({ data }) => {
          setFlow(data)
        })
        .catch((error: AxiosError<KratosVerificationFlow>) => {
          switch (error.response?.status) {
            case 400:
              setFlow(error.response?.data)
              return
          }

          throw error
        })
        .finally(() => setSubmitting(false))
    },
    [flow, values, setSubmitting]
  )

  return (
    <FlowProvider value={{ flow, loading }}>
      <ValuesProvider value={values}>
        {/* @ts-ignore Enum conflict with string */}
        <SubmitProvider value={{ submitting, onSubmit }}>{children}</SubmitProvider>
      </ValuesProvider>
    </FlowProvider>
  )
}
