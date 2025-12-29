<script setup>
import EditComponent from '../common/EditComponent.vue'

import { onMounted, ref } from 'vue'
import { useToast } from 'vue-toastification'
import { useAdminStore } from '@/stores/modules/admin'
import { useTagStore } from '@/stores/modules/tag'
import { useCategoryStore } from '@/stores/modules/category'
import { useArticleStore } from '@/stores/modules/article'
import { useSiteStore } from '@/stores/modules/site'

const toast = useToast()
const adminStore = useAdminStore()
const tagStore = useTagStore()
const categoryStore = useCategoryStore()
const articleStore = useArticleStore()
const siteStore = useSiteStore()
const curCategories = ref([])

const closeDialog = () => {
  adminStore.closeDialog('bug')
  getCategoryId()
}

const submitArticle = async (type) => {
  siteStore.loading = true
  articleStore.articleForm.type = 'ARTICLE_BUG'
  const res = await articleStore.addArticle(articleStore.articleForm)
  if (res.data.code.toLowerCase() === 'success') {
    toast.success(`${res.data.msg}`)
    closeDialog()
  } else {
    toast.error(`${res.data.msg}`)
  }
  siteStore.loading = false
}

const inputImage = (event) => {
  articleStore.articleForm.image = event.target.files[0]
}

const getCategoryId = async () => {
  const categoryResult = await categoryStore.getCategories()
  if (categoryResult.data.code.toLowerCase() === 'success') {
    curCategories.value = categoryResult.data.data.filter(
      (item) => item.type == 'ARTICLE_BUG'
    )
    articleStore.articleForm.categoryId = curCategories.value[0].id
  }
}

onMounted(async () => {
  const tagResult = await tagStore.getTagList()
  if (tagResult.data.code.toLowerCase() === 'success') {
    tagStore.tags = tagResult.data.data
  }
  getCategoryId()
})
</script>
<template>
  <dialog class="modal h-full" :open="adminStore.bugDialog">
    <div class="modal-box bg-base-200 w-3/4 max-w-3/4 h-14/15 overflow-visible">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="closeDialog()"
        >
          ✕
        </button>
      </form>
      <fieldset
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Bug</legend>

        <div class="flex flex-col gap-2">
          <div class="flex flex-col md:flex-row md:gap-2">
            <div class="md:w-1/3">
              <label class="label w-full">Question</label>
              <input
                type="text"
                class="input input-primary bg-base-200 w-full"
                placeholder="Please input question"
                v-model="articleStore.articleForm.title"
              />
            </div>
            <div class="md:w-1/3">
              <label class="label w-full">Category</label>
              <select
                class="select bg-base-200 w-full select-primary"
                v-model="articleStore.articleForm.categoryId"
              >
                <option
                  v-for="item in curCategories"
                  :key="item.id"
                  :value="item.id"
                >
                  {{ item.name }}
                </option>
              </select>
            </div>
            <div class="md:w-1/3">
              <label class="label w-full">Image</label>
              <input
                type="file"
                class="file-input file-input-primary bg-base-200 w-full"
                @change="inputImage($event)"
              />
            </div>
          </div>

          <label class="label w-full">Tag</label>
          <div class="flex flex-row flex-wrap">
            <label
              v-for="item in tagStore.tags"
              :key="item.id"
              class="cursor-pointer mr-2 mt-1"
            >
              <input
                type="checkbox"
                class="hidden peer"
                :value="item.id"
                v-model="articleStore.articleForm.tags"
              />
              <div class="badge badge-soft peer-checked:badge-primary">
                {{ item.name }}
              </div>
            </label>
          </div>
        </div>

        <div class="flex-1 mt-2 overflow-auto">
          <label class="label w-full">Description</label>
          <EditComponent class="h-full" />
        </div>

        <div class="mt-1 flex items-center justify-between gap-2">
          <button class="btn btn-primary w-1/2" @click="submitArticle('bug')">
            Submit
          </button>
          <button class="btn btn-soft w-1/2" @click="closeDialog()">
            Cancel
          </button>
        </div>
      </fieldset>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
