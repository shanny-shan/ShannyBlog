<script setup>
import categoryDialog from '@/views/_components/dialog/CategoryDialog.vue'
import { onMounted } from 'vue'
import { useAdminStore, useCategoryStore, usePageStore } from '@/stores'
import PaginationComponent from '@/views/_components/common/PaginationComponent.vue'

const adminStore = useAdminStore()
const categoryStore = useCategoryStore()
const pageStore = usePageStore()

const { pageList, totalPages } = pageStore.getPageData(
  () => categoryStore.categoryList,
)

onMounted(async () => {
  await categoryStore.getCategoryList()
})
</script>
<template>
  <div>
    <div class="flex justify-end gap-2">
      <button
        class="btn btn-primary"
        @click="adminStore.openDialog('category')"
      >
        Add category
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
            <th>Sort</th>
            <th>Type</th>
            <th>Edit</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in pageList" :key="item.id">
            <th>
              <label>
                <input type="checkbox" class="checkbox" />
              </label>
            </th>
            <td>{{ item.name }}</td>
            <td>{{ item.nameEn }}</td>
            <td>{{ item.sort }}</td>
            <td>{{ item.type }}</td>
            <th>
              <div class="flex gap-2">
                <button
                  class="btn btn-ghost btn-xs"
                  @click="categoryStore.openEditCategory(item)"
                >
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="categoryStore.deleteCategory(item)"
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
  <categoryDialog />
</template>
<style lang="scss" scoped></style>
