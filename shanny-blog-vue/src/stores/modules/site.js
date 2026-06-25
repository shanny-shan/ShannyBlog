import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useSiteStore = defineStore('site', () => {
  const drawerOpen = ref(false)

  const loading = ref(false)
  const loadCount = ref(0)
  const handleLoadComplete = () => {
    loadCount.value++
    if (loadCount.value === 4) {
      loading.value = false
    }
  }
  const resetLoadCount = () => {
    loadCount.value = 0
    loading.value = true
  }

  const curHref = ref('home')
  const openDrawer = () => {
    drawerOpen.value = true
  }
  const closeDrawer = () => {
    drawerOpen.value = false
  }
  const toggleDrawer = () => {
    drawerOpen.value = !drawerOpen.value
  }

  return {
    loading,
    drawerOpen,
    curHref,
    openDrawer,
    closeDrawer,
    toggleDrawer,
    handleLoadComplete,
    resetLoadCount,
  }
})
