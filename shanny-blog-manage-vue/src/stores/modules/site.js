import { onMounted, ref } from 'vue'
import { defineStore } from 'pinia'
import { usePageStore } from '@/stores'

export const useSiteStore = defineStore('site', () => {
  const pageStore = usePageStore()

  const loading = ref(false)
  const drawerOpen = ref(false)
  const isMobile = ref(false)
  const active = ref('note')

  const checkMobile = () => {
    const result = navigator.userAgent.match(
      /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i,
    )
    return !!result
  }
  const goWebSite = () => {
    window.open('https://www.shanny.work', '_blank')
  }
  const changeHeader = (e) => {
    active.value = e
    pageStore.selectedIds = []
  }
  const changeHeaderM = (e) => {
    changeHeader(e)
    drawerOpen.value = false
  }

  onMounted(() => {
    isMobile.value = checkMobile()
  })
  return {
    loading,
    drawerOpen,
    isMobile,
    goWebSite,
    active,
    changeHeader,
    changeHeaderM,
  }
})
