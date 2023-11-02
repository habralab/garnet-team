import { FormErrors }         from '../helpers'
import { FormUserErrorsText } from './form-user.interfaces'

export const defaultErrorsText: FormUserErrorsText = {
  name: '',
  surname: '',
  description: '',
  tags: '',
  avatar: '',
}

export const formUserErrors: FormErrors = {
  name: {
    noValue: { text: 'shared_ui.form.provide_name' },
    minValue: { text: 'shared_ui.form.minimum_symbols', number: 3 },
  },
  surname: {
    noValue: { text: 'shared_ui.form.provide_surname' },
    minValue: { text: 'shared_ui.form.minimum_symbols', number: 3 },
  },
  description: {
    noValue: { text: 'shared_ui.form.provide_description' },
    minValue: { text: 'shared_ui.form.minimum_symbols', number: 150 },
    maxValue: { text: 'shared_ui.form.maximum_symbols', number: 1000 },
  },
  tags: {
    minValue: { text: 'shared_ui.form.minimum_skills', number: 1 },
    maxValue: { text: 'shared_ui.form.maximum_skills', number: 24 },
  },
}
