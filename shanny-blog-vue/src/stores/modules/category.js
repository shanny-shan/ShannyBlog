import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getCategory, insertCategory } from '@/apis/category'

export const useCategoryStore = defineStore('category', () => {
  const categories = ref([])
  const categoryForm = ref({
    name: '文章',
    nameEn: 'Article',
    parentId: 0,
    pathName: 'article',
    sort: 0,
  })
  const resetCategoryForm = () => {
    categoryForm.value = {
      name: '文章',
      nameEn: 'Article',
      parentId: 0,
      pathName: 'article',
      sort: 0,
    }
  }
  const getCategories = async () => {
    const res = await getCategory()
    if (res) {
      categories.value = res.data.data
    }
  }
  const addCategory = async (category) => {
    return await insertCategory(category)
  }
  return {
    categories,
    categoryForm,
    resetCategoryForm,
    getCategories,
    addCategory,
  }
})
