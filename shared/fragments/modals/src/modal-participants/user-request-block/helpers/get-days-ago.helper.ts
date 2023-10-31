export const getDaysAgo = (date: string): number =>
  Math.round((Date.now() - new Date(date).getTime()) / (1000 * 60 * 60 * 24))
