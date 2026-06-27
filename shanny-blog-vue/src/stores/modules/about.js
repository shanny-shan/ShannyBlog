import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getAbout, insertAbout } from '@/apis/about'

export const useAboutStore = defineStore('about', () => {
  const authors = ref([])
  const authorInfo = ref({})
  const aboutForm = ref({
    avatar:
      'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTLdmxGOt3RE7fkLaZJBqqy7QieJTJYjpwswQ&s',
    name: 'shanny',
    introduce: '前端开发工程师、游戏开发工程师......',
    tag: '前端开发工程师;游戏开发工程师',
    github: 'https://github.com/shanny-shan?tab=repositories',
    steam: '',
    web: '',
    biliBili: '',
    isActive: false,
    other: '',
  })
  const resetAboutForm = () => {
    aboutForm.value = {
      avatar:
        'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTLdmxGOt3RE7fkLaZJBqqy7QieJTJYjpwswQ&s',
      name: 'shanny',
      introduce: '前端开发工程师、游戏开发工程师......',
      tag: '前端开发工程师;游戏开发工程师',
      github: 'https://github.com/shanny-shan?tab=repositories',
      steam: '',
      web: '',
      biliBili: '',
      other: '',
    }
  }

  const addAbout = async (about) => {
    return await insertAbout(about)
  }

  const getAboutMe = async () => {
    const res = await getAbout()
    if (res.data.code.toLowerCase() === 'success') {
      authorInfo.value = res.data.data
    }
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
