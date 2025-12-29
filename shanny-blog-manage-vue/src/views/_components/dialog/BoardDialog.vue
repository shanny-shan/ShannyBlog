<script setup>
import { ref } from 'vue'
import { useAdminStore } from '@/stores/modules/admin'
import EditComponent from '../common/EditComponent.vue'
const adminStore = useAdminStore()
const tags = [
  'Vue',
  'React',
  'Angular',
  'Svelte',
  'TypeScript',
  'JavaScript',
  'CSS',
]
const selected = ref([])
</script>
<template>
  <dialog class="modal h-full" :open="adminStore.boardDialog">
    <div class="modal-box bg-base-200 w-3/4 max-w-3/4 h-14/15 overflow-visible">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="adminStore.closeDialog('board')"
        >
          ✕
        </button>
      </form>
      <fieldset
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Board</legend>

        <div class="flex flex-col gap-2">
          <div class="flex flex-col md:flex-row md:gap-2">
            <div class="md:w-1/3">
              <label class="label w-full">Title</label>
              <input
                type="text"
                class="input input-primary bg-base-200 w-full"
                placeholder="Please input title"
              />
            </div>
            <div class="md:w-1/3">
              <label class="label w-full">Category</label>
              <select class="select bg-base-200 w-full select-primary">
                <option disabled selected>Pick a Category</option>
                <option>Notice</option>
                <option>Message</option>
              </select>
            </div>
            <div class="md:w-1/3">
              <label class="label w-full">Image</label>
              <input
                type="file"
                class="file-input file-input-primary bg-base-200 w-full"
              />
            </div>
          </div>

          <label class="label w-full">Tag</label>
          <div class="flex flex-row flex-wrap">
            <label
              v-for="tag in tags"
              :key="tag"
              class="cursor-pointer mr-2 mt-1"
            >
              <input
                type="checkbox"
                class="hidden peer"
                :value="tag"
                v-model="selected"
              />
              <div
                class="badge badge-soft badge-neutral peer-checked:badge-primary"
              >
                {{ tag }}
              </div>
            </label>
          </div>
        </div>

        <div class="flex-1 mt-2 overflow-auto">
          <label class="label w-full">Content</label>
          <EditComponent class="h-full" />
        </div>
      </fieldset>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
