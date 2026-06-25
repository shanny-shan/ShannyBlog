<script setup>
import TagDialog from '@/views/_components/dialog/TagDialog.vue'
import { onMounted, computed } from 'vue'
import { useAdminStore, useTagStore, usePageStore } from '@/stores'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const adminStore = useAdminStore()
const tagStore = useTagStore()
const pageStore = usePageStore()

const { pageList, totalPages } = pageStore.getPageData(() => tagStore.tagList)

const isSelectedAll = computed({
  get() {
    return pageStore.isAllRowsChecked(pageList)
  },
  set() {
    pageStore.toggleAllRows(pageList)
  },
})

onMounted(async () => {
  await tagStore.getTagList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button
        class="btn btn-warning"
        :disabled="pageStore.selectedIds.length === 0"
        @click="tagStore.deleteTags()"
      >
        Delete
      </button>
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
                <input
                  type="checkbox"
                  class="checkbox"
                  v-model="isSelectedAll"
                />
              </label>
            </th>
            <th>Name</th>
            <th>NameEn</th>
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
            <td>{{ item.name }}</td>
            <td>{{ item.nameEn }}</td>
            <th>
              <div class="flex gap-2">
                <button
                  class="btn btn-ghost btn-xs"
                  @click="tagStore.openEditTag(item)"
                >
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="tagStore.deleteTag(item)"
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
  <TagDialog />
</template>
<style lang="scss" scoped></style>
