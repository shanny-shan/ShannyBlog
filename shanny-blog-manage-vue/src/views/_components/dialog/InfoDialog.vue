<script setup>
import { ref } from 'vue'
import { useAdminStore } from '@/stores/modules/admin'
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
  <dialog class="modal h-full" :open="adminStore.infoDialog">
    <div class="modal-box bg-base-200">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="adminStore.closeDialog('info')"
        >
          ✕
        </button>
      </form>
      <fieldset
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add User</legend>

        <label class="label w-full">Title</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input title"
        />

        <label class="label w-full">Href</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input href"
        />

        <label class="label w-full">Image</label>
        <input
          type="file"
          class="file-input file-input-primary bg-base-200 w-full"
        />

        <label class="label w-full">Category</label>
        <select class="select bg-base-200 w-full select-primary">
          <option disabled selected>Pick a Category</option>
          <option>Study</option>
          <option>Work</option>
          <option>Life</option>
        </select>

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
      </fieldset>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
