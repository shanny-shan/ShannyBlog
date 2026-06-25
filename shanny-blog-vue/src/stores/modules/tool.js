import { defineStore } from 'pinia'
import { getToolAll } from '@/apis/tool'
import { useSiteStore } from '@/stores'
import { ref } from 'vue'

export const useToolStore = defineStore('tool', () => {
  const toolList = ref([])
  const siteStore = useSiteStore()

  const getToolList = async () => {
    siteStore.loading = true
    const res = await getToolAll()
    if (res.data.code.toLowerCase() === 'success') {
      toolList.value = res.data.data
      siteStore.loading = false
    }
  }

  return {
    toolList,
    getToolList,
  }
})
