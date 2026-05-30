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
const updateTool = (tool) => {
  return request({
    url: '/tool/update',
    method: 'Post',
    data: tool,
  })
}
const deleteToolById = (id) => {
  return request({
    url: '/tool/delete',
    method: 'Post',
    params: {
      id,
    },
  })
}

export { getTools, insertTool, updateTool, deleteToolById }
