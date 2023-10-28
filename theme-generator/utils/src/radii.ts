export const toRadiiString = (node: number[]): { token: string; radius: string } => {
  const topLeft = node.at(0)
  const topRight = node.at(1)
  const bottomLeft = node.at(2)
  const bottomRight = node.at(3)

  const formattedRadius = `${topLeft}px ${topRight}px ${bottomLeft}px ${bottomRight}px`

  if (topLeft !== 0 && topRight !== 0 && bottomLeft === 0 && bottomRight === 0) {
    return {
      token: `tl${topLeft}tr${topRight}`,
      radius: formattedRadius,
    }
  }

  if (topLeft === 0 && topRight === 0 && bottomLeft !== 0 && bottomRight !== 0) {
    return {
      token: `bl${bottomLeft}br${bottomRight}`,
      radius: formattedRadius,
    }
  }

  if (topLeft !== 0 && topRight !== 0 && bottomLeft !== 0 && bottomRight === 0) {
    return {
      token: `tl${topLeft}tr${topRight}bl${bottomLeft}`,
      radius: formattedRadius,
    }
  }

  if (topLeft !== 0 && topRight !== 0 && bottomLeft === 0 && bottomRight !== 0) {
    return {
      token: `tl${topLeft}tr${topRight}br${bottomRight}`,
      radius: formattedRadius,
    }
  }

  if (topLeft !== 0 && topRight === 0 && bottomLeft !== 0 && bottomRight !== 0) {
    return {
      token: `tl${topLeft}bl${bottomLeft}br${bottomRight}`,
      radius: formattedRadius,
    }
  }

  if (topLeft === 0 && topRight !== 0 && bottomLeft !== 0 && bottomRight !== 0) {
    return {
      token: `tr${topRight}bl${bottomLeft}br${bottomRight}`,
      radius: formattedRadius,
    }
  }

  if (topLeft !== 0 && topRight === 0 && bottomLeft !== 0 && bottomRight === 0) {
    return {
      token: `tl${topLeft}bl${bottomLeft}`,
      radius: formattedRadius,
    }
  }

  if (topLeft === 0 && topRight !== 0 && bottomLeft === 0 && bottomRight !== 0) {
    return {
      token: `tr${topRight}br${bottomRight}`,
      radius: formattedRadius,
    }
  }

  if (topLeft !== 0 && topRight === 0 && bottomLeft === 0 && bottomRight === 0) {
    return {
      token: `tl${topLeft}`,
      radius: formattedRadius,
    }
  }

  if (topLeft === 0 && topRight !== 0 && bottomLeft === 0 && bottomRight === 0) {
    return {
      token: `tr${topRight}`,
      radius: formattedRadius,
    }
  }

  if (topLeft === 0 && topRight === 0 && bottomLeft !== 0 && bottomRight === 0) {
    return {
      token: `bl${bottomLeft}`,
      radius: formattedRadius,
    }
  }

  if (topLeft === 0 && topRight === 0 && bottomLeft === 0 && bottomRight !== 0) {
    return {
      token: `br${bottomRight}`,
      radius: formattedRadius,
    }
  }

  return {
    token: '',
    radius: '',
  }
}
