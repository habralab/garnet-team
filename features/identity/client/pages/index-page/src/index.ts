export const getServerSideProps = () => ({
  redirect: {
    permanent: false,
    destination: '/auth/login',
  },
})

export default () => null
