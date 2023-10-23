import dynamic from 'next/dynamic'

// @ts-ignore
const RegistrationPage = dynamic(() => import('identity/auth/registration'), { ssr: true })

export default RegistrationPage
