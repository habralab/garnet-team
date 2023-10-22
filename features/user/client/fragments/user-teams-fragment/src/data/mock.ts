export interface User {
  id?: string
  userName?: string
  description?: string
  tags?: string[]
  avatarUrl?: string
}

export interface Team {
  id?: string
  name?: string
  description?: string
  tags?: string[]
  avatarUrl?: string
  ownerUserId?: string
}

export interface MockUser {
  userGet?: User
  teamsListByUser?: {
    teams?: Team[]
  }
}

export const mockUser: MockUser = {
  userGet: {
    id: '6516e88d723450ac19495148',
    userName: 'Маша Петрова',
    description:
      'Идейные соображения высшего порядка, а также постоянное информационно-пропагандистское обеспечение нашей деятельности играет важную роль в формировании системы обучения кадров.',
    tags: Array.from({ length: 4 }, () => [
      'SMM',
      'Маркетинг',
      'Таргетированная реклама',
      'UX/UI дизайн',
      'Google Analytics',
      'SQL',
      'Таргетированная реклама',
    ]).flat(),
    avatarUrl:
      'https://themify.org/demo/themes/wp-content/blogs.dir/353/files/2019/02/author-big.jpg',
  },
  teamsListByUser: {
    teams: Array.from({ length: 20 }, (_, index) => ({
      id: String(index),
      name: 'Dream Team',
      avatarUrl: 'https://i.scdn.co/image/ab67616d0000b273b3d36734c038005d8a465010',
      tags: Array.from({ length: 2 }, () => [
        'SMM',
        'Маркетинг',
        'Таргетированная реклама',
        'UX/UI дизайн',
        'Google Analytics',
        'SQL',
        'Таргетированная реклама',
      ]).flat(),
    })),
  },
}
