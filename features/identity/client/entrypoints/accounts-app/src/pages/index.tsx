import React from 'react'

const getServerSideProps = (_) => {
  return { redirect: { destination: '/auth/registration', permanent: true } }
}

export default getServerSideProps
