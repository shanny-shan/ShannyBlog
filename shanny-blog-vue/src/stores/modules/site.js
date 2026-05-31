import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useSiteStore = defineStore('site', () => {
  const loading = ref(false)
  return {
    loading,
  }
})
