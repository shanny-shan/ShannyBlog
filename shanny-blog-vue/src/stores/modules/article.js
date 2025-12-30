import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getArticleAll,getArticleByType,getArticleById } from '@/apis/article'

export const useArticleStore = defineStore('article', () => {
  const getArticles = async () => {
    return await getArticleAll()
  }

  const getArticleByTypes = async (type) => {
    return await getArticleByType(type)
  }

  const getArticleByIds = async (id) => {
    return await getArticleById(id)
  }


  return {
    getArticles,
    getArticleByTypes,
    getArticleByIds
  }
})
