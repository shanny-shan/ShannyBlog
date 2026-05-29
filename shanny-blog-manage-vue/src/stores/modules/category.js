import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getCategory, insertCategory } from '@/apis/category'

export const useCategoryStore = defineStore('category', () => {
  const categories = ref([])
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
