import React                 from 'react'
import { FC }                from 'react'
import { FormattedMessage }  from 'react-intl'
import { useState }          from 'react'

import { Button }            from '@ui/button'
import { CardProject }       from '@ui/card'
import { Condition }         from '@ui/condition'
import { Box }               from '@ui/layout'
import { Grid }              from '@ui/layout'
import { Layout }            from '@ui/layout'
import { Text }              from '@ui/text'

import { ListProjectsProps } from './list-projects.interfaces'
import { itemPerPage }       from './list-projects.config'

export const ListProjects: FC<ListProjectsProps> = ({ projects }) => {
  const [countPages, setCountPages] = useState(1)

  const handleShowMore = () => setCountPages(countPages + 1)

  return (
    <>
      <Condition match={projects.length === 0}>
        <Box justifyContent='center'>
          <Text fontSize='semiLarge' fontWeight='semiBold' color='text.gray' opacity={0.6}>
            <FormattedMessage id='user_projects.no_projects' />
          </Text>
        </Box>
      </Condition>
      <Condition match={projects.length > 0}>
        <Grid gap={22} gridWrap='small' justifyContent='space-between'>
          {projects.slice(0, itemPerPage * countPages).map((project) => (
            <CardProject key={project.id} project={project} cardSize='small' />
          ))}
        </Grid>
        <Condition match={projects.slice(0, itemPerPage * countPages).length !== projects.length}>
          <Layout flexBasis={40} flexShrink={0} />
          <Box justifyContent='center'>
            <Button variant='secondary' size='small' onClick={handleShowMore}>
              <Text fontSize='normal' color='currentColor'>
                <FormattedMessage id='user_projects.show_more' />
              </Text>
            </Button>
          </Box>
        </Condition>
      </Condition>
    </>
  )
}
