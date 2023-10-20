import React from 'react'

const getServerSideProps = (_) => {
  return {
    redirect: {
      destination: '/auth/login', permanent: true
    }
  }
}

export default getServerSideProps
