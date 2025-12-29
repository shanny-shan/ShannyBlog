<script setup>
import categoryDialog from '@/views/_components/dialog/CategoryDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore } from '@/stores/modules/admin'
import { useCategoryStore } from '@/stores/modules/category'
const adminStore = useAdminStore()
const categoryStore = useCategoryStore()
const categoryList = ref([])

const getCategoryList = async () => {
  const res = await categoryStore.getCategories()
  if (res.data.code.toLowerCase() === 'success') {
    categoryList.value = res.data.data || []
  }
}
onMounted(() => {
  getCategoryList()
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
          <tr v-for="item in categoryList">
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
                <button class="btn btn-ghost btn-xs">Edit</button>
                <button class="btn btn-ghost btn-xs">Delete</button>
              </div>
            </th>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <categoryDialog />
</template>
<style lang="scss" scoped></style>
