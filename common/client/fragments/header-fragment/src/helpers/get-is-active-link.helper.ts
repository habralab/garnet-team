const parseUrl = (url: string) => url.split('/')[1]

export const getIsActiveLink = (url: string, currentPathname: string) =>
  parseUrl(url) === parseUrl(currentPathname)
