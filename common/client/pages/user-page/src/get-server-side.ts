import { GetServerSideProps } from 'next'

import { GET_USER }           from '@shared/data'
import { getClient }          from '@shared/data'

export const getServerSideProps: GetServerSideProps = async (context) => {
  const id = context.params?.id as string

  const client = getClient()

  try {
    const { data } = await client.query({
      query: GET_USER,
      variables: { id, skip: 0, take: 0 },
    })

    return {
      props: {
        user: data?.userGet,
        teams: data?.teamsListByUser?.teams || [],
        projects: data?.projectFilterByUserParticipantId?.projects || [],
      },
    }
  } catch (error) {
    return { props: {} }
  }
}
