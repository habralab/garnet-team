import styled                   from '@emotion/styled'
import { Layout as BaseLayout } from '@atls-ui-parts/layout'
import { Box as BaseBox }       from '@atls-ui-parts/layout'
import { Row as BaseRow }       from '@atls-ui-parts/layout'
import { Column as BaseColumn } from '@atls-ui-parts/layout'

import { CSSProperties }        from 'react'
import { Config }               from 'styled-system'
import { GridProps }            from 'styled-system'
import { system }               from 'styled-system'
import { grid }                 from 'styled-system'

interface LayoutWithGap {
  gap?: CSSProperties['gap']
}

const gapConfig: Config = {
  gap: {
    property: 'gap',
    transform: (value) => (typeof value === 'number' ? `${value}px` : value),
  },
}

export const Layout = styled(BaseLayout)<LayoutWithGap>(system(gapConfig))

export const Box = styled(BaseBox)<LayoutWithGap>(system(gapConfig))

export const Row = styled(BaseRow)<LayoutWithGap>(system(gapConfig))

export const Column = styled(BaseColumn)<LayoutWithGap>(system(gapConfig))

export const Grid = styled(BaseLayout)<LayoutWithGap & GridProps & { gridWrap?: string }>(
  ({ theme, gridWrap }) => ({
    display: 'grid',
    gridTemplateColumns: gridWrap ? `repeat(auto-fill, ${theme.sizes?.[gridWrap]}px)` : 'none',
  }),
  system(gapConfig),
  grid
)
