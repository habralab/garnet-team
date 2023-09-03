import React                  from 'react'

import { FlowMessages }       from '@fork/next-identity-integration'
import { SettingsFlow }       from '@fork/next-identity-integration'
import { GeneralMessages }    from '@identity/messages-fragment'
import { ProfileInformation } from '@identity/profile-information-fragment'
import { ProfileLayout }      from '@identity/profile-layout-fragment'
import { ProfileOidc }        from '@identity/profile-oidc-fragment'
import { ProfileSecurity }    from '@identity/profile-security-fragment'
import { Layout }             from '@ui/layout'
import { Column }             from '@ui/layout'

export const SettingsPage = () => (
  <SettingsFlow>
    <ProfileLayout>
      <Column px={[16, 32, 32, 64]} height='100%' overflowY='scroll'>
        <Layout pt={34}>
          <FlowMessages>
            {(messages) => (
              <Layout>
                <GeneralMessages messages={messages} />
              </Layout>
            )}
          </FlowMessages>
        </Layout>
        <Layout pb={64}>
          <ProfileInformation />
        </Layout>
        <Layout pb={64}>
          <ProfileSecurity />
        </Layout>
        <Layout>
          <ProfileOidc />
        </Layout>
        <Layout flexBasis={[32, 56]} />
      </Column>
    </ProfileLayout>
  </SettingsFlow>
)
