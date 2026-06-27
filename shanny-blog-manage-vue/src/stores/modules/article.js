import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  getArticleByType,
  insertArticle,
  updateArticle,
  deleteArticleById,
  deleteArticlesById,
} from '@/apis/article'
import {
  useAdminStore,
  useCategoryStore,
  usePageStore,
  useSiteStore,
} from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

export const useArticleStore = defineStore('article', () => {
  const toast = useToast()
  const siteStore = useSiteStore()
  const categoryStore = useCategoryStore()
  const adminStore = useAdminStore()
  const pageStore = usePageStore()

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

  const getArticleList = async (categorey) => {
    siteStore.loading = true
    const res = await getArticleByType(categorey)
    if (res.data.code.toLowerCase() === 'success') {
      if (categorey == 'ARTICLE_PROJECT') {
        projectList.value = res.data.data || []
      } else {
        noteList.value = res.data.data || []
      }

      siteStore.loading = false
    }
  }

  const openEditArticle = (item, type) => {
    adminStore.openDialog(type)
    articleForm.value = { ...item }
    adminStore.isEdit = true
  }

  const deleteArticle = (item, categorey) => {
    swal(
      '',
      '',
      `确定删除标题为<span class="text-primary font-bold">${item.title}</span>的笔记吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteArticleById(item.id)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          await getArticleList(categorey)
          if (categorey == 'ARTICLE_PROJECT') {
            pageStore.lastPage(projectList.value)
          } else {
            pageStore.lastPage(noteList.value)
          }
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }
  const deleteArticles = (category) => {
    swal(
      '',
      '',
      `确定删除<span class="text-primary font-bold">${pageStore.selectedIds.length}</span>条笔记吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteArticlesById(pageStore.selectedIds)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          pageStore.selectedIds = []
          await getArticleList(category)
          if (categorey == 'ARTICLE_PROJECT') {
            pageStore.lastPage(projectList.value)
          } else {
            pageStore.lastPage(noteList.value)
          }
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const submitArticle = async (categorey) => {
    siteStore.loading = true
    articleForm.value.type = categorey

    if (articleForm.value.content.trim() == '') {
      toast.error('文章内容不能为空！')
      siteStore.loading = false
      return
    }

    let res = null

    if (adminStore.isEdit) {
      res = await updateArticle(articleForm.value)
    } else {
      res = await insertArticle(articleForm.value)
    }
    if (res != null) {
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        if (categorey == 'ARTICLE_PROJECT') {
          closeDialog('project', categorey)
        } else {
          closeDialog('note', categorey)
        }
        await getArticleList(categorey)
      } else {
        toast.error(`${res.data.msg}`)
      }
    }

    siteStore.loading = false
  }
  const closeDialog = (type, categorey) => {
    adminStore.closeDialog(type)
    categoryStore.getCategoryId(categorey)
  }

  return {
    articles,
    noteList,
    projectList,
    articleForm,
    getArticleList,
    resetArticleForm,
    submitArticle,
    openEditArticle,
    deleteArticle,
    deleteArticles,
    closeDialog,
  }
})
