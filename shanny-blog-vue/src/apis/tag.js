import request from '@/utils/request'

const getTags = () => {
  return request({
    url: '/tag/all',
    method: 'Get',
  })
}
const insertTag = (tag) => {
  return request({
    url: '/tag/add',
    method: 'Post',
    data: tag,
  })
}

export { getTags, insertTag }
