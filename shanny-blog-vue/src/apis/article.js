import request from '@/utils/request'

const getArticleAll = () => {
  return request({
    url: '/article/all',
    method: 'Get',
  })
}

const getArticleByType = (type) => {
  return request({
    url: '/article/type',
    method: 'Get',
    params: {
      type,
    },
  })
}

const getArticleById = (id) => {
  return request({
    url: '/article/id',
    method: 'Get',
    params: {
      id,
    },
  })
}
export { getArticleAll, getArticleByType, getArticleById }
