import { ref } from 'vue'
import { defineStore } from 'pinia'
export const useScrollStore = defineStore('scroll', () => {
  const isScrolled = ref(false)
  const handleScroll = () => {
    isScrolled.value = window.scrollY > 0
  }
  const enableScrollListener = () => {
    window.addEventListener('scroll', handleScroll)
  }
  const disableScrollListener = () => {
    window.removeEventListener('scroll', handleScroll)
  }
  const resetScrollState = () => {
    isScrolled.value = false
  }
  return {
    isScrolled,
    enableScrollListener,
    disableScrollListener,
    resetScrollState,
  }
})
