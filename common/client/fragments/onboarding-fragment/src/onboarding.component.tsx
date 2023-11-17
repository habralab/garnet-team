import React                from 'react'
import { FC }               from 'react'

import { Condition }        from '@ui/condition'
import { Column }           from '@ui/layout'

import { EnterDescription } from './enter-description'
import { EnterSkills }      from './enter-skills'
import { PageState }        from './onboarding.interfaces'
import { UploadPhoto }      from './upload-photo'
import { usePageState }     from './hooks'

export const OnBoarding: FC = () => {
  const { pageState, setPageState, redirectToProfilePage } = usePageState()

  const handleUploadPhoto = () => setPageState(PageState.ENTER_DESCRIPTION)
  const handleEnterDescription = () => setPageState(PageState.ENTER_SKILLS)
  const handleEnterSkills = () => redirectToProfilePage()

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
