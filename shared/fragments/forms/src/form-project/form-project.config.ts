import { FormErrors }            from '../helpers'
import { FormProjectErrorsText } from './form-project.interfaces'

export const defaultErrorsText: FormProjectErrorsText = {
  avatar: '',
  description: '',
  name: '',
  tags: '',
}

export const formProjectErrors: FormErrors = {
  name: {
    noValue: { text: 'shared_ui.form.provide_naming' },
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
