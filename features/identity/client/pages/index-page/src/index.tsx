export const getServerSideProps = () => ({
  redirect: {
    destination: '/auth/login',
    permanent: false,
  },
})

export default () => null
