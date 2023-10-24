export const getServerSideProps = () => ({
  redirect: {
    permanent: false,
    destination: '/user',
  },
})

export default () => null
