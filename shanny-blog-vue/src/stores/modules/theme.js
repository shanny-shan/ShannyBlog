import { ref, watch } from 'vue'
import { setCookie, getCookie } from '@/utils/cookie'
import { defineStore } from 'pinia'

export const useThemeStore = defineStore('theme', () => {
  const currentTheme = ref('pink-light')
  const currentThemeState = ref(true)
  const changeTheme = () => {
    currentTheme.value = currentThemeState.value ? 'pink-light' : 'dark'
    setCookie('theme', currentTheme.value, '1d')
  }
  const initTheme = () => {
    currentThemeState.value = getCookie('theme') == 'dark' ? false : true
  }
  watch(currentThemeState, () => {
    changeTheme()
  })
  return {
    currentTheme,
    currentThemeState,
    initTheme,
  }
})
