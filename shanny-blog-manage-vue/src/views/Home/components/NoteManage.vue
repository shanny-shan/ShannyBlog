<script setup>
import NoteDialog from '@/views/_components/dialog/NoteDialog.vue'
import { onMounted } from 'vue'
import { useToast } from 'vue-toastification'
import { useAdminStore, useArticleStore } from '@/stores'
import { swal } from '@/utils/sweetalert'

const toast = useToast()
const adminStore = useAdminStore()
const articleStore = useArticleStore()

const editNote = (item) => {
  adminStore.openDialog('note')
  articleStore.articleForm = { ...item }
  adminStore.isEdit = true
}

const deleteNote = (item) => {
  swal(
    '',
    '',
    `确定删除标题为<span class="text-primary font-bold">${item.title}</span>的笔记吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await articleStore.deleteArticle(item.id)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await articleStore.getNoteList()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

onMounted(async () => {
  await articleStore.getNoteList()
})
</script>
<template>
  <div>
    <div class="flex justify-end">
      <button class="btn btn-primary" @click="adminStore.openDialog('note')">
        Add Note
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
            <th>Title</th>
            <th>Content</th>
            <th>Tags</th>
            <th>Published</th>
            <!-- <th>CreateTime</th> -->
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in articleStore.noteList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.title }}</td>
            <td>{{ item.content }}</td>
            <td>
              <div class="flex flex-wrap gap-2">
                <div
                  v-for="tag in item.tagList"
                  :key="tag.id"
                  class="badge badge-soft badge-primary"
                >
                  {{ tag.name }}
                </div>
              </div>
            </td>
            <td>{{ item.published }}</td>
            <!-- <td>{{ item.createTime }}</td> -->
            <td>{{ item.updateTime?.substring(0, 10) }}</td>
            <th>
              <div class="flex gap-2">
                <button class="btn btn-ghost btn-xs" @click="editNote(item)">
                  Edit
                </button>
                <button class="btn btn-ghost btn-xs" @click="deleteNote(item)">
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <NoteDialog />
</template>
<style lang="scss" scoped></style>
