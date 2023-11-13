import { useRouter }   from 'next/router'
import { useCallback } from 'react'
import { useEffect }   from 'react'
import { useState }    from 'react'

import { useGetUser }  from '@shared/data'
import { useSession }  from '@stores/session'

import { PageState }   from '../onboarding.interfaces'

export const usePageState = () => {
  const [pageState, setPageState] = useState<PageState>(PageState.UPLOAD_PHOTO)
  const { replace } = useRouter()

  const { userId } = useSession()

  const redirectToProfilePage = useCallback(() => replace(`/profile`), [replace])

  const { user } = useGetUser({ id: userId })

  useEffect(() => {
    if (!user) return

    const { avatarUrl, description, tags } = user

    if (!avatarUrl) {
      setPageState(PageState.UPLOAD_PHOTO)
    } else if (!description) {
      setPageState(PageState.ENTER_DESCRIPTION)
    } else if (!tags || tags?.length === 0) {
      setPageState(PageState.ENTER_SKILLS)
    } else {
      redirectToProfilePage()
    }
  }, [user, redirectToProfilePage])

  return { pageState, setPageState, redirectToUserPage: redirectToProfilePage }
}
