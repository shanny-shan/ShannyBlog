<script setup>
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import { useAdminStore } from '@/stores/modules/admin'
import { useSiteStore } from '@/stores/modules/site'
import { useCategoryStore } from '@/stores/modules/category'

const adminStore = useAdminStore()
const siteStore = useSiteStore()
const categoryStore = useCategoryStore()
const toast = useToast()

const addCategory = async (category) => {
  siteStore.loading = true
  const res = await categoryStore.addCategory(category)
  if (res?.data?.code == 'SUCCESS') {
    categoryStore.categories.push(res.data.data)
    toast.success(`${res.data.msg}`)
    adminStore.closeDialog('category')
  } else {
    toast.error(`${res.data.msg}`)
  }
  siteStore.loading = false
}
</script>
<template>
  <dialog class="modal h-full" :open="adminStore.categoryDialog">
    <div class="modal-box bg-base-200">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="adminStore.closeDialog('category')"
        >
          ✕
        </button>
      </form>
      <fieldset
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Category</legend>

        <label class="label w-full">name</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input name"
          v-model="categoryStore.categoryForm.name"
        />

        <label class="label w-full">nameEn</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input nameEn"
          v-model="categoryStore.categoryForm.nameEn"
        />

        <label class="label w-full">Type</label>
        <select
          class="select bg-base-200 w-full select-primary"
          v-model="categoryStore.categoryForm.type"
        >
          <option
            v-for="(item, key) in categoryStore.categoryType"
            :key="key"
            :value="key"
          >
            {{ item }}
          </option>
        </select>

        <label class="label w-full">sort</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input sort"
          v-model="categoryStore.categoryForm.sort"
        />

        <button
          class="btn btn-primary"
          @click="addCategory(categoryStore.categoryForm)"
        >
          Add
        </button>
      </fieldset>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
