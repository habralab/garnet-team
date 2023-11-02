export const checkDisabled = (
  formValues: Record<string, string | string[]>,
  errorsText: Record<string, string>
): boolean =>
  Object.values(formValues).some((value) => value.length === 0) ||
  Object.values(errorsText).some((value) => value !== '')
