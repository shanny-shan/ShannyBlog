<script setup>
import EditComponent from '../common/EditComponent.vue'

import { onMounted } from 'vue'
import {
  useAdminStore,
  useTagStore,
  useCategoryStore,
  useArticleStore,
} from '@/stores'

const adminStore = useAdminStore()
const tagStore = useTagStore()
const articleStore = useArticleStore()
const categoryStore = useCategoryStore()

onMounted(async () => {
  tagStore.getTagAll()
  categoryStore.getCategoryId('ARTICLE_NOTE')
})
</script>
<template>
  <dialog class="modal h-full" :open="adminStore.noteDialog">
    <div class="modal-box bg-base-200 w-3/4 max-w-3/4 h-14/15 overflow-visible">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="articleStore.closeDialog('note', 'ARTICLE_NOTE')"
        >
          ✕
        </button>
      </form>
      <form
        @submit.prevent="articleStore.submitArticle('ARTICLE_NOTE')"
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Note</legend>

        <div class="flex flex-col gap-2">
          <div class="flex flex-col md:flex-row md:gap-2">
            <div class="md:w-2/3">
              <label class="label w-full">Title</label>
              <input
                type="text"
                class="input validator input-primary bg-base-200 w-full"
                required
                min="1"
                maxlength="30"
                placeholder="Please input title"
                v-model="articleStore.articleForm.title"
                title="标题不能为空，且最长不能超过30个字符"
              />
              <p class="validator-hint hidden">
                标题不能为空，且最长不能超过30个字符
              </p>
            </div>

            <div class="md:w-1/3">
              <label class="label w-full">Category</label>
              <select
                class="select bg-base-200 w-full select-primary"
                v-model="articleStore.articleForm.categoryId"
              >
                <option
                  v-for="item in categoryStore.curCategories"
                  :key="item.id"
                  :value="item.id"
                >
                  {{ item.name }}
                </option>
              </select>
            </div>
            <!-- <div class="md:w-1/3">
              <label class="label w-full">Image</label>
              <input
                type="file"
                class="file-input file-input-primary bg-base-200 w-full"
                @change="inputImage($event)"
              />
            </div> -->
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
          <label class="label w-full">Content</label>
          <EditComponent class="h-full" />
        </div>
        <div class="mt-1 flex items-center justify-between gap-2">
          <button type="submit" class="btn btn-primary w-1/2">Submit</button>
          <button
            class="btn btn-soft w-1/2"
            @click="articleStore.closeDialog('note', 'ARTICLE_NOTE')"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
