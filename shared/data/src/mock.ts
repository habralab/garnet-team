import { MockUser } from './data.interfaces'

export const mockAuthUserId = '6516e88d723450ac19495148'

export const mockMyUser: MockUser = {
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
      name: `Dream Team ${index}`,
      countProjects: index + 1,
      countUsers: index + 1,
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
  projectsListByUser: {
    projects: Array.from({ length: 20 }, (_, index) => ({
      id: String(index),
      name: `Dream Project ${index}`,
      countTeams: index + 1,
      countUsers: index + 1,
      avatarUrl:
        'https://is4-ssl.mzstatic.com/image/thumb/Music112/v4/2e/3a/7f/2e3a7f87-761b-0113-9f01-dd4fc4470dcf/5059801037680_cover.jpg/1200x1200bf-60.jpg',
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

export const mockUser: MockUser = {
  userGet: {
    id: '6516e88d723450ac19495abc',
    userName: 'Андрей Журавлев',
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
      'https://images.unsplash.com/photo-1539571696357-5a69c17a67c6?auto=format&fit=crop&w=1080&h=859',
  },
  teamsListByUser: {
    teams: Array.from({ length: 20 }, (_, index) => ({
      id: String(index),
      name: `Dream Team ${index}`,
      countProjects: index + 1,
      countUsers: index + 1,
      avatarUrl:
        'https://cdns-images.dzcdn.net/images/cover/2012895b602fffa8ce2e061b20bed087/1000x1000.jpg',
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
  projectsListByUser: {
    projects: Array.from({ length: 20 }, (_, index) => ({
      id: String(index),
      name: `Dream Project ${index}`,
      countTeams: index + 1,
      countUsers: index + 1,
      avatarUrl:
        'https://is4-ssl.mzstatic.com/image/thumb/Music122/v4/97/65/ae/9765ae34-5f56-cd3e-cfb2-6027efeb1d10/5063010116104_cover.jpg/800x800bb.jpg',
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

export const getMockUser = (id: string) => {
  if (id === mockUser.userGet?.id)
    return {
      user: mockUser.userGet,
      teams: mockUser.teamsListByUser?.teams || [],
      projects: mockUser.projectsListByUser?.projects || [],
    }

  if (id === mockMyUser.userGet?.id)
    return {
      user: mockMyUser.userGet,
      teams: mockMyUser.teamsListByUser?.teams || [],
      projects: mockMyUser.projectsListByUser?.projects || [],
    }

  return null
}
