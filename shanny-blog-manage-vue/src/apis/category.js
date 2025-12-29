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

export { getCategory, insertCategory }
