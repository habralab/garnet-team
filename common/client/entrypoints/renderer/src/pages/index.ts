import dynamic from 'next/dynamic'

// @ts-ignore
const IndexPage = dynamic(() => import('project/index-page'), { ssr: false })

export default IndexPage
