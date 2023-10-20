import { UpdateRecoveryFlowBody }             from '@ory/kratos-client'
import { RecoveryFlow as KratosRecoveryFlow } from '@ory/kratos-client'
import { PropsWithChildren }                  from 'react'

import React           from 'react'
import { AxiosError }  from 'axios'
import { FC }          from 'react'
import { useRouter }   from 'next/router'
import { useState }    from 'react'
import { useEffect }   from 'react'
import { useMemo }     from 'react'
import { useCallback } from 'react'

import { FlowProvider }    from '../providers'
import { ValuesProvider }  from '../providers'
import { ValuesStore }     from '../providers'
import { SubmitProvider }  from '../providers'
import { kratos }          from '../sdk'
import { handleFlowError } from './handle-errors.util'

export interface RecoveryFlowProps {
  onError?: (error: { id: string }) => void
  returnToUrl?: string
}

export const RecoveryFlow: FC<PropsWithChildren<RecoveryFlowProps>> = ({
  children, onError, returnToUrl
}) => {
  const [flow, setFlow] = useState<KratosRecoveryFlow>()
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
        .getRecoveryFlow({ id: String(flowId) }, { withCredentials: true })
        .then(({ data }) => {
          setFlow(data)
        })
        .catch(handleFlowError(router, 'recovery', setFlow, onError))
        .finally(() => setLoading(false))

      return
    }

    kratos
      .createBrowserRecoveryFlow(
        { returnTo: returnTo?.toString() ?? returnToUrl },
        {
          withCredentials: true,
        }
      )
      .then(({ data }) => {
        setFlow(data)
      })
      .catch(handleFlowError(router, 'recovery', setFlow, onError))
      .catch((error: AxiosError<KratosRecoveryFlow>) => {
        if (error.response?.status === 400) {
          setFlow(error.response?.data)

          return
        }

        // eslint-disable-next-line consistent-return
        return Promise.reject(error)
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
    (override?: Partial<UpdateRecoveryFlowBody>) => {
      setSubmitting(true)

      const body = {
        ...(values.getValues() as UpdateRecoveryFlowBody),
        ...(override || {}),
      }

      kratos
        .updateRecoveryFlow(
          { flow: String(flow?.id), updateRecoveryFlowBody: body },
          { withCredentials: true }
        )
        .then(({ data }) => {
          setFlow(data)
        })
        .catch(handleFlowError(router, 'recovery', setFlow))
        .catch((error: AxiosError<KratosRecoveryFlow>) => {
          if (error.response?.status === 400) {
            setFlow(error.response?.data)

            return
          }

          // eslint-disable-next-line consistent-return
          return Promise.reject(error)
        })
        .finally(() => setSubmitting(false))
    },
    [router, flow, values, setSubmitting]
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
