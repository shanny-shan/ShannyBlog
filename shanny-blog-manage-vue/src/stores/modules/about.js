import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getAbout, insertAbout } from '@/apis/about'

export const useAboutStore = defineStore('about', () => {
  const authors = ref([])
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
  const getAboutMe = async () => {
    return await getAbout()
  }
  const addAbout = async (about) => {
    return await insertAbout(about)
  }
  return {
    authors,
    authorInfo,
    aboutForm,
    resetAboutForm,
    getAboutMe,
    addAbout,
  }
})
