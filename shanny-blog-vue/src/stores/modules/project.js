import { defineStore } from 'pinia'
import { getProjectInfo } from '@/apis/project'

export const useProjectInfoStore = defineStore('projectInfo', () => {
  const getProjectInfos = async () => {
    return await getProjectInfo()
  }

  return {
    getProjectInfos,
  }
})
