import { defineStore } from 'pinia'
import { getToolAll} from '@/apis/tool'

export const useToolStore = defineStore('tool', () => {
  const getTools = async () => {
    return await getToolAll()
  }

  return {
    getTools,
  }
})
