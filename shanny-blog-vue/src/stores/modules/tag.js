import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getTags, insertTag } from '@/apis/tag'

export const useTagStore = defineStore('tag', () => {
  const tags = ref([])

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
    const res = await getTags()
    if (res.data.code.toLowerCase() === 'success') {
      tags.value = res.data.data
    }
  }

  const addTag = async (tag) => {
    return await insertTag(tag)
  }
  return {
    tags,
    tagForm,
    resetTagForm,
    getTagList,
    addTag,
  }
})
