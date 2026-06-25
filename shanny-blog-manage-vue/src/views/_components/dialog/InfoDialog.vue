<script setup>
import { onMounted } from 'vue'
import { useAdminStore, useAccountStore } from '@/stores'

const adminStore = useAdminStore()
const accountStore = useAccountStore()

onMounted(() => {
  accountStore.setDate()
})
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
        <legend class="fieldset-legend">Edit UserInfo</legend>

        <label class="label w-full">UserName</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input userName"
          v-model="accountStore.userForm.userDetails.username"
        />

        <label class="label w-full">NickName</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input nickName"
          v-model="accountStore.userForm.userDetails.nickname"
        />

        <label class="label w-full">Birthday</label>
        <input
          type="date"
          class="input input-primary bg-base-200 w-full"
          required
          placeholder="Pick a date"
          title="Must be valid URL"
          v-model="accountStore.userForm.userDetails.birthday"
        />

        <!-- <label class="label w-full">Status</label>
        <select
          class="select bg-base-200 w-full select-primary"
          v-model="accountStore.userForm.status"
        >
          <option
            v-for="[value, label] in Object.entries(statusOptions)"
            :key="value"
            :value="value"
          >
            {{ label }}
          </option>
        </select> -->

        <label class="label w-full">Sex</label>
        <select
          class="select bg-base-200 w-full select-primary"
          v-model="accountStore.userForm.userDetails.sex"
        >
          <option
            v-for="[value, label] in Object.entries(accountStore.sexOptions)"
            :key="value"
            :value="value"
          >
            {{ label }}
          </option>
        </select>

        <label class="label w-full">Mobile</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input Mobile"
          v-model="accountStore.userForm.mobile"
        />

        <div class="mt-1 flex items-center justify-between gap-2">
          <button
            class="btn btn-primary w-1/2"
            @click="accountStore.submitInfo()"
          >
            Submit
          </button>
          <button
            class="btn btn-soft w-1/2"
            @click="adminStore.closeDialog('info')"
          >
            Cancel
          </button>
        </div>
      </fieldset>
    </div>
  </dialog>
</template>
<style lang="scss" scoped>
.cally {
  background-color: theme('colors.base-100');
  border-radius: 0.5rem;
}
</style>
