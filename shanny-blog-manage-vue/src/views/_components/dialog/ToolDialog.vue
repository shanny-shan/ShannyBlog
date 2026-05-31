<script setup>
import { onMounted, ref } from 'vue'
import { useToast } from 'vue-toastification'
import {
  useAdminStore,
  useTagStore,
  useToolStore,
  useSiteStore,
} from '@/stores'

const toast = useToast()
const adminStore = useAdminStore()
const tagStore = useTagStore()
const toolStore = useToolStore()
const siteStore = useSiteStore()

const submitTool = async () => {
  siteStore.loading = true

  let res = null
  if (adminStore.isEdit) {
    res = await toolStore.editTool(toolStore.toolForm)
  } else {
    res = await toolStore.addTool(toolStore.toolForm)
  }

  if (res.data.code.toLowerCase() === 'success') {
    toast.success(`${res.data.msg}`)
    adminStore.closeDialog('tool')
    await toolStore.getToolList()
  } else {
    toast.error(`${res.data.msg}`)
  }
  siteStore.loading = false
}

const inputImage = (event) => {
  toolStore.toolForm.image = event.target.files[0]
}

onMounted(async () => {
  const tagResult = await tagStore.getTagAll()
  if (tagResult.data.code.toLowerCase() === 'success') {
    tagStore.tags = tagResult.data.data
  }
})
</script>
<template>
  <dialog class="modal h-full" :open="adminStore.toolDialog">
    <div class="modal-box bg-base-200">
      <form method="dialog">
        <button
          class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          @click="adminStore.closeDialog('tool')"
        >
          ✕
        </button>
      </form>
      <form
        @submit.prevent="submitTool"
        class="fieldset bg-base-100 border-primary shadow-sm rounded-box w-full max-w-full border p-4 h-full max-h-full flex flex-col"
      >
        <legend class="fieldset-legend">Add Tool</legend>

        <label class="label w-full">Title</label>
        <input
          type="text"
          class="input validator input-primary bg-base-200 w-full"
          required
          placeholder="Please input title"
          title="标题不能为空，且最长不能超过30个字符"
          v-model="toolStore.toolForm.title"
        />
        <p class="validator-hint hidden">
          标题不能为空，且最长不能超过30个字符
        </p>

        <label class="label w-full">Href</label>
        <input
          type="text"
          class="input validator input-primary bg-base-200 w-full"
          required
          pattern="^https?://[^\s]{1,}"
          placeholder="Please input href"
          title="请输入合法的链接地址，必须以 http:// 或 https:// 开头"
          v-model="toolStore.toolForm.href"
        />
        <p class="validator-hint hidden">
          请输入合法的链接地址，必须以 http:// 或 https:// 开头
        </p>

        <label class="label w-full">Content</label>
        <textarea
          type="text"
          class="textarea textarea-primary bg-base-200 w-full"
          placeholder="Please input content"
          v-model="toolStore.toolForm.content"
        ></textarea>

        <!-- <label class="label w-full">Image</label>
        <input
          type="file"
          class="file-input file-input-primary bg-base-200 w-full"
          @change="inputImage($event)"
        /> -->

        <label class="label w-full">Tag</label>
        <div class="flex flex-row flex-wrap">
          <label
            v-for="item in tagStore.tags"
            :key="item.id"
            class="cursor-pointer mr-2 mt-1"
          >
            <input
              type="checkbox"
              class="hidden peer"
              :value="item.id"
              v-model="toolStore.toolForm.tags"
            />
            <div class="badge badge-soft peer-checked:badge-primary">
              {{ item.name }}
            </div>
          </label>
        </div>

        <div class="mt-1 flex items-center justify-between gap-2">
          <button type="submit" class="btn btn-primary w-1/2">Submit</button>
          <button
            class="btn btn-soft w-1/2"
            @click="adminStore.closeDialog('tool')"
          >
            Cancel
          </button>
        </div>
      </form>
    </div>
  </dialog>
</template>
<style lang="scss" scoped></style>
