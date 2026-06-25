import request from '@/utils/request'

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
const updateArticle = (article) => {
  return request({
    url: '/article/update',
    method: 'Post',
    data: article,
  })
}
const deleteArticleById = (id) => {
  return request({
    url: '/article/delete',
    method: 'Post',
    params: {
      id,
    },
  })
}
const deleteArticlesById = (ids) => {
  return request({
    url: '/article/deleteAll',
    method: 'Post',
    data: {
      ids,
    },
  })
}

export {
  getArticleByType,
  insertArticle,
  updateArticle,
  deleteArticleById,
  deleteArticlesById,
}
