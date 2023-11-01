interface Items {
  [key: string]: any
  tags?: string[]
}

export const getUniqueTags = (items: Items[]): string[] => {
  const allItemsTags = items.map((item) => item.tags).flat()

  const setUniqueTags = new Set<string | undefined>(allItemsTags)

  setUniqueTags.delete(undefined)

  return Array.from(setUniqueTags) as string[]
}
