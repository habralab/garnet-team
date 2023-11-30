import React              from 'react'
import { FC }             from 'react'

import { Avatar }         from '@ui/avatar'
import { InputRadio }     from '@ui/input'
import { Box }            from '@ui/layout'
import { Column }         from '@ui/layout'
import { Row }            from '@ui/layout'
import { Layout }         from '@ui/layout'
import { Tag }            from '@ui/tag'
import { Text }           from '@ui/text'
import { routes }         from '@shared/routes'

import { ListTeamsProps } from './list-teams.interfaces'

export const ListTeams: FC<ListTeamsProps> = ({ teams, selectedTeam, onChangeSelectedTeam }) => (
  <Column fill id='custom-scrollbar' maxHeight={274} gap={20} overflow='auto'>
    {teams.map(({ id, avatarUrl, name, tags }) => (
      <Row key={id} alignItems='center'>
        <Box height={20}>
          <InputRadio value={selectedTeam === id} onChange={() => onChangeSelectedTeam(id)} />
        </Box>
        <Layout flexBasis={20} flexShrink={0} />
        <Avatar
          image={avatarUrl}
          size={74}
          title={name}
          shape='square'
          url={`${routes.teams}/${id}`}
        />
        <Layout flexBasis={24} flexShrink={0} />
        <Column fill justifyContent='center'>
          <Text fontSize='regular' fontWeight='bold' color='text.secondary'>
            {name}
          </Text>
          <Layout flexBasis={6} flexShrink={0} />
          <Row flexWrap='wrap' maxHeight={22} overflow='hidden' gap={10}>
            {tags?.map((tag) => (
              <Tag key={tag} size='small'>
                {tag}
              </Tag>
            ))}
          </Row>
        </Column>
      </Row>
    ))}
  </Column>
)
