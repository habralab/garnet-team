import React                from 'react'
import { FC }               from 'react'
import { useRouter }        from 'next/router'
import { useState }         from 'react'

import { Condition }        from '@ui/condition'
import { Column }           from '@ui/layout'
import { mockAuthUserId }   from '@shared/data'

import { EnterDescription } from './enter-description'
import { EnterSkills }      from './enter-skills'
import { UploadPhoto }      from './upload-photo'

enum PageState {
  UPLOAD_PHOTO,
  ENTER_DESCRIPTION,
  ENTER_SKILLS,
}

export const Onboarding: FC = () => {
  const [pageState, setPageState] = useState<PageState>(PageState.UPLOAD_PHOTO)

  const router = useRouter()

  const handleUploadPhoto = () => {
    setPageState(PageState.ENTER_DESCRIPTION)
  }

  const handleEnterDescription = (value: string) => {
    setPageState(PageState.ENTER_SKILLS)
  }

  const handleEnterSkills = (value: string[]) => {
    router.push(`/user/${mockAuthUserId}`)
  }

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
