import { ref } from 'vue'
import { defineStore } from 'pinia'
import { useCategoryStore } from './category'
import { useAboutStore } from './about'
import { useTagStore } from './tag'
export const useAdminStore = defineStore('admin', () => {
  const categoryStore = useCategoryStore()
  const aboutStore = useAboutStore()
  const tagStore = useTagStore()

  const noteDialog = ref(false)
  const projectDialog = ref(false)
  const toolDialog = ref(false)
  const mediaDialog = ref(false)
  const bugDialog = ref(false)
  const boardDialog = ref(false)
  const accountDialog = ref(false)
  const infoDialog = ref(false)
  const categoryDialog = ref(false)
  const aboutDialog = ref(false)
  const tagDialog = ref(false)
  const openDialog = (type) => {
    switch (type) {
      case 'note':
        noteDialog.value = true
        break
      case 'project':
        projectDialog.value = true
        break
      case 'tool':
        toolDialog.value = true
        break
      case 'media':
        mediaDialog.value = true
        break
      case 'bug':
        bugDialog.value = true
        break
      case 'board':
        boardDialog.value = true
        break
      case 'account':
        accountDialog.value = true
        break
      case 'info':
        infoDialog.value = true
        break
      case 'category':
        categoryDialog.value = true
        break
      case 'about':
        aboutDialog.value = true
        break
      case 'tag':
        tagDialog.value = true
        break
      default:
        break
    }
  }
  const closeDialog = (type) => {
    switch (type) {
      case 'note':
        noteDialog.value = false
        break
      case 'project':
        projectDialog.value = false
        break
      case 'tool':
        toolDialog.value = false
        break
      case 'media':
        mediaDialog.value = false
        break
      case 'bug':
        bugDialog.value = false
        break

      case 'board':
        boardDialog.value = false
        break
      case 'account':
        accountDialog.value = false
        break
      case 'info':
        infoDialog.value = false
        break
      case 'category':
        categoryDialog.value = false
        categoryStore.resetCategoryForm()
        break
      case 'about':
        aboutDialog.value = false
        aboutStore.resetAboutForm()
        break
      case 'tag':
        tagDialog.value = false
        tagStore.resetTagForm()
        break
      default:
        break
    }
  }

  return {
    noteDialog,
    projectDialog,
    toolDialog,
    mediaDialog,
    bugDialog,
    boardDialog,
    accountDialog,
    infoDialog,
    categoryDialog,
    aboutDialog,
    tagDialog,
    openDialog,
    closeDialog,
  }
})
