export const routes = {
  root: '/profile',
  onboard: '/onboard',
  profile: '/profile',
  projects: '/projects',
  projectsTeams: (id: string) => `/projects/${id}/teams`,
  teams: '/teams',
  teamsInvite: '/teams/invite',
  users: '/users',
  usersProjects: (id: string) => `/users/${id}/projects`,
  usersTeams: (id: string) => `/users/${id}/teams`,
}
