import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { Column }           from '@ui/layout'
import { Title }            from '@ui/title'

export const Profile = () => {
  return (
    <Column>
      <Title hasBack>
        <FormattedMessage id='profile.my_profile' />
      </Title>
      Lorem ipsum dolor sit amet consectetur, adipisicing elit. Obcaecati officia, doloribus quis
      sed sapiente eaque ex cupiditate consequatur ut veritatis esse quam cum provident excepturi
      expedita exercitationem doloremque commodi odio.
    </Column>
  )
}
