<script setup>
import { useToast } from 'vue-toastification'
import { useAdminStore, useSiteStore, useAboutStore } from '@/stores'

const toast = useToast()
const adminStore = useAdminStore()
const siteStore = useSiteStore()
const aboutStore = useAboutStore()

const addAbout = async (about) => {
  siteStore.loading = true
  const res = await aboutStore.addAbout(about)
  if (res?.data?.code == 'SUCCESS') {
    aboutStore.authors.push(res.data.data)
    if (res.data.data.isActive) {
      aboutStore.authorInfo = res.data.data
    }
    toast.success(`${res.data.msg}`)
    adminStore.closeDialog('about')
  } else {
    toast.error(`${res.data.msg}`)
  }
  siteStore.loading = false
}
</script>
<template>
  <dialog class="modal h-full" :open="adminStore.aboutDialog">
    <div class="modal-box bg-base-200">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="adminStore.closeDialog('about')"
        >
          ✕
        </button>
      </form>
      <fieldset
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add About</legend>

        <label class="label w-full">avatar</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input avatar"
          v-model="aboutStore.aboutForm.avatar"
        />

        <label class="label w-full">name</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input name"
          v-model="aboutStore.aboutForm.name"
        />

        <label class="label w-full">introduce</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input introduce"
          v-model="aboutStore.aboutForm.introduce"
        />

        <label class="label w-full">tag</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input tag"
          v-model="aboutStore.aboutForm.tag"
        />

        <label class="label w-full">github</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input github"
          v-model="aboutStore.aboutForm.github"
        />

        <!-- <label class="label w-full">steam</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input steam"
          v-model="aboutStore.aboutForm.steam"
        /> -->

        <label class="label w-full">web</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input web"
          v-model="aboutStore.aboutForm.web"
        />

        <!-- <label class="label w-full">bilibili</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input bilibili"
          v-model="aboutStore.aboutForm.biliBili"
        /> -->

        <label class="label w-full">isActive</label>
        <input
          type="checkbox"
          class="checkbox checkbox-primary bg-base-200"
          v-model="aboutStore.aboutForm.isActive"
        />

        <!-- <label class="label w-full">other</label>
        <input
          type="text"
          class="input input-primary bg-base-200 w-full"
          placeholder="Please input other"
          v-model="aboutStore.aboutForm.other"
        /> -->

        <button class="btn btn-primary" @click="addAbout(aboutStore.aboutForm)">
          Add
        </button>
      </fieldset>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
