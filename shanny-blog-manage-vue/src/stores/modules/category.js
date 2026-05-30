import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  getCategory,
  insertCategory,
  updateCategory,
  deleteCategoryById,
} from '@/apis/category'

import { useSiteStore } from '@/stores'

export const useCategoryStore = defineStore('category', () => {
  const siteStore = useSiteStore()

  const categories = ref([])
  const categoryList = ref([])
  const categoryForm = ref({
    name: '',
    nameEn: '',
    type: 'ARTICLE_NOTE',
    sort: 0,
  })
  const categoryType = ref({
    ARTICLE_NOTE: '笔记',
    ARTICLE_PROJECT: '项目',
    // 2: 'Bug',
    // 3: '书籍',
    // 4: '照片',
    // 5: '视频',
    // 6: '音乐',
    TOOL: '工具',
    // 8: '通知',
  })
  const resetCategoryForm = () => {
    categoryForm.value = {
      name: '',
      nameEn: '',
      type: 0,
      sort: 0,
    }
  }
  const getCategories = async () => {
    return await getCategory()
  }

  const getCategoryList = async () => {
    siteStore.loading = true
    const res = await getCategory()
    if (res.data.code.toLowerCase() === 'success') {
      categoryList.value = res.data.data || []
      siteStore.loading = false
    }
  }

  const addCategory = async (category) => {
    return await insertCategory(category)
  }
  const editCategory = async (category) => {
    return await updateCategory(category)
  }
  const deleteCategory = async (id) => {
    return await deleteCategoryById(id)
  }
  return {
    categories,
    categoryList,
    categoryForm,
    categoryType,
    resetCategoryForm,
    getCategories,
    getCategoryList,
    addCategory,
    editCategory,
    deleteCategory,
  }
})
