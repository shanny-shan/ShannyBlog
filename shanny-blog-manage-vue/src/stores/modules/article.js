import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getArticles, getArticleByType, insertArticle } from '@/apis/article'

export const useArticleStore = defineStore('article', () => {
  const articles = ref([])
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
  const getArticleList = async () => {
    return await getArticles()
  }
  const getArticleByTypeList = async (type) => {
    return await getArticleByType(type)
  }
  const addArticle = async (article) => {
    return await insertArticle(article)
  }
  const editArticle = async (article) => {}
  return {
    articles,
    articleForm,
    getArticleList,
    getArticleByTypeList,
    addArticle,
    resetArticleForm,
    editArticle,
  }
})
