import React                from 'react'

import { Seo }              from './seo'
import { VerificationPage } from './verification.page'

export const getServerSideProps = () => ({
  redirect: {
    destination: '/auth/login',
    permanent: true,
  },
})

const Verification = () => (
  <>
    <Seo />
    <VerificationPage />
  </>
)

export default Verification
