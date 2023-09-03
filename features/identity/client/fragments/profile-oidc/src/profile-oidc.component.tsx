import React                from 'react'
import { FormattedMessage } from 'react-intl'

import { FlowNodesFilter }  from '@fork/next-identity-integration'
import { FlowSubmit }       from '@fork/next-identity-integration'
import { Button }           from '@ui/button'
import { Column }           from '@ui/layout'
import { Row }              from '@ui/layout'
import { Layout }           from '@ui/layout'
import { Text }             from '@ui/text'

export const ProfileOidc = () => (
  <Column>
    <Layout>
      <Text fontSize={28} fontWeigth={500}>
        <FormattedMessage id='profile_oidc.social_networks' defaultMessage='Социальные сети' />
      </Text>
    </Layout>
    <Layout flexBasis={24} />
    <Layout>
      <FlowNodesFilter
        predicate={(node) => node.group === 'oidc' && node.attributes.name === 'link'}
      >
        {(nodes) => (
          <Column>
            <Layout>
              <Text fontSize={16} fontWeigth={500}>
                <FormattedMessage
                  id='profile_oidc.unlink_social_networks'
                  defaultMessage='Привязать социальные сети'
                />
              </Text>
            </Layout>
            <Layout flexBasis={16} />
            <Layout>
              <FlowSubmit>
                {({ submitting, onSubmit }) => (
                  <Row>
                    {nodes.map(({ attributes }) => (
                      <Layout m='8px' key={attributes?.value}>
                        <Button
                          id={attributes?.name}
                          shape='circle'
                          inverted
                          disabled={submitting}
                          onClick={() => {
                            onSubmit({ [attributes.name]: attributes.value, method: undefined })
                          }}
                        >
                          <Text fontSize={28} fontWeight={600} textTransform='uppercase'>
                            {String(attributes?.value).substr(0, 1)}
                          </Text>
                        </Button>
                      </Layout>
                    ))}
                  </Row>
                )}
              </FlowSubmit>
            </Layout>
            <Layout flexBasis={24} />
          </Column>
        )}
      </FlowNodesFilter>
    </Layout>
    <Layout>
      <FlowNodesFilter
        predicate={(node) => node.group === 'oidc' && node.attributes.name === 'unlink'}
      >
        {(nodes) => (
          <Column>
            <Layout>
              <Text fontSize={16} fontWeigth={500}>
                <FormattedMessage
                  id='profile_oidc.unlink_social_networks'
                  defaultMessage='Отвязать социальные сети'
                />
              </Text>
            </Layout>
            <Layout flexBasis={16} />
            <Layout>
              <FlowSubmit>
                {({ submitting, onSubmit }) => (
                  <Row>
                    {nodes.map(({ attributes }) => (
                      <Layout m='8px' key={attributes?.value}>
                        <Button
                          id={attributes?.name}
                          shape='circle'
                          inverted
                          disabled={submitting}
                          onClick={() => {
                            onSubmit({ [attributes.name]: attributes.value, method: undefined })
                          }}
                        >
                          <Text fontSize={28} fontWeight={600} textTransform='uppercase'>
                            {String(attributes?.value).substr(0, 1)}
                          </Text>
                        </Button>
                      </Layout>
                    ))}
                  </Row>
                )}
              </FlowSubmit>
            </Layout>
            <Layout flexBasis={24} />
          </Column>
        )}
      </FlowNodesFilter>
    </Layout>
  </Column>
)
