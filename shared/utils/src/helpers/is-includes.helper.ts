export const isIncludes = (value?: string, search?: string) =>
  Boolean(value?.toLowerCase().includes(search?.toLocaleLowerCase() || ''))
