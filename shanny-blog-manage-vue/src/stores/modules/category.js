import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getCategory, insertCategory } from '@/apis/category'

export const useCategoryStore = defineStore('category', () => {
  const categories = ref([])
  const categoryForm = ref({
    name: '类别1',
    nameEn: 'category1',
    type: 0,
    sort: 0,
  })
  const categoryType = ref({
    0: '笔记',
    1: '项目',
    2: 'Bug',
    3: '书籍',
    4: '照片',
    5: '视频',
    6: '音乐',
    7: '工具',
    8: '通知',
  })
  const resetCategoryForm = () => {
    categoryForm.value = {
      name: '类别1',
      nameEn: 'category1',
      type: 0,
      sort: 0,
    }
  }
  const getCategories = async () => {
    return await getCategory()
  }
  const addCategory = async (category) => {
    return await insertCategory(category)
  }
  return {
    categories,
    categoryForm,
    categoryType,
    resetCategoryForm,
    getCategories,
    addCategory,
  }
})
