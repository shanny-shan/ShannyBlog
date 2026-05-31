import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  getArticleByType,
  insertArticle,
  updateArticle,
  deleteArticleById,
} from '@/apis/article'
import { useSiteStore } from '@/stores'

export const useArticleStore = defineStore('article', () => {
  const siteStore = useSiteStore()

  const articles = ref([])
  const noteList = ref([])
  const projectList = ref([])
  const articleForm = ref({
    title: '',
    content: '',
    memo: '',
    image: '',
    href: '',
    tags: [],
    categoryId: 0,
    type: 0,
    timelines: [],
    published: true,
  })
  const resetArticleForm = () => {
    articleForm.value = {
      title: '',
      content: '',
      memo: '',
      image: '',
      href: '',
      tags: [],
      categoryId: 0,
      type: 0,
      timelines: [],
      published: true,
    }
  }

  const getArticleByTypeList = async (type) => {
    return await getArticleByType(type)
  }
  const getNoteList = async () => {
    siteStore.loading = true
    const res = await getArticleByTypeList('ARTICLE_NOTE')
    if (res.data.code.toLowerCase() === 'success') {
      noteList.value = res.data.data || []
      siteStore.loading = false
    }
  }

  const getProjectList = async () => {
    siteStore.loading = true
    const res = await getArticleByTypeList('ARTICLE_PROJECT')
    if (res.data.code.toLowerCase() === 'success') {
      projectList.value = res.data.data || []
      siteStore.loading = false
    }
  }
  const addArticle = async (article) => {
    return await insertArticle(article)
  }
  const editArticle = async (article) => {
    return await updateArticle(article)
  }
  const deleteArticle = async (id) => {
    return await deleteArticleById(id)
  }
  return {
    articles,
    noteList,
    projectList,
    articleForm,
    getArticleByTypeList,
    getNoteList,
    getProjectList,
    addArticle,
    resetArticleForm,
    editArticle,
    deleteArticle,
  }
})
