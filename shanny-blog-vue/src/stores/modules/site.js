import { onMounted, ref } from 'vue'
import { defineStore } from 'pinia'

export const useSiteStore = defineStore('site', () => {
  const loading = ref(false)
  const drawerOpen = ref(false)
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
  }
})
