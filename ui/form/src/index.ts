import styled       from '@emotion/styled'

import { position } from 'styled-system'
import { flexbox }  from 'styled-system'
import { space }    from 'styled-system'
import { layout }   from 'styled-system'

const Form = styled('form')(layout, space, flexbox, position)

Form.defaultProps = {
  display: 'flex',
  boxSizing: 'border-box',
}

export { Form }
