import { defineStore } from 'pinia'
import { ref } from 'vue'
import { deleteToolById, getTools, insertTool, updateTool } from '@/apis/tool'
import { useSiteStore } from '@/stores'

export const useToolStore = defineStore('tool', () => {
  const siteStore = useSiteStore()

  const tools = ref([])
  const toolList = ref([])
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
    siteStore.loading = true
    const res = await getTools()
    if (res.data.code.toLowerCase() === 'success') {
      toolList.value = res.data.data || []
      siteStore.loading = false
    }
  }

  const addTool = async (tool) => {
    return await insertTool(tool)
  }
  const editTool = async (tool) => {
    return await updateTool(tool)
  }
  const deleteTool = async (id) => {
    return await deleteToolById(id)
  }

  return {
    tools,
    toolList,
    toolForm,
    resetToolForm,
    getToolList,
    addTool,
    editTool,
    deleteTool,
  }
})
