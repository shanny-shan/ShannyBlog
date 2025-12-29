import request from '@/utils/request'

const getArticles = () => {
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
const insertArticle = (article) => {
  return request({
    url: '/article/add',
    method: 'Post',
    data: article,
  })
}

export { getArticles, getArticleByType, insertArticle }
