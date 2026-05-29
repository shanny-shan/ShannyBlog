<script setup>
import ToolDialog from '@/views/_components/dialog/ToolDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore, useToolStore, useSiteStore } from '@/stores'
const adminStore = useAdminStore()
const toolStore = useToolStore()
const siteStore = useSiteStore()

const toolList = ref([])

const getToolList = async () => {
  siteStore.loading = true
  const res = await toolStore.getToolList()
  if (res.data.code.toLowerCase() === 'success') {
    toolList.value = res.data.data || []
    siteStore.loading = false
  }
}

const editTool = (item) => {
  adminStore.openDialog('tool')
  toolStore.toolForm = item
  adminStore.isEdit = true
}
const deleteTool = (item) => {}

onMounted(() => {
  getToolList()
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
          <tr v-for="item in toolList">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.title }}</td>
            <td>{{ item.content }}</td>
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
            <td>{{ item.updateTime.substring(0, 10) }}</td>
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
