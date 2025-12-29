<script setup>
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import { useAdminStore } from '@/stores/modules/admin'
import { useSiteStore } from '@/stores/modules/site'
import { useTagStore } from '@/stores/modules/tag'

const toast = useToast()
const adminStore = useAdminStore()
const siteStore = useSiteStore()
const tagStore = useTagStore()

const addTag = async (tag) => {
  siteStore.loading = true
  const res = await tagStore.addTag(tag)
  if (res.data.code.toLowerCase() === 'success') {
    tagStore.tags.push(res.data.data)
    toast.success(`${res.data.msg}`)
    adminStore.closeDialog('tag')
  } else {
    toast.error(`${res.data.msg}`)
  }
  siteStore.loading = false
}
</script>
<template>
  <dialog class="modal h-full" :open="adminStore.tagDialog">
    <div class="modal-box bg-base-200">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="adminStore.closeDialog('tag')"
        >
          ✕
        </button>
      </form>
      <fieldset
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Tag</legend>

        <label class="label w-full">name</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input name"
          v-model="tagStore.tagForm.name"
        />

        <label class="label w-full">nameEn</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input nameEn"
          v-model="tagStore.tagForm.nameEn"
        />
        <button class="btn btn-primary" @click="addTag(tagStore.tagForm)">
          Add
        </button>
      </fieldset>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
