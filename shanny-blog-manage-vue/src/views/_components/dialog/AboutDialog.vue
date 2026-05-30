<script setup>
import { useToast } from 'vue-toastification'
import { useAdminStore, useSiteStore, useAboutStore } from '@/stores'

const toast = useToast()
const adminStore = useAdminStore()
const siteStore = useSiteStore()
const aboutStore = useAboutStore()

const closeDialog = () => {
  adminStore.closeDialog('about')
}

const submitAbout = async (about) => {
  siteStore.loading = true
  let res = null
  if (adminStore.isEdit) {
    res = await aboutStore.editAbout(about)
  } else {
    res = await aboutStore.addAbout(about)
  }

  if (res?.data?.code == 'SUCCESS') {
    toast.success(`${res.data.msg}`)
    adminStore.closeDialog('about')
    await aboutStore.getAboutList()
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
        <div class="mt-1 flex items-center justify-between gap-2">
          <button
            class="btn btn-primary w-1/2"
            @click="submitAbout(aboutStore.aboutForm)"
          >
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
<style lang="scss" scoped></style>
