import { JoinRequest } from '../data'

export interface JoinRequestsProps {
  joinRequests: JoinRequest[]
  refetch: () => void
}
