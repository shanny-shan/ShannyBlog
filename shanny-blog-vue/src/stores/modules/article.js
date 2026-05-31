import { defineStore } from 'pinia'
import {
  getArticleByRecent,
  getArticleByType,
  getArticleById,
  getArticleByViews,
} from '@/apis/article'

export const useArticleStore = defineStore('article', () => {
  const getRecents = async () => {
    return await getArticleByRecent()
  }

  const getViews = async () => {
    return await getArticleByViews()
  }

  const getArticleByTypes = async (type) => {
    return await getArticleByType(type)
  }

  const getArticleByIds = async (id) => {
    return await getArticleById(id)
  }

  return {
    getRecents,
    getViews,
    getArticleByTypes,
    getArticleByIds,
  }
})
