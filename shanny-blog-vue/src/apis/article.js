import request from '@/utils/request'

const getArticleByRecent = () => {
  return request({
    url: '/article/recent',
    method: 'Get',
  })
}

const getArticleByViews = () => {
  return request({
    url: '/article/views',
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
export {
  getArticleByRecent,
  getArticleByViews,
  getArticleByType,
  getArticleById,
}
