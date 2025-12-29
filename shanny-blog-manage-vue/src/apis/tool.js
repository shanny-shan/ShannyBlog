import request from '@/utils/request'

const getTools = () => {
  return request({
    url: '/tool/all',
    method: 'Get',
  })
}
const insertTool = (tool) => {
  return request({
    url: '/tool/add',
    method: 'Post',
    data: tool,
  })
}

export { getTools, insertTool }
