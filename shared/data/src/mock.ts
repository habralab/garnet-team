import { MockUser }        from './data.interfaces'
import { UserWithRequest } from './data.interfaces'
import { User }            from './data.interfaces'
import { Team }            from './data.interfaces'
import { Project }         from './data.interfaces'

export const mockAuthUserId = 'd0dd5942-b9fd-4c80-bb3a-eac39bdee251'
export const mockNotAuthUserId = '6516e88d723450ac19495abc'

const mockTags = [
  'SMM',
  'Таргетированная реклама',
  'Маркетинг',
  'UX/UI дизайн',
  'Google Analytics',
  'SQL',
  'JavaScrip',
  'TypeScript',
  'NextJS',
  'Nest js',
  'React js',
  'Angular',
  'html',
  'css',
]

export const mockMyUser: MockUser = {
  userGet: {
    id: mockAuthUserId,
    userName: 'Маша Петрова',
    description:
      'Идейные соображения высшего порядка, а также постоянное информационно-пропагандистское обеспечение нашей деятельности играет важную роль в формировании системы обучения кадров.',
    tags: mockTags,
    avatarUrl:
      'https://themify.org/demo/themes/wp-content/blogs.dir/353/files/2019/02/author-big.jpg',
  },
  teamsListByUser: {
    teams: Array.from({ length: 20 }, (_, index) => ({
      id: String(index),
      name: `Dream Team ${index}`,
      countProjects: index + 1,
      ownerUserId: mockAuthUserId,
      countUsers: index + 1,
      avatarUrl: 'https://i.pinimg.com/originals/59/18/00/5918007654bb37ae44692437ba3b2c6d.jpg',
      tags: mockTags,
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
      tags: mockTags,
    })),
  },
}

export const mockUser: MockUser = {
  userGet: {
    id: mockNotAuthUserId,
    userName: 'Андрей Журавлев',
    description:
      'Идейные соображения высшего порядка, а также постоянное информационно-пропагандистское обеспечение нашей деятельности играет важную роль в формировании системы обучения кадров.',
    tags: mockTags,
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
      tags: mockTags,
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
      tags: mockTags,
    })),
  },
}

export const mockUsers: User[] = Array.from({ length: 10 }, (_, index) => [
  { ...mockMyUser.userGet, id: `${mockMyUser.userGet?.id}${index}` },
  { ...mockUser.userGet, id: `${mockUser.userGet?.id}${index}` },
]).flat()

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

  return undefined
}

export const mockProjects: Project[] = Array.from({ length: 20 }, (_, index) => ({
  id: String(index),
  name: `Dream Project ${index}`,
  description:
    'Идейные соображения высшего порядка, а также постоянное информационно-пропагандистское обеспечение нашей деятельности играет важную роль в формировании системы обучения кадров.',
  ownerUserId: index % 2 === 0 ? mockAuthUserId : mockNotAuthUserId,
  countTeams: index + 1,
  countUsers: index + 1,
  avatarUrl:
    index % 2 === 0
      ? 'https://is4-ssl.mzstatic.com/image/thumb/Music112/v4/2e/3a/7f/2e3a7f87-761b-0113-9f01-dd4fc4470dcf/5059801037680_cover.jpg/1200x1200bf-60.jpg'
      : 'https://is4-ssl.mzstatic.com/image/thumb/Music122/v4/97/65/ae/9765ae34-5f56-cd3e-cfb2-6027efeb1d10/5063010116104_cover.jpg/800x800bb.jpg',
  tags: mockTags,
}))

export const mockTeams: Team[] = Array.from({ length: 20 }, (_, index) => ({
  id: String(index),
  name: `Dream Team ${index}`,
  description:
    'Идейные соображения высшего порядка, а также постоянное информационно-пропагандистское обеспечение нашей деятельности играет важную роль в формировании системы обучения кадров.',
  ownerUserId: index % 2 === 0 ? mockAuthUserId : mockNotAuthUserId,
  countProjects: index + 1,
  countUsers: index + 1,
  avatarUrl:
    index % 2 === 0
      ? 'https://i.pinimg.com/originals/59/18/00/5918007654bb37ae44692437ba3b2c6d.jpg'
      : 'https://cdns-images.dzcdn.net/images/cover/2012895b602fffa8ce2e061b20bed087/1000x1000.jpg',
  tags: mockTags,
}))

export const getMockProject = (id: string) => ({
  project: mockProjects.find((project) => project.id === id),
  projectTeams: mockTeams,
})

export const getMockTeam = (id: string) => ({
  team: mockTeams.find((team) => team.id === id),
  teamProjects: mockProjects,
  teamParticipants: Array.from({ length: 10 }, (_, index) => [
    { ...mockMyUser.userGet, id: `${mockMyUser.userGet?.id}${index}` },
    { ...mockUser.userGet, id: `${mockUser.userGet?.id}${index}` },
  ]).flat() as User[],
  applicationParticipants: Array.from({ length: 3 }, (_, index) => [
    {
      ...mockMyUser.userGet,
      id: `${mockMyUser.userGet?.id}${index}`,
      requestType: 'application',
      date: new Date(new Date().setDate(new Date().getDate() - index)).toISOString(),
    },
    {
      ...mockUser.userGet,
      id: `${mockUser.userGet?.id}${index}`,
      requestType: 'application',
      date: new Date(new Date().setDate(new Date().getDate() - index)).toISOString(),
    },
  ]).flat() as UserWithRequest[],
  invitedParticipants: Array.from({ length: 3 }, (_, index) => [
    {
      ...mockMyUser.userGet,
      id: `${mockMyUser.userGet?.id}${index}`,
      requestType: 'invite',
      date: new Date(new Date().setDate(new Date().getDate() - index)).toISOString(),
    },
    {
      ...mockUser.userGet,
      id: `${mockUser.userGet?.id}${index}`,
      requestType: 'invite',
      date: new Date(new Date().setDate(new Date().getDate() - index)).toISOString(),
    },
  ]).flat() as UserWithRequest[],
})
