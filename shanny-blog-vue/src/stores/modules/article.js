import { defineStore } from 'pinia'
import {
  getArticleByRecent,
  getArticleByType,
  getArticleById,
  getArticleByViews,
  getArticleByTag,
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
  const getArticleByTags = async (tagId) => {
    return await getArticleByTag(tagId)
  }

  const getArticleByIds = async (id) => {
    return await getArticleById(id)
  }

  return {
    getRecents,
    getViews,
    getArticleByTypes,
    getArticleByIds,
    getArticleByTags,
  }
})
