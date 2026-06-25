<script setup>
import { useAdminStore, useCategoryStore } from '@/stores'
const adminStore = useAdminStore()
const categoryStore = useCategoryStore()
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
      <form
        @submit.prevent="categoryStore.submitCategory"
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Category</legend>

        <label class="label w-full">name</label>
        <input
          type="text"
          class="input validator input-primary bg-base-200 w-full"
          required
          placeholder="Please input name"
          minlength="1"
          maxlength="10"
          title="名称不能为空，且最长不能超过10个字符"
          v-model="categoryStore.categoryForm.name"
        />
        <p class="validator-hint hidden">
          名称不能为空，且最长不能超过10个字符
        </p>

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
            v-for="[value, label] in Object.entries(categoryStore.categoryType)"
            :key="value"
            :value="value"
          >
            {{ label }}
          </option>
        </select>

        <label class="label w-full">sort</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input sort"
          v-model="categoryStore.categoryForm.sort"
        />

        <div class="mt-1 flex items-center justify-between gap-2">
          <button type="submit" class="btn btn-primary w-1/2">Submit</button>
          <button
            class="btn btn-soft w-1/2"
            @click="adminStore.closeDialog('category')"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
