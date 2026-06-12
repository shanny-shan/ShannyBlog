<script setup>
import ToolDialog from '@/views/_components/dialog/ToolDialog.vue'
import { onMounted, ref, computed } from 'vue'
import { useAdminStore, useToolStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const toast = useToast()
const adminStore = useAdminStore()
const toolStore = useToolStore()

const currentPage = ref(1)
const itemsPerPage = ref(10)

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

const totalPages = computed(() => {
  return Math.ceil(toolStore.toolList.length / itemsPerPage.value)
})

// 计算当前页显示的条目
const items = computed(() => {
  const startIndex = (currentPage.value - 1) * itemsPerPage.value
  const endIndex = startIndex + itemsPerPage.value
  return toolStore.toolList.slice(startIndex, endIndex)
})
// 处理页码变化事件
const handlePageChange = (page) => {
  currentPage.value = page
  window.scrollTo({ top: 0, behavior: 'smooth' })
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
          <tr v-for="item in items" :key="item.id">
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
    <div class="mt-2 md:mt-10 flex justify-center" v-if="totalPages > 1">
      <PaginationComponent
        :current-page="currentPage"
        :total-pages="totalPages"
        :page-range="5"
        @page-change="handlePageChange"
      />
    </div>
  </div>
  <ToolDialog />
</template>
<style lang="scss" scoped></style>
