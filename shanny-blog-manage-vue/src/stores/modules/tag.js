import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  getTags,
  insertTag,
  getTagsById,
  updateTag,
  deleteTagById,
  deleteTagsById,
} from '@/apis/tag'

import { useAdminStore, usePageStore, useSiteStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

export const useTagStore = defineStore('tag', () => {
  const toast = useToast()
  const siteStore = useSiteStore()
  const adminStore = useAdminStore()
  const pageStore = usePageStore()

  const tags = ref([])
  const tagList = ref([])
  const tagForm = ref({
    name: '',
    nameEn: '',
  })
  const resetTagForm = () => {
    tagForm.value = {
      name: '',
      nameEn: '',
    }
  }

  const getTagList = async () => {
    siteStore.loading = true
    const res = await getTags()
    if (res.data.code.toLowerCase() === 'success') {
      tagList.value = res.data.data || []
      siteStore.loading = false
    }
  }
  const getTagAll = async () => {
    const tagResult = await getTags()
    if (tagResult.data.code.toLowerCase() === 'success') {
      tags.value = tagResult.data.data
    }
  }
  const getTagByIdList = async () => {
    return await getTagsById()
  }
  const openEditTag = (item) => {
    adminStore.openDialog('tag')
    tagForm.value = { ...item }
    adminStore.isEdit = true
  }

  const deleteTag = (item) => {
    swal(
      '',
      '',
      `确定删除名称为<span class="text-primary font-bold">${item.name}</span>的标签吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteTagById(item.id)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          await getTagList()
          pageStore.lastPage(tagList.value)
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const deleteTags = () => {
    swal(
      '',
      '',
      `确定删除<span class="text-primary font-bold">${pageStore.selectedIds.length}</span>条标签吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteTagsById(pageStore.selectedIds)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          pageStore.selectedIds = []
          await getTagList()
          pageStore.lastPage(tagList.value)
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const submitTag = async () => {
    siteStore.loading = true

    let res = null
    if (adminStore.isEdit) {
      res = await updateTag(tagForm.value)
    } else {
      res = await insertTag(tagForm.value)
    }
    if (res.data.code.toLowerCase() === 'success') {
      toast.success(`${res.data.msg}`)
      adminStore.closeDialog('tag')
      await getTagList()
    } else {
      toast.error(`${res.data.msg}`)
    }

    siteStore.loading = false
  }

  return {
    tags,
    tagList,
    tagForm,
    resetTagForm,
    getTagList,
    getTagAll,
    getTagByIdList,
    openEditTag,
    deleteTag,
    deleteTags,
    submitTag,
  }
})
