import dynamic from 'next/dynamic'

// @ts-ignore
export default dynamic(() => import('identity/auth/login'), { ssr: false })
