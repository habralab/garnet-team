import { useRouter }        from 'next/router'
import { useEffect }        from 'react'
import { useState }         from 'react'

import { useGetAuthUserId } from '@shared/data'
import { useGetUser }       from '@shared/data'

import { PageState }        from '../onboarding.interfaces'

export const usePageState = () => {
  const [pageState, setPageState] = useState<PageState>(PageState.UPLOAD_PHOTO)
  const router = useRouter()

  const { authUserId } = useGetAuthUserId()

  const redirectToUserPage = () => router.push(`/user/${authUserId}`)

  const { user } = useGetUser({ id: authUserId, skip: 0, take: 20 })

  useEffect(() => {
    if (user && !user.avatarUrl) {
      setPageState(PageState.UPLOAD_PHOTO)
    } //
    else if (user && !user.description) {
      setPageState(PageState.ENTER_DESCRIPTION)
    } //
    else if (user && (!user.tags || user.tags.length === 0)) {
      setPageState(PageState.ENTER_SKILLS)
    } //
    else if (user) {
      redirectToUserPage()
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user])

  return { pageState, setPageState, redirectToUserPage }
}
