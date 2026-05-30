<script setup>
import { onMounted, ref } from 'vue'
import 'cally'
import { useAdminStore, useAccountStore, useSiteStore } from '@/stores'
import { useToast } from 'vue-toastification'

const toast = useToast()
const adminStore = useAdminStore()
const accountStore = useAccountStore()
const siteStore = useSiteStore()

const sexOptions = ref({
  UNKNOWN: '未知',
  MAN: '男',
  FEMALE: '女',
})
const statusOptions = ref({
  ACTIVE: '启用',
  LOCKED: '锁定',
  DELETED: '删除',
})
const DEFAULT_TEXT = ref('请选择日期')

const selectedDate = ref(DEFAULT_TEXT.value)
const calendarRef = ref(null)
const dropdownOpen = ref(false)

const handleDateChange = (e) => {
  const date = e.target.value
  selectedDate.value = date
  accountStore.userForm.userDetails.birthday = date
  dropdownOpen.value = false
}

const setDate = () => {
  const birthday = accountStore.userForm.userDetails.birthday
  if (birthday) {
    selectedDate.value = birthday
    if (calendarRef.value) {
      calendarRef.value.value = birthday
    }
  } else {
    selectedDate.value = DEFAULT_TEXT.value
  }
}

const closeDialog = () => {
  adminStore.closeDialog('info')
}

const submitInfo = async () => {
  siteStore.loading = true

  if (adminStore.isEdit) {
    const res = await accountStore.editUserInfo(accountStore.userForm)
    if (res.data.code.toLowerCase() === 'success') {
      toast.success(`${res.data.msg}`)
      closeDialog()
      await accountStore.getAllUsers()
    } else {
      toast.error(`${res.data.msg}`)
    }
  }

  siteStore.loading = false
}

onMounted(() => {
  setDate()
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
        <div class="dropdown w-full">
          <button
            @click="dropdownOpen = true"
            class="input input-primary bg-base-200 w-full"
          >
            {{ selectedDate }}
          </button>

          <div
            v-show="dropdownOpen"
            class="dropdown-content bg-base-100 rounded-box shadow-lg absolute top-full left-0 z-50 mt-2"
          >
            <calendar-date
              ref="calendarRef"
              class="cally"
              @change="handleDateChange"
            >
              <svg
                aria-label="Previous"
                class="fill-current size-4"
                slot="previous"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
              >
                <path d="M15.75 19.5 8.25 12l7.5-7.5"></path>
              </svg>
              <svg
                aria-label="Next"
                class="fill-current size-4"
                slot="next"
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
              >
                <path d="m8.25 4.5 7.5 7.5-7.5 7.5"></path>
              </svg>
              <calendar-month></calendar-month>
            </calendar-date>
          </div>
        </div>

        <label class="label w-full">Status</label>
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
        </select>

        <label class="label w-full">Sex</label>
        <select
          class="select bg-base-200 w-full select-primary"
          v-model="accountStore.userForm.userDetails.sex"
        >
          <option
            v-for="[value, label] in Object.entries(sexOptions)"
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
          <button class="btn btn-primary w-1/2" @click="submitInfo()">
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
<style lang="scss" scoped>
.cally {
  background-color: theme('colors.base-100');
  border-radius: 0.5rem;
}
</style>
