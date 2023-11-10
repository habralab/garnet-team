import { useRouter }        from 'next/router'
import { useCallback }      from 'react'
import { useEffect }        from 'react'
import { useState }         from 'react'

import { useGetAuthUserId } from '@shared/data'
import { useGetUser }       from '@shared/data'

import { PageState }        from '../onboarding.interfaces'

export const usePageState = () => {
  const [pageState, setPageState] = useState<PageState>(PageState.UPLOAD_PHOTO)
  const { replace } = useRouter()

  const { authUserId } = useGetAuthUserId()

  const redirectToProfilePage = useCallback(() => replace(`/profile`), [replace])

  const { user } = useGetUser({ id: authUserId, skip: 0, take: 20 })

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
