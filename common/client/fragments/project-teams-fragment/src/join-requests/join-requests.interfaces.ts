import { MockJoinRequest } from '../data'

export interface JoinRequestsProps {
  joinRequests: MockJoinRequest[]
  refetch: () => void
}
