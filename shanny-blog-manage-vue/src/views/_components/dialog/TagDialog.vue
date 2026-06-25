<script setup>
import { useAdminStore, useTagStore } from '@/stores'
const adminStore = useAdminStore()
const tagStore = useTagStore()
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
      <form
        @submit.prevent="tagStore.submitTag"
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Tag</legend>

        <label class="label w-full">name</label>
        <input
          type="text"
          class="input validator input-primary bg-base-200 w-full"
          required
          minlength="1"
          maxlength="10"
          title="名称不能为空，且最长不能超过10个字符"
          placeholder="Please input name"
          v-model="tagStore.tagForm.name"
        />
        <p class="validator-hint hidden">
          名称不能为空，且最长不能超过10个字符
        </p>

        <label class="label w-full">nameEn</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input nameEn"
          v-model="tagStore.tagForm.nameEn"
        />

        <div class="mt-1 flex items-center justify-between gap-2">
          <button type="submit" class="btn btn-primary w-1/2">Submit</button>
          <button
            class="btn btn-soft w-1/2"
            @click="adminStore.closeDialog('tag')"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
