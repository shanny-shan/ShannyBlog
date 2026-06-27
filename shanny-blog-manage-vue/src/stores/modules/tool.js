import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  deleteToolById,
  deleteToolsById,
  getTools,
  insertTool,
  updateTool,
} from '@/apis/tool'
import { useAdminStore, usePageStore, useSiteStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

export const useToolStore = defineStore('tool', () => {
  const toast = useToast()
  const siteStore = useSiteStore()
  const adminStore = useAdminStore()
  const pageStore = usePageStore()

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

  const openEditTool = (item) => {
    adminStore.openDialog('tool')
    toolForm.value = { ...item }
    adminStore.isEdit = true
  }

  const deleteTool = (item) => {
    swal(
      '',
      '',
      `确定删除标题为<span class="text-primary font-bold">${item.title}</span>的工具吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteToolById(item.id)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          await getToolList()
          pageStore.lastPage(toolList.value)
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const deleteTools = () => {
    swal(
      '',
      '',
      `确定删除<span class="text-primary font-bold">${pageStore.selectedIds.length}</span>个工具吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteToolsById(pageStore.selectedIds)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          pageStore.selectedIds = []
          await getToolList()
          pageStore.lastPage(toolList.value)
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const submitTool = async () => {
    siteStore.loading = true

    let res = null
    if (adminStore.isEdit) {
      res = await updateTool(toolForm.value)
    } else {
      res = await insertTool(toolForm.value)
    }

    if (res.data.code.toLowerCase() === 'success') {
      toast.success(`${res.data.msg}`)
      adminStore.closeDialog('tool')
      await getToolList()
    } else {
      toast.error(`${res.data.msg}`)
    }
    siteStore.loading = false
  }

  return {
    tools,
    toolList,
    toolForm,
    resetToolForm,
    getToolList,
    submitTool,
    openEditTool,
    deleteTool,
    deleteTools,
  }
})
