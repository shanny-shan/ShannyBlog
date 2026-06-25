import { defineStore } from 'pinia'
import { getWebInfo } from '@/apis/project'
import { ref } from 'vue'

export const useWebInfoStore = defineStore('webInfo', () => {
  const webInfo = ref({})
  const getWebInfos = async () => {
    const res = await getWebInfo()
    if (res.data.code.toLowerCase() === 'success') {
      webInfo.value = res.data.data
    }
  }

  return {
    webInfo,
    getWebInfos,
  }
})
