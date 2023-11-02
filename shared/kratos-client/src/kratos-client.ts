import { KratosClient } from '@atls/next-identity-integration'

export const kratosClient = new KratosClient({ basePath: process.env.NEXT_PUBLIC_KRATOS_URL })
