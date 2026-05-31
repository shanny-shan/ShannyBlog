import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  getTags,
  insertTag,
  getTagsById,
  updateTag,
  deleteTagById,
} from '@/apis/tag'

import { useSiteStore } from '@/stores'

export const useTagStore = defineStore('tag', () => {
  const siteStore = useSiteStore()

  const tags = ref([])
  const tagList = ref([])
  const tagForm = ref({
    name: '标签1',
    nameEn: 'tag1',
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
    return await getTags()
  }
  const getTagByIdList = async () => {
    return await getTagsById()
  }
  const addTag = async (tag) => {
    return await insertTag(tag)
  }
  const editTag = async (tag) => {
    return await updateTag(tag)
  }
  const deleteTag = async (id) => {
    return await deleteTagById(id)
  }
  return {
    tags,
    tagList,
    tagForm,
    resetTagForm,
    getTagList,
    getTagAll,
    getTagByIdList,
    addTag,
    editTag,
    deleteTag,
  }
})
