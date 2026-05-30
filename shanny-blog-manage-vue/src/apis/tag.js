import request from '@/utils/request'

const getTags = () => {
  return request({
    url: '/tag/all',
    method: 'Get',
  })
}
const getTagsById = (id) => {
  return request({
    url: '/tag/id',
    method: 'Get',
    params: {
      id,
    },
  })
}
const insertTag = (tag) => {
  return request({
    url: '/tag/add',
    method: 'Post',
    data: tag,
  })
}

const updateTag = (tag) => {
  return request({
    url: '/tag/update',
    method: 'Post',
    data: tag,
  })
}
const deleteTagById = (id) => {
  return request({
    url: '/tag/delete',
    method: 'Post',
    params: {
      id,
    },
  })
}

export { getTags, getTagsById, insertTag, updateTag, deleteTagById }
