import { UpdateLoginFlowBody }          from '@ory/kratos-client'
import { LoginFlow as KratosLoginFlow } from '@ory/kratos-client'
import { PropsWithChildren }            from 'react'

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

export interface LoginFlowProps {
  onError?: (error: { id: string }) => void
  returnToUrl?: string
}

export const LoginFlow: FC<PropsWithChildren<LoginFlowProps>> = ({ children, onError, returnToUrl }) => {
  const [flow, setFlow] = useState<KratosLoginFlow>()
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
        .getLoginFlow({ id: String(flowId) }, { withCredentials: true })
        .then(({ data }) => {
          setFlow(data)
        })
        .catch(handleFlowError(router, 'login', setFlow, onError))
        .finally(() => setLoading(false))

      return
    }

    kratos
      .createBrowserLoginFlow(
        {
          refresh: Boolean(refresh),
          aal: aal ? String(aal) : undefined,
          returnTo: returnTo?.toString() ?? returnToUrl,
        },
        { withCredentials: true }
      )
      .then(({ data }) => {
        setFlow(data)
      })
      .catch(handleFlowError(router, 'login', setFlow, onError))
      .finally(() => setLoading(false))
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [flowId, router, router.isReady, aal, refresh, returnTo, flow, onError])

  useEffect(() => {
    if (flow) {
      values.setFromFlow(flow)
    }
  }, [values, flow])

  const onSubmit = useCallback(
    (override?: Partial<UpdateLoginFlowBody>) => {
      setSubmitting(true)

      const body = {
        ...(values.getValues() as UpdateLoginFlowBody),
        ...(override || {}),
      }

      kratos
        .updateLoginFlow(
          { flow: String(flow?.id), updateLoginFlowBody: body },
          { withCredentials: true }
        )
        .then(() => {
          if (flow?.return_to) {
            window.location.href = flow?.return_to
          } else {
            router.push(returnToUrl ?? '/')
          }
        })
        .catch(handleFlowError(router, 'login', setFlow))
        .catch((error: AxiosError<KratosLoginFlow>) => {
          if (error.response?.status === 400) {
            setFlow(error.response?.data)

            return
          }

          // eslint-disable-next-line consistent-return
          return Promise.reject(error)
        })
        .finally(() => setSubmitting(false))
    },
    // eslint-disable-next-line react-hooks/exhaustive-deps
    [router, flow, values, setSubmitting]
  )

  return (
    <FlowProvider value={{ flow, loading }}>
      <ValuesProvider value={values}>
        <SubmitProvider value={{ submitting, onSubmit }}>{children}</SubmitProvider>
      </ValuesProvider>
    </FlowProvider>
  )
}
