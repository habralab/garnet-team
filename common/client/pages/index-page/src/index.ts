export const getServerSideProps = () => ({
  redirect: {
    permanent: false,
    destination: '/profile',
  },
})

export default () => null
