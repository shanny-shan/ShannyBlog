<script setup>
import categoryDialog from '@/views/_components/dialog/CategoryDialog.vue'
import { onMounted, ref } from 'vue'
import { useAdminStore, useCategoryStore } from '@/stores'
import { useToast } from 'vue-toastification'
import { swal } from '@/utils/sweetalert'

const toast = useToast()
const adminStore = useAdminStore()
const categoryStore = useCategoryStore()

const editCategory = (item) => {
  adminStore.openDialog('category')
  categoryStore.categoryForm = { ...item }
  adminStore.isEdit = true
}
const deleteCategory = (item) => {
  swal(
    '',
    '',
    `确定删除名称为<span class="text-primary font-bold">${item.name}</span>的类型吗？`,
    'question',
    true,
    true,
  ).then(async (result) => {
    if (result.isConfirmed) {
      const res = await categoryStore.deleteCategory(item.id)
      if (res.data.code.toLowerCase() === 'success') {
        toast.success(`${res.data.msg}`)
        await categoryStore.getCategoryList()
      } else {
        toast.error(`${res.data.msg}`)
      }
    }
  })
}

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
          <tr v-for="item in categoryStore.categoryList">
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
                  @click="editCategory(item)"
                >
                  Edit
                </button>
                <button
                  class="btn btn-ghost btn-xs"
                  @click="deleteCategory(item)"
                >
                  Delete
                </button>
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
