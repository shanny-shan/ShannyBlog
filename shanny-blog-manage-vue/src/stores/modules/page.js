import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export const usePageStore = defineStore('page', () => {
  const currentPage = ref(1)
  const itemsPerPage = ref(5)
  const selectedIds = ref([])

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
    selectedIds.value = []
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }

  const lastPage = (list) => {
    const { pageList } = getPageData(() => list)
    if (pageList.value.length == 0 && currentPage.value > 1) {
      currentPage.value--
      handlePageChange(currentPage.value)
    }
  }

  const resetPage = () => {
    currentPage.value = 1
  }

  const toggleAllRows = (list, key = 'id') => {
    const rows = list.value
    if (key == 'id') {
      const allChecked = rows.every((item) =>
        selectedIds.value.includes(item.id),
      )
      if (allChecked) {
        rows.forEach((item) => {
          const i = selectedIds.value.indexOf(item.id)
          if (i > -1) selectedIds.value.splice(i, 1)
        })
      } else {
        rows.forEach((item) => {
          if (!selectedIds.value.includes(item.id)) {
            selectedIds.value.push(item.id)
          }
        })
      }
    } else if (key == 'uuid') {
      const allChecked = rows.every((item) =>
        selectedIds.value.includes(item.uuid),
      )
      if (allChecked) {
        rows.forEach((item) => {
          const i = selectedIds.value.indexOf(item.uuid)
          if (i > -1) selectedIds.value.splice(i, 1)
        })
      } else {
        rows.forEach((item) => {
          if (!selectedIds.value.includes(item.uuid)) {
            selectedIds.value.push(item.uuid)
          }
        })
      }
    }
  }

  const isAllRowsChecked = (list, key = 'id') => {
    const rows = list.value
    if (!rows.length) return false
    if (key == 'id') {
      return rows.every((item) => selectedIds.value.includes(item.id))
    } else if (key == 'uuid') {
      return rows.every((item) => selectedIds.value.includes(item.uuid))
    }
  }

  return {
    currentPage,
    itemsPerPage,
    getPageData,
    handlePageChange,
    resetPage,
    lastPage,

    selectedIds,
    toggleAllRows,
    isAllRowsChecked,
  }
})
