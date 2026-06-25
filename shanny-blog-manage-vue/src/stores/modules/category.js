import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  getCategory,
  insertCategory,
  updateCategory,
  deleteCategoryById,
} from '@/apis/category'

import { useSiteStore, useArticleStore, useAdminStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

export const useCategoryStore = defineStore('category', () => {
  const toast = useToast()
  const siteStore = useSiteStore()
  const articleStore = useArticleStore()
  const adminStore = useAdminStore()
  const categorStore = useCategoryStore

  const curCategories = ref([])
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
  const getCategoryList = async () => {
    siteStore.loading = true
    const res = await getCategory()
    if (res.data.code.toLowerCase() === 'success') {
      categoryList.value = res.data.data || []
      siteStore.loading = false
    }
  }

  const openEditCategory = (item) => {
    adminStore.openDialog('category')
    categoryForm.value = { ...item }
    adminStore.isEdit = true
  }
  const deleteCategory = (item) => {
    swal(
      '',
      '',
      `确定删除名称为<span class="text-primary font-bold">${item.name}</span>的类型吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteCategoryById(tem.id)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          await getCategoryList()
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const submitCategory = async () => {
    siteStore.loading = true
    let res = null
    if (adminStore.isEdit) {
      res = await updateCategory(categoryForm.value)
    } else {
      res = await insertCategory(categoryForm.value)
    }

    if (res?.data?.code.toLowerCase() == 'success') {
      toast.success(`${res.data.msg}`)
      adminStore.closeDialog('category')
      await getCategoryList()
    } else {
      toast.error(`${res.data.msg}`)
    }

    siteStore.loading = false
  }
  const getCategoryId = async (categorey) => {
    const categoryResult = await getCategory()
    if (categoryResult.data.code.toLowerCase() === 'success') {
      curCategories.value = categoryResult.data.data.filter(
        (item) => item.type == categorey,
      )
      if (curCategories.value.length > 0) {
        articleStore.articleForm.categoryId = curCategories.value[0].id
      }
    }
  }
  return {
    curCategories,
    categories,
    categoryList,
    categoryForm,
    categoryType,
    resetCategoryForm,
    getCategoryList,
    submitCategory,
    openEditCategory,
    deleteCategory,
    getCategoryId,
  }
})
