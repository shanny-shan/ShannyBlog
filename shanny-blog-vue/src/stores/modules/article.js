import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  getArticleByRecent,
  getArticleByType,
  getArticleById,
  getArticleByViews,
  getArticleByTag,
} from '@/apis/article'

import { useSiteStore } from '@/stores'

export const useArticleStore = defineStore('article', () => {
  const siteStore = useSiteStore()

  const article = ref({})
  const projectList = ref([])
  const noteList = ref([])
  const viewList = ref([])
  const recentList = ref([])
  const tagList = ref([])

  const getTagArticleList = async (tagId) => {
    siteStore.loading = true
    const res = await getArticleByTag(tagId)
    if (res?.data?.code.toLowerCase() === 'success') {
      tagList.value = res.data.data
      siteStore.loading = false
    }
  }

  const getRecentList = async () => {
    const res = await getArticleByRecent()
    if (res.data.code.toLowerCase() === 'success') {
      recentList.value = res.data.data
    }
  }

  const getViewList = async () => {
    const res = await getArticleByViews()
    if (res.data.code.toLowerCase() === 'success') {
      viewList.value = res.data.data
    }
  }

  const getArticleByTypes = async (type) => {
    siteStore.loading = true
    const res = await getArticleByType(type)
    if (res.data.code.toLowerCase() === 'success') {
      if (type == 'ARTICLE_PROJECT') {
        projectList.value = res.data.data
      } else {
        noteList.value = res.data.data
      }
      siteStore.loading = false
    }
  }

  const getArticleDetail = async (id) => {
    const res = await getArticleById(id)
    if (res.data.code.toLowerCase() == 'success') {
      article.value = res.data.data
      siteStore.loading = false
    }
  }

  return {
    article,
    projectList,
    noteList,
    recentList,
    tagList,
    viewList,
    getRecentList,
    getViewList,
    getArticleByTypes,
    getArticleDetail,
    getTagArticleList,
  }
})
