import React          from 'react'

import { Background } from '@ui/background'
import { Wrapper }    from '@ui/background'
import { Header }     from '@user/header-fragment'

const UserPage = () => (
  <Background fill color='lightGreyForty' flexDirection='column' alignItems='center'>
    <Header />
    <Wrapper>
      Lorem ipsum dolor sit amet consectetur, adipisicing elit. Obcaecati officia, doloribus quis
      sed sapiente eaque ex cupiditate consequatur ut veritatis esse quam cum provident excepturi
      expedita exercitationem doloremque commodi odio.
    </Wrapper>
  </Background>
)

export default UserPage
