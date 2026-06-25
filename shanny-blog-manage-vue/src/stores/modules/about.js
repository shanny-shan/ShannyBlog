import { defineStore } from 'pinia'
import { ref } from 'vue'
import {
  deleteAboutById,
  deleteAboutsById,
  getAbout,
  insertAbout,
  updateAbout,
} from '@/apis/about'
import { useSiteStore, useAdminStore, usePageStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

export const useAboutStore = defineStore('about', () => {
  const toast = useToast()
  const siteStore = useSiteStore()
  const adminStore = useAdminStore()
  const pageStore = usePageStore()

  const authors = ref([])
  const aboutList = ref([])
  const authorInfo = ref({})
  const aboutForm = ref({
    avatar: '',
    name: '',
    introduce: '',
    tag: '',
    github: '',
    steam: '',
    web: '',
    biliBili: '',
    isActive: false,
    other: '',
  })
  const resetAboutForm = () => {
    aboutForm.value = {
      avatar: '',
      name: '',
      introduce: '',
      tag: '',
      github: '',
      steam: '',
      web: '',
      biliBili: '',
      other: '',
    }
  }

  const getAboutList = async () => {
    siteStore.loading = true
    const res = await getAbout()
    if (res.data.code.toLowerCase() === 'success') {
      aboutList.value = res.data.data || []
      siteStore.loading = false
    }
  }

  const openEditAbout = (item) => {
    adminStore.openDialog('about')
    aboutForm.value = { ...item }
    adminStore.isEdit = true
  }
  const deleteAbout = (item) => {
    swal(
      '',
      '',
      `确定删除名称为<span class="text-primary font-bold">${item.name}</span>的个人信息吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteAboutById(item.id)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          await getAboutList()
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const deleteAbouts = () => {
    swal(
      '',
      '',
      `确定删除<span class="text-primary font-bold">${pageStore.selectedIds.length}</span>条信息吗？`,
      'question',
      true,
      true,
    ).then(async (result) => {
      if (result.isConfirmed) {
        const res = await deleteAboutsById(pageStore.selectedIds)
        if (res.data.code.toLowerCase() === 'success') {
          toast.success(`${res.data.msg}`)
          pageStore.selectedIds = []
          await getAboutList()
        } else {
          toast.error(`${res.data.msg}`)
        }
      }
    })
  }

  const submitAbout = async (about) => {
    siteStore.loading = true
    let res = null
    if (adminStore.isEdit) {
      res = await updateAbout(about)
    } else {
      res = await insertAbout(about)
    }

    if (res?.data?.code.toLowerCase() == 'success') {
      toast.success(`${res.data.msg}`)
      adminStore.closeDialog('about')
      await getAboutList()
    } else {
      toast.error(`${res.data.msg}`)
    }

    siteStore.loading = false
  }

  return {
    authors,
    aboutList,
    authorInfo,
    aboutForm,
    resetAboutForm,
    submitAbout,
    openEditAbout,
    deleteAbout,
    deleteAbouts,
    getAboutList,
  }
})
