import { BackgroundImagePlaceholder } from '@atls-ui-proto/placeholder'
import { LogoPlaceholder }            from '@atls-ui-proto/placeholder'

import React                          from 'react'
import { FC }                         from 'react'
import { FormattedMessage }           from 'react-intl'

import { Column }                     from '@ui/layout'
import { Row }                        from '@ui/layout'
import { Layout }                     from '@ui/layout'
import { Box }                        from '@ui/layout'
import { Text }                       from '@ui/text'

import { SidebarLayoutProps }         from './layout.interfaces'

export const SidebarLayout: FC<SidebarLayoutProps> = ({
  children,
  actions,
  title,
  position = 'left',
}) => (
  <BackgroundImagePlaceholder>
    <Row height='100%'>
      {position === 'right' && <Layout flexBasis={[0, '50%']} />}
      <Layout flexBasis={['100%', '50%']}>
        <Box background='white' width='100%' px={[16, 32]}>
          <Column>
            <Layout flexGrow={1} flexBasis='50%'>
              <Column>
                <Layout flexBasis={120} justifyContent='center'>
                  <LogoPlaceholder>
                    <FormattedMessage
                      id='auth_layout.project_starter'
                      defaultMessage='Project Starter'
                    />
                  </LogoPlaceholder>
                </Layout>
              </Column>
            </Layout>
            <Layout>
              <Column>
                {title && (
                  <>
                    <Layout justifyContent='center'>
                      <Text fontSize={24} color='#A0A4B2'>
                        {title}
                      </Text>
                    </Layout>
                    <Layout flexBasis={36} />
                  </>
                )}
                <Layout justifyContent='center'>{children}</Layout>
              </Column>
            </Layout>
            <Layout flexGrow={1} flexBasis='50%' alignItems='flex-end' justifyContent='center'>
              {actions && (
                <Layout mb={40} justifyContent='center'>
                  {actions}
                </Layout>
              )}
            </Layout>
          </Column>
        </Box>
      </Layout>
    </Row>
  </BackgroundImagePlaceholder>
)
