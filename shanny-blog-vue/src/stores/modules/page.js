import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const usePageStore = defineStore('page', () => {
  const currentPage = ref(1)
  const itemsPerPage = ref(4)

  const getPageData = (listFun) => {
    const totalPages = computed(() =>
      Math.ceil(listFun().length / itemsPerPage.value),
    )

    const pageList = computed(() => {
      const start = (currentPage.value - 1) * itemsPerPage.value
      const end = start + itemsPerPage.value
      return listFun().slice(start, end)
    })

    return { totalPages, pageList }
  }

  const handlePageChange = (page) => {
    currentPage.value = page
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  const resetPage = () => {
    currentPage.value = 1
  }

  return {
    currentPage,
    itemsPerPage,
    getPageData,
    handlePageChange,
    resetPage,
  }
})
