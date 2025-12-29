import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getTools, insertTool } from '@/apis/tool'

export const useToolStore = defineStore('tool', () => {
  const tools = ref([])
  const toolForm = ref({
    title: '',
    content: '',
    href: '',
    image: '',
    tags: [],
    published: true,
  })
  const resetToolForm = () => {
    toolForm.value = {
      title: '',
      content: '',
      href: '',
      image: '',
      tags: [],
      published: true,
    }
  }
  const getToolList = async () => {
    return await getTools()
  }
  const addTool = async (tool) => {
    return await insertTool(tool)
  }

  return {
    tools,
    toolForm,
    resetToolForm,
    getToolList,
    addTool,
  }
})
