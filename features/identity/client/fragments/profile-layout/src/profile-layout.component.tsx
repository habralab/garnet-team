import { Layout }         from '@atls-ui-proto/layout'
import { Row }            from '@atls-ui-proto/layout'

import React              from 'react'

import { ProfileSidebar } from './profile-sidebar.component'

export const ProfileLayout = ({ children }) => (
  <Row height='100%'>
    <Layout flexBasis={[280, 280, 328]} flexShrink={0} display={['none', 'flex']}>
      <ProfileSidebar />
    </Layout>
    <Layout flexGrow={1}>{children}</Layout>
  </Row>
)
