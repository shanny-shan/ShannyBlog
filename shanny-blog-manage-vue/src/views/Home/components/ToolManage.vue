<script setup>
import ToolDialog from '@/views/_components/dialog/ToolDialog.vue'
import { onMounted, computed } from 'vue'
import { useAdminStore, useToolStore, usePageStore } from '@/stores'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const adminStore = useAdminStore()
const toolStore = useToolStore()
const pageStore = usePageStore()

const { pageList, totalPages } = pageStore.getPageData(() => toolStore.toolList)

const isSelectedAll = computed({
  get() {
    return pageStore.isAllRowsChecked(pageList)
  },
  set() {
    pageStore.toggleAllRows(pageList)
  },
})

onMounted(async () => {
  await toolStore.getToolList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button
        class="btn btn-warning"
        :disabled="pageStore.selectedIds.length === 0"
        @click="toolStore.deleteTools()"
      >
        Delete
      </button>
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
                <input
                  type="checkbox"
                  class="checkbox"
                  v-model="isSelectedAll"
                />
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
          <tr v-for="item in pageList" :key="item.id">
            <th>
              <label>
                <input
                  type="checkbox"
                  class="checkbox"
                  :value="item.id"
                  v-model="pageStore.selectedIds"
                />
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
            <td>
              {{
                item.href.length > 20
                  ? item.href.slice(0, 20) + '...'
                  : item.href
              }}
            </td>
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
                <button
                  class="btn btn-ghost btn-xs"
                  @click="toolStore.openEditTool(item)"
                >
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="toolStore.deleteTool(item)"
                >
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
        :current-page="pageStore.currentPage"
        :total-pages="totalPages"
        :page-range="5"
        @page-change="pageStore.handlePageChange"
      />
    </div>
  </div>
  <ToolDialog />
</template>
<style lang="scss" scoped></style>
