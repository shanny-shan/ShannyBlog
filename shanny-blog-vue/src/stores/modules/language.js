import { ref, watch } from 'vue'
import { setCookie, getCookie } from '@/utils/cookie.js'
import { defineStore } from 'pinia'
export const useLanguageStore = defineStore('language', () => {
  const isEnglish = ref(false)
  const changeLanguage = () => {
    setCookie('language', isEnglish.value, '1d')
  }
  const initLanguage = () => {
    isEnglish.value = getCookie('language') === 'true' ? true : false
  }
  watch(isEnglish, () => {
    changeLanguage()
  })
  return { isEnglish, initLanguage }
})
