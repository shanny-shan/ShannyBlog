import { onMounted, ref } from 'vue'
import { defineStore } from 'pinia'

export const useSiteStore = defineStore('site', () => {
  const loading = ref(false)
  const drawerOpen = ref(false)
  const isMobile = ref(false)
  const checkMobile = () => {
    const result = navigator.userAgent.match(
      /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i,
    )
    return !!result
  }
  onMounted(() => {
    isMobile.value = checkMobile()
  })
  return {
    loading,
    drawerOpen,
    isMobile,
  }
})
