<script setup>
import TagDialog from '@/views/_components/dialog/TagDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore, useTagStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

const toast = useToast()
const adminStore = useAdminStore()
const tagStore = useTagStore()

const editTag = (item) => {
  adminStore.openDialog('tag')
  tagStore.tagForm = { ...item }
  adminStore.isEdit = true
}
const deleteTag = (item) => {
  swal(
    '',
    '',
    `确定删除名称为<span class="text-primary font-bold">${item.name}</span>的标签吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await tagStore.deleteTag(item.id)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await tagStore.getTagList()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

onMounted(async () => {
  await tagStore.getTagList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button class="btn btn-primary" @click="adminStore.openDialog('tag')">
        Add tag
      </button>
    </div>
    <div class="overflow-x-auto mt-2">
      <table class="table">
        <thead>
          <tr>
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <th>Name</th>
            <th>NameEn</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in tagStore.tagList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.name }}</td>
            <td>{{ item.nameEn }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs" @click="editTag(item)">
                  Edit
                </button>
                <button class="btn btn-ghost btn-xs" @click="deleteTag(item)">
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <TagDialog />
</template>
<style lang="scss" scoped></style>
