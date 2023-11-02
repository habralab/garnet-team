import React                from 'react'
import { FC }               from 'react'
import { useRouter }        from 'next/router'
import { useEffect }        from 'react'
import { useState }         from 'react'

import { Condition }        from '@ui/condition'
import { Column }           from '@ui/layout'
import { mockAuthUserId }   from '@shared/data'
import { useGetUser }       from '@shared/data'

import { EnterDescription } from './enter-description'
import { EnterSkills }      from './enter-skills'
import { PageState }        from './onboarding.interfaces'
import { UploadPhoto }      from './upload-photo'

const getUserProps = { id: mockAuthUserId, skip: 0, take: 20 }

export const Onboarding: FC = () => {
  const [pageState, setPageState] = useState<PageState>(PageState.UPLOAD_PHOTO)
  const router = useRouter()

  const { user } = useGetUser(getUserProps)

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
      router.push(`/user/${mockAuthUserId}`)
    }
  }, [user])

  const handleUploadPhoto = () => setPageState(PageState.ENTER_DESCRIPTION)
  const handleEnterDescription = () => setPageState(PageState.ENTER_SKILLS)
  const handleEnterSkills = () => router.push(`/user/${mockAuthUserId}`)

  return (
    <Column fill marginBottom={32} maxWidth={632} justifyContent='center'>
      <Condition match={pageState === PageState.UPLOAD_PHOTO}>
        <UploadPhoto onSubmit={handleUploadPhoto} />
      </Condition>
      <Condition match={pageState === PageState.ENTER_DESCRIPTION}>
        <EnterDescription onSubmit={handleEnterDescription} />
      </Condition>
      <Condition match={pageState === PageState.ENTER_SKILLS}>
        <EnterSkills onSubmit={handleEnterSkills} />
      </Condition>
    </Column>
  )
}
