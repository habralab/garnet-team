import { ketoPermission } from '../sdk'

export const PermissionNode = () => {
  ketoPermission.checkPermissionOrError({})
}
