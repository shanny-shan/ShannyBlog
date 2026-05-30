import request from '@/utils/request'

const getCategory = () => {
  return request({
    url: '/category/all',
    method: 'Get',
  })
}
const insertCategory = (category) => {
  return request({
    url: '/category/add',
    method: 'POST',
    data: category,
  })
}

const updateCategory = (category) => {
  return request({
    url: '/category/update',
    method: 'Post',
    data: category,
  })
}
const deleteCategoryById = (id) => {
  return request({
    url: '/category/delete',
    method: 'Post',
    params: {
      id,
    },
  })
}

export { getCategory, insertCategory, updateCategory, deleteCategoryById }
