import { Project }    from '@shared/data'
import { isIncludes } from '@shared/utils'

export const filterProjects = (
  projects: Project[],
  selectedTags: string[],
  search: string
): Project[] =>
  projects.filter(
    (project) =>
      selectedTags.some((tag) => project.tags?.includes(tag)) ||
      (search.length > 2 && isIncludes(project.projectName, search))
  )
