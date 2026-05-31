<script setup>
import ToolDialog from '@/views/_components/dialog/ToolDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore, useToolStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

const toast = useToast()
const adminStore = useAdminStore()
const toolStore = useToolStore()

const editTool = (item) => {
  adminStore.openDialog('tool')
  toolStore.toolForm = { ...item }
  adminStore.isEdit = true
}
const deleteTool = (item) => {
  swal(
    '',
    '',
    `确定删除标题为<span class="text-primary font-bold">${item.title}</span>的工具吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await toolStore.deleteTool(item.id)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await toolStore.getToolList()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

onMounted(async () => {
  await toolStore.getToolList()
})
</script>
<template>
  <div>
    <div class="flex justify-end">
      <button class="btn btn-primary" @click="adminStore.openDialog('tool')">
        Add Tool
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
            <th>Href</th>
            <th>Tags</th>
            <th>Published</th>
            <!-- <th>CreateTime</th> -->
            <th>UpdateTime</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in toolStore.toolList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>
              {{
                item.title.length > 10
                  ? item.title.slice(0, 10) + '...'
                  : item.title
              }}
            </td>
            <td>
              {{
                item.content.length > 10
                  ? item.content.slice(0, 10) + '...'
                  : item.content
              }}
            </td>
            <td>{{ item.href }}</td>
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
                <button class="btn btn-ghost btn-xs" @click="editTool(item)">
                  Edit
                </button>
                <button class="btn btn-ghost btn-xs" @click="deleteTool(item)">
                  Delete
                </button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <ToolDialog />
</template>
<style lang="scss" scoped></style>
