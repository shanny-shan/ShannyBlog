import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  deleteAboutById,
  getAbout,
  insertAbout,
  updateAbout,
} from '@/apis/about'
import { useSiteStore } from '@/stores'

export const useAboutStore = defineStore('about', () => {
  const siteStore = useSiteStore()

  const authors = ref([])
  const aboutList = ref([])
  const authorInfo = ref({})
  const aboutForm = ref({
    avatar: '',
    name: '',
    introduce: '',
    tag: '',
    github: '',
    steam: '',
    web: '',
    biliBili: '',
    isActive: false,
    other: '',
  })
  const resetAboutForm = () => {
    aboutForm.value = {
      avatar: '',
      name: '',
      introduce: '',
      tag: '',
      github: '',
      steam: '',
      web: '',
      biliBili: '',
      other: '',
    }
  }

  const getAboutList = async () => {
    siteStore.loading = true
    const res = await getAbout()
    if (res.data.code.toLowerCase() === 'success') {
      aboutList.value = res.data.data || []
      siteStore.loading = false
    }
  }

  const addAbout = async (about) => {
    return await insertAbout(about)
  }
  const editAbout = async (about) => {
    return await updateAbout(about)
  }
  const deleteAbout = async (id) => {
    return await deleteAboutById(id)
  }

  return {
    authors,
    aboutList,
    authorInfo,
    aboutForm,
    resetAboutForm,
    addAbout,
    editAbout,
    deleteAbout,
    getAboutList,
  }
})
